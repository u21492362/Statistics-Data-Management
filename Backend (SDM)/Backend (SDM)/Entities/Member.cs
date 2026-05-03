using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class Member : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string MembershipNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [StringLength(20)]
        public string? MobileNumber { get; set; }

        [StringLength(250)]
        public string? PhysicalAddress { get; set; }

        [ForeignKey(nameof(Society))]
        public int SocietyId { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Society? Society { get; set; }
        public virtual ICollection<RegisterMemberEntry> RegisterEntries { get; set; } = new List<RegisterMemberEntry>();

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }
    }
}
