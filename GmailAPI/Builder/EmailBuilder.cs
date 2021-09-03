using GmailAPI.Objects;


namespace GmailAPI.Builders
{
    public class EmailBuilder
    {
      
        public EmailInfo StandardEmail(string gmail = "alburbulea@gmail.com")
        {
            return new EmailInfo
            {
                Subject = Faker.Identification.MedicareBeneficiaryIdentifier(),
                Body = Faker.Lorem.Sentence(),
                Mailinator = Faker.Internet.UserName(),
                Gmail = gmail
            };
        }

    }
}