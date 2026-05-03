using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class StatisticalYearViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(1000, 3000)]
        public int Year { get; set; }

        public bool IsOpen { get; set; } = true;
        public bool IsClosed { get; set; } = false;
    }
}
