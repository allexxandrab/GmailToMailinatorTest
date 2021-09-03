using Mailinator.UI.Controls;
using Mailinator.Utils;
using OpenQA.Selenium;
using System.Diagnostics;

namespace Mailinator.UI.Actions
{
    public class InboxActions
    {
        private readonly PublicMessagesPage publicMessagePage;
        private readonly MessagePage messagePage;

        public InboxActions(IWebDriver driver)
        {
            publicMessagePage = new PublicMessagesPage(driver);
            messagePage = new MessagePage(driver);
        }
        public void ChooseFirstLetter()
        {
            Wait.UntilTrue(() => publicMessagePage.FirstLetter.Displayed);
            Debug.WriteLine("[UI ACTION] : Choosing first letter from the inbox ");
            publicMessagePage.FirstLetter.Click();

        }
        public string GetReceiver()
        {
            Wait.UntilTrue(() => !string.IsNullOrEmpty(messagePage.FieldTo.Text));
            Debug.WriteLine($"[INFO] : Getting the receiver field value");
            return messagePage.FieldTo.Text;
        }
        public string GetSender()
        {
            Wait.UntilTrue(() => !string.IsNullOrEmpty(messagePage.FieldFrom.Text));
            Debug.WriteLine($"[INFO] : Getting the sender field value");
            return messagePage.FieldFrom.Text;
         }
            

        
    }
}

