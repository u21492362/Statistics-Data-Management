export function calculateAge(dob: string, year: number): number {
  const birth = new Date(dob);
  const endOfYear = new Date(year, 11, 31);
  let age = endOfYear.getFullYear() - birth.getFullYear();
  const monthDiff = endOfYear.getMonth() - birth.getMonth();
  const dayDiff = endOfYear.getDate() - birth.getDate();

  if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)) {
    age--;
  }

  return age;
}

export function demographicBucket(gender: string, dob: string, year: number): string {
  const age = calculateAge(dob, year);

  if (gender === "Male" && age > 18) return "Men Above 18";
  if (gender === "Female" && age > 18) return "Women Above 18";
  if (gender === "Male" && age >= 6 && age <= 18) return "Boys 6–18";
  if (gender === "Female" && age >= 6 && age <= 18) return "Girls 6–18";
  if (gender === "Male" && age < 5) return "Infant Boys Under 5";
  if (gender === "Female" && age < 5) return "Infant Girls Under 5";

  return "Unclassified";
}