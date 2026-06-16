using HttpClient.domain.Features.Manager.ReqResModel;
using HttpClient.shared.Commons;

namespace HttpClient.domain.Features.Manager
{
    public interface IManagerService
    {
        Task<Result<CreateManagerResponse>> CreateAsync(CreateManagerRequest request);
        Task<Result<DeleteManagerResponse>> DeleteAsync(int id);
        Task<Result<ManagersResponse>> GetAllAsync();
        Task<Result<ManagerResponse>> GetByIdAsync(int id);
        Task<Result<ManagerResponse>> UpdateAsync(int id, UpdateManagerRequest request);
    }
}