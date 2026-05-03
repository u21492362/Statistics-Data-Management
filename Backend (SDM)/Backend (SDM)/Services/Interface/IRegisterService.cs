using Backend__SDM_.Models.ViewModels;

namespace Backend__SDM_.Services.Interface
{
    public interface IRegisterService
    {
        Task<List<StatisticalRegisterViewModel>> GetRegistersAsync(int? yearId = null, int? societyId = null);
        Task<StatisticalRegisterViewModel?> GetRegisterAsync(int id);
        Task<int> CreateRegisterAsync(StatisticalRegisterViewModel model);
        Task<bool> SubmitRegisterAsync(int id);
        Task<bool> FinaliseRegisterAsync(int id);

        Task<RegisterCaptureViewModel?> GetRegisterCaptureAsync(int registerId);
        Task<int> AddMemberToRegisterAsync(int registerId, int memberId, string? remarks);
        Task<bool> SaveEntryCategoriesAsync(int registerEntryId, List<RegisterMemberCategoryViewModel> categories);
    }
}
