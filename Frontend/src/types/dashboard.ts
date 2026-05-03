import type { SummaryItemDto } from "./report";

export type DashboardDto = {
  totalMembers: number;
  totalSocieties: number;
  totalRegisters: number;
  draftRegisters: number;
  submittedRegisters: number;
  approvedRegisters: number;
  finalisedRegisters: number;
  topCategories: SummaryItemDto[];
};