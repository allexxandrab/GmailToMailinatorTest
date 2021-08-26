using OpenQA.Selenium;


namespace Mailinator.UI.Controls.Base
{
    public class BasePage
    {
        protected IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }


        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}
