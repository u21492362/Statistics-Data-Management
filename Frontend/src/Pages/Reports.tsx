import { useEffect, useMemo, useState } from "react";
import {
  RefreshCw,
  CalendarRange,
  Church,
  BarChart3,
  Building2,
  Network,
} from "lucide-react";
import { ErrorAlert } from "../Components/common/ErrorAlert";
import type { DropdownItem } from "../types/lookup";
import type { SocietySummaryDto, CircuitSummaryDto } from "../types/report";
import type { RegisterDto } from "../types/register";
import { lookupService } from "../services/lookupService";
import { registerService } from "../services/registerService";
import { reportService } from "../services/reportService";
import { LoadingState } from "../Components/common/LoadingState";

export default function ReportsPage() {
  const [societies, setSocieties] = useState<DropdownItem[]>([]);
  const [years, setYears] = useState<DropdownItem[]>([]);
  const [registers, setRegisters] = useState<RegisterDto[]>([]);

  const [selectedSociety, setSelectedSociety] = useState("");
  const [selectedYear, setSelectedYear] = useState("");

  const [societySummary, setSocietySummary] = useState<SocietySummaryDto | null>(null);
  const [circuitSummary, setCircuitSummary] = useState<CircuitSummaryDto | null>(null);

  const [loading, setLoading] = useState(true);
  const [loadingReports, setLoadingReports] = useState(false);
  const [error, setError] = useState("");

  const currentSocietyId = Number(selectedSociety || 0);
  const currentYearId = Number(selectedYear || 0);

  const activeRegister = useMemo(() => {
    return (
      registers.find(
        (x) =>
          x.societyId === currentSocietyId &&
          x.statisticalYearId === currentYearId
      ) || null
    );
  }, [registers, currentSocietyId, currentYearId]);

  useEffect(() => {
    loadInitialData();
  }, []);

  useEffect(() => {
    if (activeRegister) {
      loadReports(activeRegister);
    } else {
      setSocietySummary(null);
      setCircuitSummary(null);
    }
  }, [activeRegister?.id]);

  async function loadInitialData() {
    try {
      setLoading(true);
      setError("");

      const [societyData, yearData, registerData] = await Promise.all([
        lookupService.getSocieties(),
        lookupService.getYears(),
        registerService.getRegisters(),
      ]);

      setSocieties(societyData);
      setYears(yearData);
      setRegisters(registerData);

      if (societyData.length > 0) {
        setSelectedSociety(String(societyData[0].id));
      }

      if (yearData.length > 0) {
        setSelectedYear(String(yearData[0].id));
      }
    } catch (e) {
      setError((e as Error).message);
    } finally {
      setLoading(false);
    }
  }

  async function loadReports(register: RegisterDto) {
    try {
      setLoadingReports(true);
      setError("");

      const [societyData, circuitData] = await Promise.all([
        reportService.getSocietySummary(
          register.statisticalYearId,
          register.societyId
        ),
        reportService.getCircuitSummary(
          register.statisticalYearId,
          register.circuitId
        ),
      ]);

      setSocietySummary(societyData);
      setCircuitSummary(circuitData);
    } catch (e) {
      setError((e as Error).message);
      setSocietySummary(null);
      setCircuitSummary(null);
    } finally {
      setLoadingReports(false);
    }
  }

  async function refreshReports() {
    await loadInitialData();
    if (activeRegister) {
      await loadReports(activeRegister);
    }
  }

  const societyTotals = useMemo(() => {
    if (!societySummary) return [];
    return [
      {
        label: "Total Registered Members",
        total: societySummary.totalRegisteredMembers,
      },
      ...societySummary.items.map((item) => ({
        label: item.categoryName,
        total: item.total,
      })),
    ];
  }, [societySummary]);

  const circuitTotals = useMemo(() => {
    if (!circuitSummary) return [];
    return [
      {
        label: "Total Registered Members",
        total: circuitSummary.totalRegisteredMembers,
      },
      ...circuitSummary.items.map((item) => ({
        label: item.categoryName,
        total: item.circuitTotal,
      })),
    ];
  }, [circuitSummary]);

  if (loading) {
    return <LoadingState message="Loading reports..." />;
  }

  return (
    <div className="space-y-6">
      <ErrorAlert message={error} />

      <section className="content-card">
        <div className="section-header">
          <div>
            <h3 className="section-title">Reports Centre</h3>
            <p className="section-subtitle">
              Generate society and circuit summaries by year and society
            </p>
          </div>
        </div>

        <div className="register-toolbar">
          <div className="register-filter">
            <label htmlFor="reportsYear">Statistical Year</label>
            <div className="register-select-wrap">
              <CalendarRange className="register-select-icon" size={18} />
              <select
                id="reportsYear"
                value={selectedYear}
                onChange={(e) => setSelectedYear(e.target.value)}
              >
                {years.map((year) => (
                  <option key={year.id} value={String(year.id)}>
                    {year.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          <div className="register-filter">
            <label htmlFor="reportsSociety">Society</label>
            <div className="register-select-wrap">
              <Church className="register-select-icon" size={18} />
              <select
                id="reportsSociety"
                value={selectedSociety}
                onChange={(e) => setSelectedSociety(e.target.value)}
              >
                {societies.map((society) => (
                  <option key={society.id} value={String(society.id)}>
                    {society.name}
                  </option>
                ))}
              </select>
            </div>
          </div>

          <button className="btn" type="button" onClick={refreshReports}>
            <RefreshCw size={16} />
            <span>Refresh</span>
          </button>
        </div>
      </section>

      {!activeRegister && (
        <div className="register-warning">
          No report exists for the selected society and year, so reports cannot be
          generated yet.
        </div>
      )}

      {loadingReports && (
        <LoadingState message="Loading society and circuit reports..." />
      )}

      {activeRegister && !loadingReports && (
        <div className="reports-page-grid">
          <section className="content-card reports-summary-card">
            <div className="section-header">
              <div>
                <h3 className="section-title">
                  <span className="reports-title-with-icon">
                    <Building2 size={18} />
                    Society Summary
                  </span>
                </h3>
                <p className="section-subtitle">
                  {activeRegister.societyName} — {activeRegister.year}
                </p>
              </div>
            </div>

            <div className="reports-totals-grid">
              {societyTotals.map((item, index) => (
                <div key={item.label} className="reports-total-card">
                  <div className="reports-total-top">
                    <div className={`reports-total-icon ${index === 0 ? "purple" : "cyan"}`}>
                      <BarChart3 size={18} />
                    </div>
                    <span className="reports-total-value">{item.total}</span>
                  </div>
                  <div className="reports-total-label">{item.label}</div>
                </div>
              ))}
            </div>
          </section>

          <section className="content-card reports-summary-card">
            <div className="section-header">
              <div>
                <h3 className="section-title">
                  <span className="reports-title-with-icon">
                    <Network size={18} />
                    Circuit Summary
                  </span>
                </h3>
                <p className="section-subtitle">
                  {circuitSummary?.circuitName || "Circuit"} —{" "}
                  {circuitSummary?.year || activeRegister.year}
                </p>
              </div>
            </div>

            <div className="reports-table-shell">
              <div className="reports-table">
                <div className="reports-table-head">
                  <div>Category</div>
                  <div>Total</div>
                </div>

                <div className="reports-table-body">
                  {circuitTotals.map((item, index) => (
                    <div key={item.label} className="reports-table-row">
                      <div className="reports-category-cell">
                        <span className={`reports-row-dot ${index === 0 ? "primary" : ""}`} />
                        <span>{item.label}</span>
                      </div>
                      <div className="reports-total-pill">{item.total}</div>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </section>
        </div>
      )}
    </div>
  );
}