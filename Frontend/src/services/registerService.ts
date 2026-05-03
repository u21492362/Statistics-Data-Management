import { apiGet, apiPost, apiPut } from "./api";
import type { RegisterCaptureDto, RegisterCategoryDto, RegisterDto } from "../types/register";

export const registerService = {
  getRegisters: () => apiGet<RegisterDto[]>("/registers"),

  getCapture: (registerId: number) =>
    apiGet<RegisterCaptureDto>(`/registers/${registerId}/capture`),

  addEntry: (registerId: number, memberId: number) =>
    apiPost<{ entryId: number }>(`/registers/${registerId}/entries`, {
      memberId,
      remarks: null,
    }),

  saveEntryCategories: (entryId: number, payload: RegisterCategoryDto[]) =>
    apiPut(`/registers/entries/${entryId}/categories`, payload),
};