using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Membership Number")]
        public string MembershipNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        [Display(Name = "Physical Address")]
        public string? PhysicalAddress { get; set; }

        [Required]
        [Display(Name = "Society")]
        public int SocietyId { get; set; }

        public string? SocietyName { get; set; }

        public bool IsActive { get; set; } = true;

        public int Age { get; set; }
    }
}
