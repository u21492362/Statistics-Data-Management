import { apiGet } from "./api";
import type { DropdownItem } from "../types/lookup";

export const lookupService = {
  getSocieties: () => apiGet<DropdownItem[]>("/lookups/societies"),
  getYears: () => apiGet<DropdownItem[]>("/lookups/years"),
  getCategories: () => apiGet<DropdownItem[]>("/lookups/categories"),
};