using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Entities
{
    public class StatisticalCategory : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        public CategoryGroup CategoryGroup { get; set; }
        public bool IsBoolean { get; set; } = true;
        public bool IsSystemGenerated { get; set; } = false;
        public int DisplayOrder { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public virtual ICollection<RegisterMemberCategory> RegisterMemberCategories { get; set; } = new List<RegisterMemberCategory>();
    }
}
