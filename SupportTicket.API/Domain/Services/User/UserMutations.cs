namespace SupportTicket.API.Domain.Services.User;

public class UserMutations : ObjectGraphType
{
    public UserMutations(IUserService userService)
    {
        Description = "Mutations in the user domain.";

        Field<UserType>("update")
            .Authorize()
            .Arguments(new QueryArgument<UserInputType>
            {
                Name = "user",
            })
            .ResolveAsync(async (context) =>
            {
                return await userService.UpdateUser(context.GetArgument<Repository.Models.User>("user"));
            })
            .Description("Update the user's details.");

        Field<UserType>("create")
            .Authorize()
            .Arguments(new QueryArgument<UserInputType>
            {
                Name = "user",
            })
            .ResolveAsync(async (context) =>
            {
                return await userService.CreateUser(context.GetArgument<Repository.Models.User>("user"));
            })
            .Description("Create a new user.");

    }
}