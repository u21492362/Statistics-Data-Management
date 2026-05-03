import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../App";
import LoginPage from "../Pages/LoginPage";
import RegisterPage from "../Pages/RegisterPage";
import DashboardPage from "../Pages/Dashboard"; 
import MembersPage from "../Pages/Members";
import RegisterCapturePage from "../Pages/RegisterCapture";
import ProtectedRoute from "../Components/ProtectedRoute";
import ReportsPage from "../Pages/Reports";

function RouteError() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-black text-white">
      <div className="rounded-2xl border border-white/10 bg-white/5 p-8 text-center">
        <h1 className="text-2xl font-bold">Page not found</h1>
        <p className="mt-2 text-slate-300">
          The page you requested does not exist.
        </p>
      </div>
    </div>
  );
}

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Navigate to="/app" replace />,
    errorElement: <RouteError />,
  },
  {
    path: "/login",
    element: <LoginPage />,
    errorElement: <RouteError />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
    errorElement: <RouteError />,
  },
  {
    path: "/app",
    element: (
      <ProtectedRoute>
        <App />
      </ProtectedRoute>
    ),
    errorElement: <RouteError />,
    children: [
      { index: true, element: <DashboardPage /> },
      { path: "members", element: <MembersPage /> },
      { path: "registers", element: <RegisterCapturePage /> },
      { path: "reports", element: <ReportsPage /> },
    ],
  },
  {
    path: "*",
    element: <Navigate to="/login" replace />,
  },
]);