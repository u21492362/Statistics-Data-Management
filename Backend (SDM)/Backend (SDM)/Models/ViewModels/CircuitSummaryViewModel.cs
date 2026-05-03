namespace Backend__SDM_.Models.ViewModels
{
    public class CircuitSummaryViewModel
    {
        public int StatisticalYearId { get; set; }
        public int Year { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = string.Empty;

        public int CircuitId { get; set; }
        public string CircuitName { get; set; } = string.Empty;

        public int TotalSocieties { get; set; }
        public int TotalRegisteredMembers { get; set; }

        public List<CircuitSummaryItemViewModel> Items { get; set; } = new();
    }
}
