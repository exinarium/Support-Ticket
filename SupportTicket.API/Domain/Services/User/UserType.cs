using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.User;

public class UserType : ObjectGraphType<Repository.Models.User>
{
    public UserType(
        )
    {
        Description = "A registered user of the system.";

        Field(x => x.Id).Description("The ID of the user.");
        Field(x => x.Email).Description("The user's email address.");
        Field(x => x.FirstName).Description("The user's first name.");
        Field(x => x.LastName).Description("The user's last name.");
        Field(x => x.IsActive).Description("The user's active status.");
        Field<AccountType>(
            "account",
            resolve: context => context.Source.Account,
            description: "The user's account.");
    }

    public class UserPageListType : PageListTypeBase<Repository.Models.User, UserType>
    {
        public UserPageListType() : base()
        {
            Name = "UserPageList";
        }
    }
}