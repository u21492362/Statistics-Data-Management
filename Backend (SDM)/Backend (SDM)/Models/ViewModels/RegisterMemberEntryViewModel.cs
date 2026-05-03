using Backend__SDM_.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend__SDM_.Models.ViewModels
{
    public class RegisterMemberEntryViewModel
    {
        public int Id { get; set; }

        [Required]
        public int StatisticalRegisterId { get; set; }

        [Required]
        public int MemberId { get; set; }

        public int RowNumber { get; set; }

        public string MembershipNumber { get; set; } = string.Empty;
        public string MemberFullName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public int Age { get; set; }

        public string? Remarks { get; set; }

        public List<RegisterMemberCategoryViewModel> Categories { get; set; } = new();
    }
}
