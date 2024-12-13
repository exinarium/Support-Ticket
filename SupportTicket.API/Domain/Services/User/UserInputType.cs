namespace SupportTicket.API.Domain.Services.User;

public class UserInputType : InputObjectGraphType<Repository.Models.User>
{
    public UserInputType()
    {
        Description = "The input properties for a user";

        Field<GuidGraphType, Guid?>("id").Description("The user's ID").DefaultValue(Guid.NewGuid());
        Field(x => x.FirstName).Description("The user's first name");
        Field(x => x.LastName).Description("The user's last name");
        Field<StringGraphType, string?>("email").Description("The user's email");
        Field(x => x.Password).Description("The user's password");
        Field<GuidGraphType, Guid?>("accountId").Description("The user's account ID");
        Field(x => x.IsLockoutEnabled).Description("Whether the account can be locked or not.").DefaultValue(true);
        Field(x => x.IsActive).Description("The user's active status.").DefaultValue(true);
    }
}