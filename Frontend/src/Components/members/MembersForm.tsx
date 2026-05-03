
import type { DropdownItem } from "../../types/lookup";
import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { Label } from "../ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../ui/select";

type Props = {
  societies: DropdownItem[];
  value: {
    membershipNumber: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    gender: string;
    societyId: string;
  };
  onChange: (value: Props["value"]) => void;
  onSubmit: () => void;
};

export function MemberForm({ societies, value, onChange, onSubmit }: Props) {
  return (
    <div className="space-y-4">
      <div className="space-y-2">
        <Label>Membership Number</Label>
        <Input
          value={value.membershipNumber}
          onChange={(e) => onChange({ ...value, membershipNumber: e.target.value })}
        />
      </div>
      <div className="space-y-2">
        <Label>First Name</Label>
        <Input
          value={value.firstName}
          onChange={(e) => onChange({ ...value, firstName: e.target.value })}
        />
      </div>
      <div className="space-y-2">
        <Label>Last Name</Label>
        <Input
          value={value.lastName}
          onChange={(e) => onChange({ ...value, lastName: e.target.value })}
        />
      </div>
      <div className="space-y-2">
        <Label>Date of Birth</Label>
        <Input
          type="date"
          value={value.dateOfBirth}
          onChange={(e) => onChange({ ...value, dateOfBirth: e.target.value })}
        />
      </div>
      <div className="space-y-2">
        <Label>Gender</Label>
        <Select value={value.gender} onValueChange={(gender) => onChange({ ...value, gender })}>
          <SelectTrigger>
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="Male">Male</SelectItem>
            <SelectItem value="Female">Female</SelectItem>
          </SelectContent>
        </Select>
      </div>
      <div className="space-y-2">
        <Label>Society</Label>
        <Select value={value.societyId} onValueChange={(societyId) => onChange({ ...value, societyId })}>
          <SelectTrigger>
            <SelectValue />
          </SelectTrigger>
          <SelectContent>
            {societies.map((society) => (
              <SelectItem key={society.id} value={String(society.id)}>
                {society.name}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>
      <Button className="w-full rounded-2xl" onClick={onSubmit}>
        Add Member
      </Button>
    </div>
  );
}