using Mailinator.UI.Controls.Base;
using OpenQA.Selenium;


namespace Mailinator.UI.Controls
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }


        public IWebElement SearchInput => Driver.FindElement(By.XPath("//input[@aria-label='Enter Inbox Name']"));


        public IWebElement GoButton => Driver.FindElement(By.XPath("//button[@class='btn btn-go-public h-auto']"));

    }
}
