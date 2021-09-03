using GmailAPI.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TestProject.Tests.Base;


namespace TestProject.Tests
{
    [TestClass]
    public class MailinatorTest : MailinatorBase
    {

        [TestMethod]
        public void EmailSenderTest()
        {
            Debug.WriteLine("[STEP] : Opening mailinator web page");
            HomeActions.NavigateTo("https://www.mailinator.com/");

            HomeActions.SearchInbox("testautomationalexandra");
            Debug.WriteLine($"[INFO] : Recieved messages");

            Debug.WriteLine("[STEP] : Choosing first letter from the inbox");
            InboxActions.ChooseFirstLetter();

            Assert.IsTrue(InboxActions.GetReceiver().Equals("testautomationalexandra"));
            Assert.IsTrue(InboxActions.GetSender().Equals("alburbulea@gmail.com"));
            Debug.WriteLine("[ASSERT] : Sender is equal to corresponding email");
           

        }

        [TestMethod]
        public void GmailToMailinatorTest()
        {
            var emailInfo = EmailBuilder.StandardEmail();          
            
            Debug.WriteLine($"[STEP] : Sending emails to {emailInfo.Mailinator}@mailinator.com from alburbulea@gmail.com");
            var sentMessage = GmailApiActions.SendMessage(emailInfo);

            Debug.WriteLine($"[INFO] : Recieved message from alburbulea@gmail.com");
            Debug.WriteLine("[ASSERT] : Ensure that first label id equals to 'SENT' ");
            Assert.IsTrue(sentMessage.LabelIds[0].Equals("SENT"));

            Debug.WriteLine("[STEP] : Opening mailinator web page");
            HomeActions.NavigateTo("https://www.mailinator.com/");

            HomeActions.SearchInbox(emailInfo.Mailinator);
            Debug.WriteLine("[INFO] : Recieved messages");
            
            InboxActions.ChooseFirstLetter();

            Assert.IsTrue(InboxActions.GetReceiver().Equals(emailInfo.Mailinator));
            Assert.IsTrue(InboxActions.GetSender().Equals(emailInfo.Gmail));
            Debug.WriteLine("[ASSERT] : Sender is equal to corresponding email");
        }
    }
}
