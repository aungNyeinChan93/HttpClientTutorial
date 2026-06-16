using FluentEmail.Core.Models;
using HttpClient.domain.Features.Email.ReqResModels;

namespace HttpClient.domain.Features.Email
{
    public interface IEmailService
    {
        Task<SendResponse> SendAsync(EmailRequest request);
    }
}