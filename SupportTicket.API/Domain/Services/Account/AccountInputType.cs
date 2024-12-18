namespace SupportTicket.API.Domain.Services.Account;

public class AccountInputType : InputObjectGraphType<Repository.Models.Account>
{
    public AccountInputType()
    {
        Description = "The input properties for a account";

        Field<GuidGraphType, Guid?>("id").Description("The account's ID").DefaultValue(Guid.NewGuid());
        Field(x => x.Name).Description("The account's name");
        Field(x => x.ContactName).Description("The account holder's name");
        Field(x => x.ContactEmail).Description("The account holder's email");
        Field(x => x.ContactTelephone).Description("The account holder's telephone number");
        Field(x => x.IsActive).Description("The account's active status.").DefaultValue(true);
    }
}