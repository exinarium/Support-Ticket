namespace SupportTicket.API.Domain.Services.Account;

public class AccountFilter
{
    public string? Name { get; set; } = null;

    public string? ContactName { get; set; } = null;

    public string? ContactEmail { get; set; } = null;

    public string? ContactTelephone { get; set; } = null;

    public bool? IsActive { get; set; } = true;
}

public class AccountFilterInputType : InputObjectGraphType<AccountFilter>
{
    public AccountFilterInputType()
    {
        Description = "The filters to apply to the accounts before retrieving.";

        Field(x => x.Name).Description("The name of the account.");
        Field(x => x.ContactName).Description("The name of the account holder.");
        Field(x => x.ContactEmail).Description("The email address of the account holder.");
        Field(x => x.ContactTelephone).Description("The telephone number of the account holder.");
        Field(x => x.IsActive).Description("Whether the Account is active.").DefaultValue(true);
    }
}