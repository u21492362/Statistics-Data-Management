export type SummaryItemDto = {
  categoryCode: string;
  categoryName: string;
  total: number;
};

export type SocietySummaryDto = {
  statisticalYearId: number;
  year: number;
  districtId: number;
  districtName: string;
  circuitId: number;
  circuitName: string;
  societyId: number;
  societyName: string;
  totalRegisteredMembers: number;
  items: SummaryItemDto[];
};

export type CircuitSummaryDto = {
  statisticalYearId: number;
  year: number;
  districtId: number;
  districtName: string;
  circuitId: number;
  circuitName: string;
  totalSocieties: number;
  totalRegisteredMembers: number;
  items: Array<{
    categoryCode: string;
    categoryName: string;
    circuitTotal: number;
  }>;
};