using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class AppUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

        [Display(Name = "Circuit")]
        public int? CircuitId { get; set; }

        [Display(Name = "Society")]
        public int? SocietyId { get; set; }

        public string? CircuitName { get; set; }
        public string? SocietyName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
