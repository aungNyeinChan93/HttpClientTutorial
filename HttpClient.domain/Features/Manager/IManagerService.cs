using HttpClient.shared.Commons;

namespace HttpClient.domain.Features.Manager
{
    public interface IManagerService
    {
        Task<Result<ManagersResponse>> GetAllAsync();
    }
}