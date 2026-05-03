using Backend__SDM_.Models.ViewModels;

namespace Backend__SDM_.Services.Interface
{
    public interface IMemberService
    {
        Task<List<MemberViewModel>> GetAllAsync(int? societyId = null, string? search = null);
        Task<MemberViewModel?> GetByIdAsync(int id);
        Task<int> CreateAsync(MemberViewModel model);
        Task<bool> UpdateAsync(int id, MemberViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
