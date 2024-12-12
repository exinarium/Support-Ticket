namespace SupportTicket.API.Domain.Services.Account;

public class AccountType : ObjectGraphType<Repository.Models.Account>
{
    public AccountType()
    {
        Description = "Account the user is linked to in a multi-tenant solution";

        Field(x => x.Id).Description("The ID of the account.");
        Field(x => x.Name).Description("The name of the account.");
    }
}