using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class RegisterMemberCategory : BaseEntity
    {
        [ForeignKey(nameof(RegisterMemberEntry))]
        public int RegisterMemberEntryId { get; set; }

        [ForeignKey(nameof(StatisticalCategory))]
        public int StatisticalCategoryId { get; set; }

        public bool ValueBool { get; set; } = true;

        public int? ValueNumber { get; set; }

        [StringLength(250)]
        public string? ValueText { get; set; }

        public virtual RegisterMemberEntry? RegisterMemberEntry { get; set; }
        public virtual StatisticalCategory? StatisticalCategory { get; set; }
    }
}
