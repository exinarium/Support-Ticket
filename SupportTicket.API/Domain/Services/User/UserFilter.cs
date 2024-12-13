namespace SupportTicket.API.Domain.Services.User;

public class UserFilter
{
    public string? FirstName { get; set; } = null;

    public string? LastName { get; set; } = null;

    public string? Email { get; set; } = null;

    public bool? IsActive { get; set; } = true;

    public Guid? AccountId { get; set; } = null;
}

public class UserFilterInputType : InputObjectGraphType<UserFilter>
{
    public UserFilterInputType()
    {
        Description = "The filters to apply to the users before retrieving.";

        Field(x => x.FirstName).Description("The first name of the user.");
        Field(x => x.LastName).Description("The last name of the user.");
        Field(x => x.Email).Description("The email address of the user.");
        Field(x => x.IsActive).Description("Whether the user is active.").DefaultValue(true);
        Field(x => x.AccountId).Description("The account ID of the user.");
    }
}