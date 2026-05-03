namespace Backend__SDM_.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalSocieties { get; set; }
        public int TotalRegisters { get; set; }
        public int DraftRegisters { get; set; }
        public int SubmittedRegisters { get; set; }
        public int ApprovedRegisters { get; set; }
        public int FinalisedRegisters { get; set; }

        public List<SocietySummaryItemViewModel> TopCategories { get; set; } = new();
    }
}
