namespace Backend__SDM_.Models.ViewModels
{
    public class CircuitSummaryItemViewModel
    {
        public string CategoryCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int CircuitTotal { get; set; }
        public List<CircuitSocietyBreakdownViewModel> SocietyBreakdown { get; set; } = new();
    }
}
