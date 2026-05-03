import { useState } from "react";
import { NavLink, Outlet, useNavigate } from "react-router-dom";
import { authService } from "./services/authService";
import {
  BarChart3,
  Building2,
  FileBarChart2,
  LayoutDashboard,
  LogOut,
  Menu,
  Users,
  X,
} from "lucide-react";

const navItems = [
  { to: "/app", label: "Dashboard", icon: LayoutDashboard },
  { to: "/app/members", label: "Members", icon: Users },
  { to: "/app/registers", label: "Registers", icon: FileBarChart2 },
  { to: "/app/reports", label: "Reports", icon: BarChart3 },
];

function NavItems({ onNavigate }: { onNavigate?: () => void }) {
  return (
    <nav>
      <ul className="nav-menu">
        {navItems.map(({ to, label, icon: Icon }) => (
          <li key={to} className="nav-item">
            <NavLink
              to={to}
              end={to === "/app"}
              onClick={onNavigate}
              className={({ isActive }) =>
                `nav-link ${isActive ? "active" : ""}`
              }
            >
              <Icon size={20} />
              <span>{label}</span>
            </NavLink>
          </li>
        ))}
      </ul>
    </nav>
  );
}

export default function App() {
  const [mobileOpen, setMobileOpen] = useState(false);
  const navigate = useNavigate();
  const currentUser = authService.getCurrentUser();

  function logout() {
    authService.logout();
    navigate("/login", { replace: true });
  }

  return (
    <div className="app-shell">
      <header className="mobile-nav">
        <div className="logo">
          <img src="/logo.jpg" alt="MCZ Logo" className="app-logo" />
          <div>
            <div className="logo-title">MCZ SDM</div>
          </div>
        </div>

        <button
          className={`mobile-menu-btn ${mobileOpen ? "active" : ""}`}
          onClick={() => setMobileOpen((prev) => !prev)}
          aria-label="Toggle menu"
          type="button"
        >
          {mobileOpen ? <X size={22} /> : <Menu size={22} />}
        </button>
      </header>

      <aside className="sidebar">
        <div className="logo">
          <img src="/logo.jpg" alt="MCZ Logo" className="app-logo" />
          <div>
            <div className="logo-title">Methodist SDM</div>
            <div className="logo-subtitle">Kuwadzana Circuit</div>
          </div>
        </div>

        <NavItems />

        <div className="sidebar-footer">
          <div className="user-profile">
            <div className="user-avatar">
              <Building2 size={20} />
            </div>
            <div>
              <div className="font-semibold">
                {currentUser?.fullName || "MCZ Admin"}
              </div>
              <div className="text-sm text-slate-400">
                Statistical Management
              </div>
            </div>
          </div>

          <button className="btn btn-outline logout-btn" onClick={logout}>
            <LogOut size={18} />
            <span>Logout</span>
          </button>
        </div>
      </aside>

      <aside className={`mobile-sidebar ${mobileOpen ? "active" : ""}`}>
        <div className="mb-6">
          <NavItems onNavigate={() => setMobileOpen(false)} />
        </div>

        <button
          className="btn btn-outline logout-btn"
          onClick={() => {
            setMobileOpen(false);
            logout();
          }}
        >
          <LogOut size={18} />
          <span>Logout</span>
        </button>
      </aside>

      <main className="main-content">
        <div className="header">
          <div>
            <h1>Methodist Statistical Data Management</h1>
            <p className="header-subtitle">
              Membership capture, consolidation and reporting dashboard.
            </p>
          </div>

          <div className="header-actions">
            <button className="btn" type="button">
              Circuit Overview
            </button>
            <button className="btn btn-primary" type="button">
              Generate Report
            </button>
          </div>
        </div>

        <Outlet />
      </main>
    </div>
  );
}