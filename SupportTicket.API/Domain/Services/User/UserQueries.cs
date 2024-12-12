namespace SupportTicket.API.Domain.Services.User;

public class UserQueries : ObjectGraphType
{
    public UserQueries(IUserService userService)
    {
        Description = "Queries in the user domain.";

        Field<ListGraphType<UserType>>("list")
            .ResolveAsync(async (context) => await userService.ListUsers())
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