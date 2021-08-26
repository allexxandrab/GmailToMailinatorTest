using GmailAPI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject.Tests.Base
{
    [TestClass]
    public class BaseApi
    {
        protected GmailApiActions gmailApiActions;

        [TestInitialize]
        public void Initialize()
        {
            gmailApiActions = new GmailApiActions();
        }
    }
}
