namespace Backend__SDM_.Models.ViewModels
{
    public class SocietySummaryViewModel
    {
        public int StatisticalYearId { get; set; }
        public int Year { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;

        public int CircuitId { get; set; }
        public string CircuitName { get; set; } = string.Empty;

        public int SocietyId { get; set; }
        public string SocietyName { get; set; } = string.Empty;

        public int TotalRegisteredMembers { get; set; }

        public List<SocietySummaryItemViewModel> Items { get; set; } = new();
    }
}
