using GmailAPI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject.Tests.Base
{
    [TestClass]
    public abstract class BaseUi
    {
        protected IWebDriver Driver;
        protected GmailApiActions GmailApiActions;

        [TestInitialize]
        public void TestInit()
        {
            GmailApiActions = new GmailApiActions();
            new DriverManager().SetUpDriver(new ChromeConfig(), "MatchingBrowser");
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Initialize();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Driver.Quit();
        }
        protected abstract void Initialize();
    }
}
    
