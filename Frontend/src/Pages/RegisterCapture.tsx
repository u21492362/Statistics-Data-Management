import { useEffect, useMemo, useState } from "react";
import {
  Save,
  RefreshCw,
  CalendarRange,
  Church,
  UserPlus,
  Tags,
  Users,
} from "lucide-react";
import { ErrorAlert } from "../Components/common/ErrorAlert";
import type { DropdownItem } from "../types/lookup";
import type { MemberDto } from "../types/member";
import { lookupService } from "../services/lookupService";
import { registerService } from "../services/registerService";
import { memberService } from "../services/memberService";
import { LoadingState } from "../Components/common/LoadingState";
import type {
  RegisterCaptureDto,
  RegisterCategoryDto,
  RegisterDto,
} from "../types/register";
import { demographicBucket } from "../utils/demographics";

export default function RegisterCapturePage() {
  const [societies, setSocieties] = useState<DropdownItem[]>([]);
  const [years, setYears] = useState<DropdownItem[]>([]);
  const [registers, setRegisters] = useState<RegisterDto[]>([]);
  const [members, setMembers] = useState<MemberDto[]>([]);
  const [capture, setCapture] = useState<RegisterCaptureDto | null>(null);

  const [selectedSociety, setSelectedSociety] = useState("");
  const [selectedYear, setSelectedYear] = useState("");
  const [newEntryMemberId, setNewEntryMemberId] = useState("");
  const [selectedCategories, setSelectedCategories] = useState<string[]>([]);

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
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

  const currentYear = useMemo(() => {
    if (activeRegister?.year) return activeRegister.year;
    const item = years.find((y) => y.id === currentYearId);
    return item ? Number(item.name) : 0;
  }, [activeRegister, years, currentYearId]);

  useEffect(() => {
    loadInitialData();
  }, []);

  useEffect(() => {
    if (currentSocietyId > 0) {
      loadMembers(currentSocietyId);
    }
  }, [currentSocietyId]);

  useEffect(() => {
    if (activeRegister) {
      loadCapture(activeRegister.id);
    } else {
      setCapture(null);
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

  async function loadMembers(societyId: number) {
    try {
      const data = await memberService.getMembers(societyId);
      setMembers(data);
    } catch (e) {
      setError((e as Error).message);
    }
  }

  async function loadCapture(registerId: number) {
    try {
      const data = await registerService.getCapture(registerId);
      setCapture(data);
    } catch (e) {
      setError((e as Error).message);
    }
  }

  async function refreshPage() {
    await loadInitialData();
    if (currentSocietyId > 0) {
      await loadMembers(currentSocietyId);
    }
    if (activeRegister) {
      await loadCapture(activeRegister.id);
    }
  }

  function toggleCategory(categoryName: string, checked: boolean) {
    setSelectedCategories((prev) =>
      checked ? [...prev, categoryName] : prev.filter((x) => x !== categoryName)
    );
  }

  async function addRegisterEntry() {
    if (!activeRegister || !newEntryMemberId) return;

    try {
      setSaving(true);
      setError("");

      const addResult = await registerService.addEntry(
        activeRegister.id,
        Number(newEntryMemberId)
      );

      const refreshedCapture = await registerService.getCapture(activeRegister.id);
      const entry = refreshedCapture.entries.find((x) => x.id === addResult.entryId);

      if (entry) {
        const payload: RegisterCategoryDto[] = entry.categories.map((item) => ({
          ...item,
          selected: item.isSystemGenerated
            ? item.selected
            : selectedCategories.includes(item.categoryName),
        }));

        await registerService.saveEntryCategories(entry.id, payload);
      }

      const finalCapture = await registerService.getCapture(activeRegister.id);
      setCapture(finalCapture);
      setNewEntryMemberId("");
      setSelectedCategories([]);
    } catch (e) {
      setError((e as Error).message);
    } finally {
      setSaving(false);
    }
  }

  const selectableMembers = useMemo(() => {
    if (!capture) return members;
    const existingIds = new Set(capture.entries.map((x) => x.memberId));
    return members.filter((m) => !existingIds.has(m.id));
  }, [members, capture]);

  if (loading) {
    return <LoadingState message="Loading register capture..." />;
  }

  return (
    <div className="space-y-6">
      <ErrorAlert message={error} />

      <section className="content-card">
        <div className="section-header">
          <div>
            <h3 className="section-title">Register Capture</h3>
            <p className="section-subtitle">
              Capture annual register entries by society and year
            </p>
          </div>
        </div>

        <div className="register-toolbar">
          <div className="register-filter">
            <label htmlFor="registerYear">Statistical Year</label>
            <div className="register-select-wrap">
              <CalendarRange className="register-select-icon" size={18} />
              <select
                id="registerYear"
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
            <label htmlFor="registerSociety">Society</label>
            <div className="register-select-wrap">
              <Church className="register-select-icon" size={18} />
              <select
                id="registerSociety"
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

          <button className="btn" type="button" onClick={refreshPage}>
            <RefreshCw size={16} />
            <span>Refresh</span>
          </button>
        </div>
      </section>

      {!activeRegister && (
        <div className="register-warning">
          No register exists for the selected society and year. Create the annual
          register first from the backend or your register setup screen.
        </div>
      )}

      {activeRegister && (
        <div className="register-page-grid">
          <section className="form-neon register-form-card">
            <h2>
              <UserPlus size={20} />
              <span>Add Register Entry</span>
            </h2>

            <div className="register-meta-card">
              <div>
                <span className="register-meta-label">Society:</span>{" "}
                {activeRegister.societyName}
              </div>
              <div>
                <span className="register-meta-label">Year:</span>{" "}
                {activeRegister.year}
              </div>
              <div>
                <span className="register-meta-label">Status:</span>{" "}
                {String(activeRegister.status)}
              </div>
            </div>

            <div className="form-group form-select-group">
              <Users className="form-leading-icon" size={18} />
              <select
                value={newEntryMemberId}
                onChange={(e) => setNewEntryMemberId(e.target.value)}
              >
                <option value="">Select a member</option>
                {selectableMembers.map((member) => (
                  <option key={member.id} value={String(member.id)}>
                    {member.fullName} ({member.membershipNumber})
                  </option>
                ))}
              </select>
            </div>

            <div className="register-category-block">
              <div className="register-category-title">
                <Tags size={16} />
                <span>Manual Categories</span>
              </div>

              <div className="register-category-list">
                {(capture?.availableCategories || []).map((category) => {
                  const checked = selectedCategories.includes(category.name);

                  return (
                    <label key={category.id} className="register-category-item">
                      <input
                        type="checkbox"
                        checked={checked}
                        onChange={(e) =>
                          toggleCategory(category.name, e.target.checked)
                        }
                      />
                      <span>{category.name}</span>
                    </label>
                  );
                })}
              </div>
            </div>

            <button
              className="btn-neon"
              type="button"
              onClick={addRegisterEntry}
              disabled={!newEntryMemberId || saving}
            >
              <Save size={16} />
              <span>{saving ? "Saving..." : "Save Register Entry"}</span>
            </button>
          </section>

          <section className="content-card register-entries-card">
            <div className="section-header">
              <div>
                <h3 className="section-title">Current Register Entries</h3>
                <p className="section-subtitle">
                  Members already captured in this register
                </p>
              </div>
            </div>

            {(capture?.entries || []).length === 0 ? (
              <div className="register-empty-state">
                No entries captured yet.
              </div>
            ) : (
              <div className="register-entry-list">
                {capture?.entries.map((entry) => (
                  <div key={entry.id} className="register-entry-card">
                    <div className="register-entry-head">
                      <div>
                        <div className="register-entry-name">
                          {entry.memberFullName}
                        </div>
                        <div className="register-entry-number">
                          {entry.membershipNumber}
                        </div>
                      </div>

                      <span className="register-demographic-badge">
                        {demographicBucket(
                          entry.gender,
                          entry.dateOfBirth,
                          currentYear
                        )}
                      </span>
                    </div>

                    <div className="register-badge-row">
                      {entry.categories
                        .filter((cat) => cat.selected)
                        .map((cat) => (
                          <span
                            key={`${entry.id}-${cat.statisticalCategoryId}`}
                            className="register-category-badge"
                          >
                            {cat.categoryName}
                          </span>
                        ))}
                    </div>
                  </div>
                ))}
              </div>
            )}
          </section>
        </div>
      )}
    </div>
  );
}