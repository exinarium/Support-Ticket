using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.Account;

public class AccountType : ObjectGraphType<Repository.Models.Account>
{
    public AccountType()
    {
        Description = "Account the user is linked to in a multi-tenant solution";

        Field(x => x.Id).Description("The ID of the account.");
        Field(x => x.Name).Description("The name of the account.");
        Field(x => x.ContactName).Description("The name of the account holder");
        Field(x => x.ContactEmail).Description("The email address of the account holder");
        Field(x => x.ContactTelephone).Description("The telephone number of the account holder");
        Field(x => x.IsActive).Description("The active state of the account");
        Field(x => x.CreatedDateTime).Description("The created date and time of the account");
        Field(x => x.UpdatedDateTime).Description("The updated date and time of the account");
        Field<ListGraphType<UserType>>(
            "users",
            resolve: context => context.Source.Users,
            description: "The users linked to the account.");
    }

    public class AccountPageListType : PageListTypeBase<Repository.Models.Account, AccountType>
    {
        public AccountPageListType() : base()
        {
            Name = "AccountPageList";
        }
    }
}