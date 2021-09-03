using GmailAPI.Builders;
using Mailinator.UI.Actions;


namespace TestProject.Tests.Base
{
    public class MailinatorBase : BaseUi
    {
        protected HomeActions HomeActions;
        protected InboxActions InboxActions;
        protected EmailBuilder EmailBuilder;
        
        protected override void Initialize()
        {
            HomeActions = new HomeActions(Driver);
            InboxActions = new InboxActions(Driver);
            EmailBuilder = new EmailBuilder();
        }

      
    }
}

