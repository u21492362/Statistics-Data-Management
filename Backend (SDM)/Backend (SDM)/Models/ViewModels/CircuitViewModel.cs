using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class CircuitViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Circuit Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Circuit Code")]
        public string? Code { get; set; }

        [Required]
        [Display(Name = "District")]
        public int DistrictId { get; set; }

        public string? DistrictName { get; set; }
    }
}
