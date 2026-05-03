using Backend__SDM_.Models.Enums;
using Microsoft.AspNetCore.Components.Server.Circuits;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class AppUser : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        [ForeignKey(nameof(Circuit))]
        public int? CircuitId { get; set; }

        [ForeignKey(nameof(Society))]
        public int? SocietyId { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Circuit? Circuit { get; set; }
        public virtual Society? Society { get; set; }
    }
}
