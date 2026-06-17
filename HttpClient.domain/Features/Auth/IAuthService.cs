using HttpClient.domain.Features.Auth.ReqResModels;
using HttpClient.shared.Commons;

namespace HttpClient.domain.Features.Auth
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> Login(LoginRequest request);
        Task<Result<RegisterResponse>> Register(RegisterRequest request);
    }
}