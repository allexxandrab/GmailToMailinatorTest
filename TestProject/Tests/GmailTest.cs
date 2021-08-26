using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TestProject.Tests.Base;

namespace TestProject.Tests
{
    
    [TestClass]
    public class GmailTest: BaseApi
    {
        [TestMethod]
        public void MessagesTest()
        {
            Debug.WriteLine("[STEP] : Getting emails from alburbulea@gmail.com");
            var messages = gmailApiActions.ReceiveMessages();

            Debug.WriteLine($"[INFO] : Recieved {messages.Count} messages");
            Debug.WriteLine("[ASSERT] : Ensure that email count is greater than 0");
            Assert.IsTrue(messages.Count > 0);
            
        }

        [TestMethod]
        public void FirstMessageTest()
        {
            Debug.WriteLine("[STEP] : Getting first email from alburbulea@gmail.com");
            var message = gmailApiActions.ReceiveFirstMessage();
            
            Debug.WriteLine("[INFO] : Recieved first sent message");
            Debug.WriteLine("[ASSERT] : Ensure that email is not null");
            Assert.IsTrue(message != null); 
            Debug.WriteLine($"[INFO] : {message}");
        }

        [TestMethod]
        public void SendMessageTest()
        {
            Debug.WriteLine("[STEP] : Sending emails to alburbulea@gmail.com from testautomationalexandra@mailinator.com");
            var sentMessage = gmailApiActions.SendMessage("TestSubject", "TestBody", "testautomationalexandra@mailinator.com");

            Debug.WriteLine($"[INFO] : Recieved messages from testautomationalexandra@mailinator.com");
            Debug.WriteLine("[ASSERT] : Ensure that first label id equals to 'SENT' ");
            Assert.IsTrue(sentMessage.LabelIds[0].Equals("SENT"));
        }
    }
}
