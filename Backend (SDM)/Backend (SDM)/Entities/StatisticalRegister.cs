using Backend__SDM_.Models.Enums;
using Microsoft.AspNetCore.Components.Server.Circuits;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class StatisticalRegister : BaseEntity
    {
        [ForeignKey(nameof(StatisticalYear))]
        public int StatisticalYearId { get; set; }

        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        [ForeignKey(nameof(Circuit))]
        public int CircuitId { get; set; }

        [ForeignKey(nameof(Society))]
        public int SocietyId { get; set; }

        [ForeignKey(nameof(CompiledByUser))]
        public int CompiledByUserId { get; set; }

        public DateTime DateCompiled { get; set; } = DateTime.UtcNow;

        public RegisterStatus Status { get; set; } = RegisterStatus.Draft;

        [StringLength(1000)]
        public string? Notes { get; set; }

        public virtual StatisticalYear? StatisticalYear { get; set; }
        public virtual District? District { get; set; }
        public virtual Circuit? Circuit { get; set; }
        public virtual Society? Society { get; set; }
        public virtual AppUser? CompiledByUser { get; set; }

        public virtual ICollection<RegisterMemberEntry> RegisterMemberEntries { get; set; } = new List<RegisterMemberEntry>();
    }
}
