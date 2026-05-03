using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class SocietyViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Society Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Society Code")]
        public string? Code { get; set; }

        [Required]
        [Display(Name = "Circuit")]
        public int CircuitId { get; set; }

        public string? CircuitName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
