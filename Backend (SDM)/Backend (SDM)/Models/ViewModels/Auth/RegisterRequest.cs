using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public int? CircuitId { get; set; }
        public int? SocietyId { get; set; }
    }
}