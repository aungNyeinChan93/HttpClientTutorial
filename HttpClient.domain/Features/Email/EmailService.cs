using FluentEmail.Core;
using FluentEmail.Core.Models;
using HttpClient.domain.Features.Email.ReqResModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Features.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task<SendResponse> SendAsync(EmailRequest request)
        {
            var response = await _fluentEmail
                .To(request.ToEmail)
                .Subject(request.Subject)
                .Body(request.Body)
                .SendAsync();

            return response;
        }
    }
}
