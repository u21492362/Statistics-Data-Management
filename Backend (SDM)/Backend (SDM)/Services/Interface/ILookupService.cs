using Backend__SDM_.Models.ViewModels;

namespace Backend__SDM_.Services.Interface
{
    public interface ILookupService
    {
        Task<List<DropdownViewModel>> GetDistrictsAsync();
        Task<List<DropdownViewModel>> GetCircuitsAsync(int? districtId = null);
        Task<List<DropdownViewModel>> GetSocietiesAsync(int? circuitId = null);
        Task<List<DropdownViewModel>> GetYearsAsync();
        Task<List<StatisticalCategoryViewModel>> GetCategoriesAsync();
    }
}
