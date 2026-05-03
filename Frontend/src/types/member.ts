export type MemberDto = {
  id: number;
  membershipNumber: string;
  firstName?: string;
  lastName?: string;
  fullName: string;
  dateOfBirth?: string;
  dob?: string;
  gender: string;
  societyId: number;
  societyName?: string;
  isActive?: boolean;
};

export type CreateMemberRequest = {
  membershipNumber: string;
  firstName: string;
  lastName: string;
  fullName: string;
  dateOfBirth: string;
  gender: string;
  societyId: number;
  isActive: boolean;
};