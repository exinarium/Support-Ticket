namespace SupportTicket.API.Domain.Services.User;

public class UserMutations : ObjectGraphType
{
    public UserMutations()
    {
        Description = "Mutations in the user domain.";

        Field<UserType>("update")
            .Arguments(new QueryArgument<UserInputType>
            {
                Name = "user",
            })
            .ResolveAsync(async (context) =>
            {
                return null;
                // var updateUser = context.GetArgument<User>("user");
                // var id = updateUser.Id;
                // var user = await  dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                //
                // user.FirstName = updateUser.FirstName;
                // user.LastName = updateUser.LastName;
                //
                // await dataContext.SaveChangesAsync();
                // return user;
            })
            .Description("Update the user's details.");

    }
}