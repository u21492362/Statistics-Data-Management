namespace Backend__SDM_.Models.ViewModels
{
    public class RegisterCaptureViewModel
    {
        public StatisticalRegisterViewModel Register { get; set; } = new();
        public List<RegisterMemberEntryViewModel> Entries { get; set; } = new();
        public List<DropdownViewModel> AvailableMembers { get; set; } = new();
        public List<DropdownViewModel> AvailableCategories { get; set; } = new();
    }
}
