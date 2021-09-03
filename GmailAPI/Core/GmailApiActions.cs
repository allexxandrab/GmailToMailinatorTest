using GmailAPI.Objects;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GmailAPI.Core
{
    public class GmailApiActions
    {
        private static readonly string[] Scopes = { GmailService.Scope.MailGoogleCom };
        private static readonly string ApplicationName = "Gmail API .NET Quickstart";
        private readonly GmailService service;

        public GmailApiActions()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("Credentials\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

        }

        public IList<Message> ReceiveMessages()
        {
            var getMessagesRequest = service.Users.Messages.List("me");
            return getMessagesRequest.Execute().Messages;
        }

        public string ReceiveFirstMessage()
        {
            var messages = ReceiveMessages();

            var messageId = messages.Last().Id;
            var getMessageRequest = service.Users.Messages.Get("me", messageId);
            var message = getMessageRequest.Execute();

            var decodeData = Decode(message.Payload.Parts.First().Body.Data);

            return decodeData;
        }

        public Message SendMessage(string subject, string body, string sendTo) {
            Debug.WriteLine($"[INFO] : Creating message with {subject} subject, {body} body, {sendTo} sender ");
            var mailMessage = new System.Net.Mail.MailMessage(); 
            mailMessage.To.Add(sendTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            var mimeMessage = MimeMessage.CreateFromMailMessage(mailMessage);

        var gmailMessage = new Message
        {
            Raw = Encode(mimeMessage)
        };

        var request = service.Users.Messages.Send(gmailMessage, "me");
         Message testResponse = request.Execute();
            return testResponse;
    }

        public Message SendMessage(EmailInfo emailInfo, string domen = "@mailinator.com")
        {
            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.To.Add($"{emailInfo.Mailinator}{domen}");
            mailMessage.Subject = emailInfo.Subject;
            mailMessage.Body = emailInfo.Body;
            var mimeMessage = MimeMessage.CreateFromMailMessage(mailMessage);

            var gmailMessage = new Message
            {
                Raw = Encode(mimeMessage)
            };

            var request = service.Users.Messages.Send(gmailMessage, "me");
            Message testResponse = request.Execute();
            return testResponse;
        }

        private string Decode (string data)
        {
            data = data.PadRight(data.Length + (4 - data.Length % 4) % 4, '=')
            .Replace("_", "/")
            .Replace("-", "+");
            var byteArray = Convert.FromBase64String(data);
            return Encoding.ASCII.GetString(byteArray);
        }

        private string Encode(MimeMessage mimeMessage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                mimeMessage.WriteTo(ms);
                return Convert.ToBase64String(ms.GetBuffer())
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
            }
        }

    }
}
