import { apiGet, apiPost } from "./api";
import type { CreateMemberRequest, MemberDto } from "../types/member";

export const memberService = {
  getMembers: (societyId: number) =>
    apiGet<MemberDto[]>(`/members?societyId=${societyId}`),

  createMember: (payload: CreateMemberRequest) =>
    apiPost<{ id: number }>("/members", payload),
};