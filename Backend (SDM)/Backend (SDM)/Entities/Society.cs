using Microsoft.AspNetCore.Components.Server.Circuits;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class Society : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Code { get; set; }
        [ForeignKey(nameof(Circuit))]
        public int CircuitId { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Circuit? Circuit { get; set; }
        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
        public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public virtual ICollection<StatisticalRegister> StatisticalRegisters { get; set; } = new List<StatisticalRegister>();
    }
}
