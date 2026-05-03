import { apiGet } from "./api";
import type { CircuitSummaryDto, SocietySummaryDto } from "../types/report";

export const reportService = {
  getSocietySummary: (yearId: number, societyId: number) =>
    apiGet<SocietySummaryDto>(
      `/reports/society-summary?yearId=${yearId}&societyId=${societyId}`
    ),

  getCircuitSummary: (yearId: number, circuitId: number) =>
    apiGet<CircuitSummaryDto>(
      `/reports/circuit-summary?yearId=${yearId}&circuitId=${circuitId}`
    ),
};