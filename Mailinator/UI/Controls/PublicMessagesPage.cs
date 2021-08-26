using Mailinator.UI.Controls.Base;
using OpenQA.Selenium;

namespace Mailinator.UI.Controls
{
    public class PublicMessagesPage : BasePage
    {
        public PublicMessagesPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement FirstLetter => Driver.FindElement(By.XPath("//tr[@ng-repeat='email in emails']/child::td[2]"));
    }
}
