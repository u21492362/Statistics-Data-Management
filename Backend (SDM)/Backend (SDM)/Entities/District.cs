using Microsoft.AspNetCore.Components.Server.Circuits;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Entities
{
    public class District : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Code { get; set; }
        public virtual ICollection<Circuit> Circuits { get; set; } = new List<Circuit>();
        public virtual ICollection<StatisticalRegister> StatisticalRegisters { get; set; } = new List<StatisticalRegister>();
    }
}
