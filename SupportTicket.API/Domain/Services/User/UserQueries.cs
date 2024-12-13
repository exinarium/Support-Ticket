using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.User;

public class UserQueries : ObjectGraphType
{
    public UserQueries(IUserService userService)
    {
        Description = "Queries in the user domain.";

        Field<UserType.UserPageListType>("list")
            .Authorize()
            .Arguments(
                new QueryArgument<PageInfoInputType?>
                {
                    Name = "pageInfo",
                    Description = "The page information for the query."
                },
                new QueryArgument<UserFilterInputType>
                {
                    Name = "filter",
                    Description = "The filter to apply to the query."
                })
            .ResolveAsync(async (context) => await userService.ListUsers(context.GetArgument<PageInfo?>("pageInfo"), context.GetArgument<UserFilter>("filter")))
            .Description("Get a list of users.");

        Field<UserType>("get")
            .Authorize()
            .Arguments(new QueryArgument<GuidGraphType>
            {
                Name = "id",
            })
            .ResolveAsync(async (context) => await userService.GetUser(context.GetArgument<Guid>("id", Guid.Empty)))
            .Description("Get a user by the user's ID.");
    }
}