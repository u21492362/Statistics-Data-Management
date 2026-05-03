namespace Backend__SDM_.Models.ViewModels
{
    public class RegisterMemberCategoryViewModel
    {
        public int Id { get; set; }
        public int StatisticalCategoryId { get; set; }
        public string CategoryCode { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public bool Selected { get; set; }
        public int? ValueNumber { get; set; }
        public string? ValueText { get; set; }
        public bool IsSystemGenerated { get; set; }
    }
}
