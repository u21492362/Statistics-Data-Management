import type { MemberDto } from "../../types/member";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table";


export function MembersTable({ members }: { members: MemberDto[] }) {
  return (
    <div className="overflow-hidden rounded-2xl border bg-white">
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Member No.</TableHead>
            <TableHead>Name</TableHead>
            <TableHead>DOB</TableHead>
            <TableHead>Gender</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {members.map((member) => (
            <TableRow key={member.id}>
              <TableCell>{member.membershipNumber}</TableCell>
              <TableCell>{member.fullName}</TableCell>
              <TableCell>{member.dateOfBirth || member.dob}</TableCell>
              <TableCell>{member.gender}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}