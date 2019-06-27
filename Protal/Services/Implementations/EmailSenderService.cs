using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.ApDbContext;
using Models.Entities;
using Portal.Services.Contracts;

namespace Portal.Services.Implementations
{
    public class EmailSenderService: IEmailSenderService
    {
        private APDbContext DbContext { get; set; }
        private IConfiguration Configuration {get; set;}
        public EmailSenderService(APDbContext db, IConfiguration config)
        {
            DbContext = db;
            Configuration = config;
        }
        public async Task SendEmailVerificationLink(Guid userId)
        {
            var user = await DbContext.Set<Teacher>().FindAsync(userId);
            UserCredential credential;
            string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose };
            string ApplicationName = "my application";

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var code = GetRandomCode();
            var base64EmailBody = GetEmailBody(user, code);
            var message = new Message();
            message.Raw = base64EmailBody;
            
            UsersResource.MessagesResource.SendRequest request2 = service.Users.Messages.Send(message, "me");   
            request2.Execute();

            var codeRow = new EmailVerificaionCode()
            {
                Id = Guid.NewGuid(), Code = code.ToString(), UserId = user.Id
            };
            DbContext.Set<EmailVerificaionCode>().Add(codeRow);
            await DbContext.SaveChangesAsync();
        }

        private string GetEmailBody(Teacher teacher, int code)
        {
            var link = Configuration["WebsiteAddress"] + $"api/Authentication/VerifyEmail?Id={teacher.Id}&code={code}";
            var base64EmailBody = System.Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"To: {teacher.Email.Split(',')[0]},\r\n" +
                                       "Subject: subject Test\r\n" +
                                       "Content-Type: text/html; charset=us-ascii\r\n\r\n" +
                                       $"<h1><a href=\"{link}\">لینک فعالسازی حساب کاربری شما در سامانه اطلاع رسانی دانشگاه زنجان</a></h1>")
            ).TrimEnd().Replace('+', '-').Replace('/', '_');
            return base64EmailBody;
        }
        private static int GetRandomCode()
        {
            var code = new Random().Next(999, 10_000);
            return code;
        }
    }
}