using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class DistrictViewModelcs
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "District Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "District Code")]
        public string? Code { get; set; }
    }
}
