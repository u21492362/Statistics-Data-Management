import { useEffect, useMemo, useState } from "react";
import {
  ArrowUpRight,
  BarChart3,
  Building2,
  FileBarChart2,
  Users,
} from "lucide-react";
import type { DashboardDto } from "../types/dashboard";
import { dashboardService } from "../services/dashboardService";
import { LoadingState } from "../Components/common/LoadingState";
import { ErrorAlert } from "../Components/common/ErrorAlert";

type StatCardProps = {
  title: string;
  value: number;
  icon: React.ElementType;
  tone: "purple" | "cyan" | "pink" | "green";
  note: string;
};

function StatCard({ title, value, icon: Icon, tone, note }: StatCardProps) {
  return (
    <div className="stat-card">
      <div className="stat-header">
        <div>
          <div className="stat-value">{value.toLocaleString()}</div>
          <div className="stat-label">{title}</div>
        </div>
        <div className={`stat-icon ${tone}`}>
          <Icon size={22} />
        </div>
      </div>
      <div className="stat-change positive">
        <ArrowUpRight size={16} />
        <span>{note}</span>
      </div>
    </div>
  );
}

export default function DashboardPage() {
  const [data, setData] = useState<DashboardDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    dashboardService
      .getDashboard()
      .then(setData)
      .catch((e) => setError(e.message || "Failed to load dashboard"))
      .finally(() => setLoading(false));
  }, []);

  const stats = useMemo(
    () => [
      {
        title: "Total Members",
        value: data?.totalMembers ?? 0,
        icon: Users,
        tone: "purple" as const,
        note: "Live membership total",
      },
      {
        title: "Societies",
        value: data?.totalSocieties ?? 0,
        icon: Building2,
        tone: "cyan" as const,
        note: "Active societies tracked",
      },
      {
        title: "Registers",
        value: data?.totalRegisters ?? 0,
        icon: FileBarChart2,
        tone: "pink" as const,
        note: "Captured register records",
      },
      {
        title: "Draft Registers",
        value: data?.draftRegisters ?? 0,
        icon: BarChart3,
        tone: "green" as const,
        note: "Pending completion",
      },
    ],
    [data]
  );

  if (loading) {
    return <LoadingState message="Loading dashboard..." />;
  }

  return (
    <div className="space-y-6">
      <ErrorAlert message={error} />

      <section className="chart-section">
        <div className="chart-header">
          <div>
            <h2 className="chart-title">Welcome to the dashboard</h2>
            <p className="chart-subtitle">
              Monitor membership activity, societies, registers and reporting progress.
            </p>
          </div>
        </div>
      </section>

      <section className="stats-grid">
        {stats.map((stat) => (
          <StatCard key={stat.title} {...stat} />
        ))}
      </section>

      <section className="content-grid">
        <div className="content-card">
          <div className="section-header">
            <div>
              <h3 className="section-title">Dashboard Summary</h3>
              <p className="section-subtitle">Current operational overview</p>
            </div>
          </div>

          <ul className="transaction-list">
            <li className="transaction-item">
              <div className="transaction-info">
                <div className="transaction-icon receive">
                  <Users size={18} />
                </div>
                <div className="transaction-details">
                  <h4>Members Captured</h4>
                  <p>Total recorded members in the system</p>
                </div>
              </div>
              <div className="transaction-amount">
                <span className="value positive">
                  {(data?.totalMembers ?? 0).toLocaleString()}
                </span>
                <span className="time">Updated from API</span>
              </div>
            </li>

            <li className="transaction-item">
              <div className="transaction-info">
                <div className="transaction-icon swap">
                  <Building2 size={18} />
                </div>
                <div className="transaction-details">
                  <h4>Societies</h4>
                  <p>Configured societies across the circuit</p>
                </div>
              </div>
              <div className="transaction-amount">
                <span className="value">
                  {(data?.totalSocieties ?? 0).toLocaleString()}
                </span>
                <span className="time">Current count</span>
              </div>
            </li>

            <li className="transaction-item">
              <div className="transaction-info">
                <div className="transaction-icon news">
                  <FileBarChart2 size={18} />
                </div>
                <div className="transaction-details">
                  <h4>Registers Submitted</h4>
                  <p>Total submitted and saved registers</p>
                </div>
              </div>
              <div className="transaction-amount">
                <span className="value">
                  {(data?.totalRegisters ?? 0).toLocaleString()}
                </span>
                <span className="time">Across all periods</span>
              </div>
            </li>
          </ul>
        </div>

        <div className="content-card">
          <div className="section-header">
            <div>
              <h3 className="section-title">Quick Insights</h3>
              <p className="section-subtitle">Useful status indicators</p>
            </div>
          </div>

          <div className="space-y-4">
            <div className="stat-card">
              <div className="stat-label">Draft Completion Pressure</div>
              <div className="stat-value">{data?.draftRegisters ?? 0}</div>
              <div className="stat-change negative">Drafts still need review and submission.</div>
            </div>

            <div className="stat-card">
              <div className="stat-label">Data Health</div>
              <div className="stat-value">{(data?.totalRegisters ?? 0) > 0 ? "Good" : "Starter"}</div>
              <div className="stat-change positive">
                Dashboard is connected and returning live figures.
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}