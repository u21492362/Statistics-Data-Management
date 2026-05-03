using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class StatisticalCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Code")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Category Group")]
        public CategoryGroup CategoryGroup { get; set; }

        public bool IsBoolean { get; set; } = true;
        public bool IsSystemGenerated { get; set; } = false;
        public int DisplayOrder { get; set; }
        public string? Description { get; set; }
    }
}
