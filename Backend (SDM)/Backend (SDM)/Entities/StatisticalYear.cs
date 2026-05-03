using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Entities
{
    public class StatisticalYear : BaseEntity
    {
        [Required]
        public int Year { get; set; }
        public bool IsOpen { get; set; } = true;
        public bool IsClosed { get; set; } = false;
        public virtual ICollection<StatisticalRegister> StatisticalRegisters { get; set; } = new List<StatisticalRegister>();
    }
}
