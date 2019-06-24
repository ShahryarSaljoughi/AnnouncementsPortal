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
using Models.ApDbContext;
using Models.Entities;
using Portal.Services.Contracts;

namespace Portal.Services.Implementations
{
    public class EmailSenderService: IEmailSenderService
    {
        private APDbContext DbContext { get; set; }
        public EmailSenderService(APDbContext db)
        {
            DbContext = db;
        }
        public async Task SendEmailVerificationLink(Guid userId)
        {
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
            
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }

            var messageBodt = System.Convert.ToBase64String(
                Encoding.UTF8.GetBytes("To: s.shahryar75@gmail.com,\r\n" +
                                       "Subject: subject Test\r\n" +
                                       "Content-Type: text/html; charset=us-ascii\r\n\r\n" +
                                       "<h1>Body Test </h1>")
            );
            var message = new Message();
            message.Raw = messageBodt;
            
            UsersResource.MessagesResource.SendRequest request2 =
                service.Users.Messages.Send(message, "me");
            
            request2.Execute();




            throw new NotImplementedException();
        }
    }
}