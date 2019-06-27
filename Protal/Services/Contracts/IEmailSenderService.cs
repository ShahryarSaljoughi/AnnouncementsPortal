using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services.Contracts
{
    public interface IEmailSenderService
    {
        Task SendEmailVerificationLink(Guid userId);
    }
}
