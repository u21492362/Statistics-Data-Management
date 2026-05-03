import type { DropdownItem } from "./lookup";

export type RegisterCategoryDto = {
  id: number;
  statisticalCategoryId: number;
  categoryCode: string;
  categoryName: string;
  selected: boolean;
  valueNumber?: number | null;
  valueText?: string | null;
  isSystemGenerated?: boolean;
};

export type RegisterEntryDto = {
  id: number;
  statisticalRegisterId: number;
  memberId: number;
  rowNumber: number;
  membershipNumber: string;
  memberFullName: string;
  dateOfBirth: string;
  gender: string;
  age: number;
  remarks?: string | null;
  categories: RegisterCategoryDto[];
};

export type RegisterDto = {
  id: number;
  statisticalYearId: number;
  year: number;
  districtId: number;
  districtName?: string;
  circuitId: number;
  circuitName?: string;
  societyId: number;
  societyName?: string;
  compiledByUserId: number;
  compiledByUserName?: string;
  dateCompiled: string;
  status: string | number;
  notes?: string | null;
};

export type RegisterCaptureDto = {
  register: RegisterDto;
  entries: RegisterEntryDto[];
  availableMembers: DropdownItem[];
  availableCategories: DropdownItem[];
};