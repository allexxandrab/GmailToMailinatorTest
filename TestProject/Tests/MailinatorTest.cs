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
            Debug.WriteLine("[STEP] : Sending emails to alburbulea@gmail.com from testautomationalexandra@mailinator.com");
            var sentMessage = GmailApiActions.SendMessage("TestSubject", "TestBody", "testautomationalexandra@mailinator.com");

            Debug.WriteLine("[INFO] : Recieved messages from testautomationalexandra@mailinator.com");
            Debug.WriteLine("[ASSERT] : Ensure that first label id equals to 'SENT' ");
            Assert.IsTrue(sentMessage.LabelIds[0].Equals("SENT"));

            Debug.WriteLine("[STEP] : Opening mailinator web page");
            HomeActions.NavigateTo("https://www.mailinator.com/");

            HomeActions.SearchInbox("testautomationalexandra");
            Debug.WriteLine("[INFO] : Recieved messages");
            
            InboxActions.ChooseFirstLetter();

            Assert.IsTrue(InboxActions.GetReceiver().Equals("testautomationalexandra"));
            Assert.IsTrue(InboxActions.GetSender().Equals("alburbulea@gmail.com"));
            Debug.WriteLine("[ASSERT] : Sender is equal to corresponding email");
        }
    }
}
