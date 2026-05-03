using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class StatisticalRegisterViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int StatisticalYearId { get; set; }

        public int Year { get; set; }

        [Required]
        [Display(Name = "District")]
        public int DistrictId { get; set; }

        public string? DistrictName { get; set; }

        [Required]
        [Display(Name = "Circuit")]
        public int CircuitId { get; set; }

        public string? CircuitName { get; set; }

        [Required]
        [Display(Name = "Society")]
        public int SocietyId { get; set; }

        public string? SocietyName { get; set; }

        [Required]
        [Display(Name = "Compiled By")]
        public int CompiledByUserId { get; set; }

        public string? CompiledByUserName { get; set; }

        [Display(Name = "Date Compiled")]
        public DateTime DateCompiled { get; set; }

        public RegisterStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}
