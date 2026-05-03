using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class RegisterMemberEntry : BaseEntity
    {
        [ForeignKey(nameof(StatisticalRegister))]
        public int StatisticalRegisterId { get; set; }

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }

        public int RowNumber { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        public virtual StatisticalRegister? StatisticalRegister { get; set; }
        public virtual Member? Member { get; set; }

        public virtual ICollection<RegisterMemberCategory> Categories { get; set; } = new List<RegisterMemberCategory>();
    }
}
