using Mailinator.UI.Controls;
using OpenQA.Selenium;
using System.Diagnostics;

namespace Mailinator.UI.Actions
{
    public class HomeActions
    {
        private readonly HomePage homePage; 
        public HomeActions(IWebDriver driver)
        {
            homePage = new HomePage(driver);
        }

        public void NavigateTo(string url)
        {
            Debug.WriteLine($"[INFO] : Navigating to {url}");
            homePage.NavigateTo(url);
        }
        public void SearchInbox(string inboxName)
        {
            Debug.WriteLine($"[UI ACTION] : Filling search input field with '{inboxName}' ");
            homePage.SearchInput.SendKeys(inboxName);
            homePage.GoButton.Click();
        }
    }


}
