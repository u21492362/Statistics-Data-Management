import { apiGet } from "./api";
import type { DashboardDto } from "../types/dashboard";

export const dashboardService = {
  getDashboard: () => apiGet<DashboardDto>("/reports/dashboard"),
};