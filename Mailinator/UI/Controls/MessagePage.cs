using Mailinator.UI.Controls.Base;
using OpenQA.Selenium;

namespace Mailinator.UI.Controls
{
    public class MessagePage : BasePage
    {
        public MessagePage(IWebDriver driver) : base(driver)
        {
        }

       public IWebElement FieldTo => Driver.FindElement(By.XPath("//div[contains(text(),'To')]/following-sibling::div"));

        
       public IWebElement FieldFrom => Driver.FindElement(By.XPath("//div[contains(text(),'From')]/following-sibling::div"));

    }
}
