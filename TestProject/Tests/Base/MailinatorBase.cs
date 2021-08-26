using Mailinator.UI.Actions;


namespace TestProject.Tests.Base
{
    public class MailinatorBase : BaseUi
    {
        protected HomeActions HomeActions;
        protected InboxActions InboxActions;

        
        protected override void Initialize()
        {
            HomeActions = new HomeActions(Driver);
            InboxActions = new InboxActions(Driver);
        }

      
    }
}

