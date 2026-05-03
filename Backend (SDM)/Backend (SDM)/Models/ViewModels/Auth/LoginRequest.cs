using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}