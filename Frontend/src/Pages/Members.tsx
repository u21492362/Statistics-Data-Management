import { useEffect, useMemo, useState } from "react";
import {
  Search,
  Users,
  UserPlus,
  Building2,
  BadgeInfo,
  CalendarDays,
} from "lucide-react";
import type { DropdownItem } from "../types/lookup";
import type { MemberDto } from "../types/member";
import { lookupService } from "../services/lookupService";
import { memberService } from "../services/memberService";
import { ErrorAlert } from "../Components/common/ErrorAlert";
import { MembersTable } from "../Components/members/MembersTable";

export default function MembersPage() {
  const [societies, setSocieties] = useState<DropdownItem[]>([]);
  const [selectedSociety, setSelectedSociety] = useState("");
  const [members, setMembers] = useState<MemberDto[]>([]);
  const [search, setSearch] = useState("");
  const [error, setError] = useState("");
  const [submitting, setSubmitting] = useState(false);

  const [form, setForm] = useState({
    membershipNumber: "",
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    gender: "Male",
    societyId: "",
  });

  useEffect(() => {
    lookupService
      .getSocieties()
      .then((data) => {
        setSocieties(data);
        if (data[0]) {
          const firstSocietyId = String(data[0].id);
          setSelectedSociety(firstSocietyId);
          setForm((prev) => ({ ...prev, societyId: firstSocietyId }));
        }
      })
      .catch((e) => setError(e.message || "Failed to load societies"));
  }, []);

  useEffect(() => {
    if (!selectedSociety) return;

    memberService
      .getMembers(Number(selectedSociety))
      .then(setMembers)
      .catch((e) => setError(e.message || "Failed to load members"));
  }, [selectedSociety]);

  const filtered = useMemo(
    () =>
      members.filter(
        (m) =>
          !search ||
          m.fullName.toLowerCase().includes(search.toLowerCase()) ||
          m.membershipNumber.toLowerCase().includes(search.toLowerCase())
      ),
    [members, search]
  );

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();

    try {
      setSubmitting(true);
      setError("");

      await memberService.createMember({
        membershipNumber: form.membershipNumber,
        firstName: form.firstName,
        lastName: form.lastName,
        fullName: `${form.firstName} ${form.lastName}`.trim(),
        dateOfBirth: form.dateOfBirth,
        gender: form.gender,
        societyId: Number(form.societyId),
        isActive: true,
      });

      const refreshed = await memberService.getMembers(Number(form.societyId));
      setMembers(refreshed);

      setForm({
        membershipNumber: "",
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        gender: "Male",
        societyId: selectedSociety,
      });
    } catch (e) {
      setError((e as Error).message);
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <div className="members-page-grid">
      <section className="content-card members-list-card">
        <div className="section-header">
          <div>
            <h3 className="section-title">Society Members</h3>
            <p className="section-subtitle">
              View, search and manage registered members
            </p>
          </div>

          <div className="members-stat-pill">
            <Users size={16} />
            <span>{filtered.length} member(s)</span>
          </div>
        </div>

        <ErrorAlert message={error} />

        <div className="members-toolbar">
          <div className="members-search">
            <Search className="members-search-icon" size={16} />
            <input
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              placeholder="Search by member name or number"
              className="members-search-input"
            />
          </div>

          <div className="members-filter">
            <label htmlFor="societyFilter">Society</label>
            <select
              id="societyFilter"
              value={selectedSociety}
              onChange={(e) => {
                setSelectedSociety(e.target.value);
                setForm((prev) => ({ ...prev, societyId: e.target.value }));
              }}
            >
              {societies.map((society) => (
                <option key={society.id} value={society.id}>
                  {society.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="members-table-shell">
          <MembersTable members={filtered} />
        </div>
      </section>

      <section className="form-neon members-form-card">
        <h2>
          <UserPlus size={20} />
          <span>Add Member</span>
        </h2>

        <form onSubmit={onSubmit}>
          <div className="form-group">
            <input
              type="text"
              id="membershipNumber"
              placeholder=" "
              value={form.membershipNumber}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  membershipNumber: e.target.value,
                }))
              }
              required
            />
            <label htmlFor="membershipNumber">
              Membership Number
            </label>
          </div>

          <div className="form-group">
            <input
              type="text"
              id="firstName"
              placeholder=" "
              value={form.firstName}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  firstName: e.target.value,
                }))
              }
              required
            />
            <label htmlFor="firstName">First Name</label>
          </div>

          <div className="form-group">
            <input
              type="text"
              id="lastName"
              placeholder=" "
              value={form.lastName}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  lastName: e.target.value,
                }))
              }
              required
            />
            <label htmlFor="lastName">Last Name</label>
          </div>

          <div className="form-group form-select-group">
            <Building2 className="form-leading-icon" size={18} />
            <select
              value={form.societyId}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  societyId: e.target.value,
                }))
              }
              required
            >
              <option value="">Select society</option>
              {societies.map((society) => (
                <option key={society.id} value={society.id}>
                  {society.name}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group form-select-group">
            <BadgeInfo className="form-leading-icon" size={18} />
            <select
              value={form.gender}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  gender: e.target.value,
                }))
              }
              required
            >
              <option value="Male">Male</option>
              <option value="Female">Female</option>
            </select>
          </div>

          <div className="form-group form-date-group">
            <CalendarDays className="form-leading-icon" size={18} />
            <input
              type="date"
              id="dateOfBirth"
              value={form.dateOfBirth}
              onChange={(e) =>
                setForm((prev) => ({
                  ...prev,
                  dateOfBirth: e.target.value,
                }))
              }
              required
            />
          </div>

          <button type="submit" className="btn-neon" disabled={submitting}>
            {submitting ? "Saving Member..." : "Save Member"}
          </button>
        </form>
      </section>
    </div>
  );
}