using Microsoft.AspNetCore.Components.Server.Circuits;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend__SDM_.Entities
{
    public class Circuit : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Code { get; set; }

        [ForeignKey(nameof(District))]
        public int DistrictId { get; set; }

        public virtual District? District { get; set; }

        public virtual ICollection<Society> Societies { get; set; } = new List<Society>();
        public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public virtual ICollection<StatisticalRegister> StatisticalRegisters { get; set; } = new List<StatisticalRegister>();
    }
}
