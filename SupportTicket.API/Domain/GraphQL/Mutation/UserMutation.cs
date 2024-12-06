using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using SupportTicket.API.Domain.GraphQL.Type;
using SupportTicket.API.Domain.Repository.Models;


namespace SupportTicket.API.Domain.GraphQL.Mutation;

public class UserMutation : ObjectGraphType
{
    public UserMutation(DataContext dataContext)
    {
        Field<UserType>("updateUser")
            .Arguments(new QueryArgument<UserInputType>
            {
                Name = "user",
            })
            .ResolveAsync(async (context) =>
            {
                var updateUser = context.GetArgument<User>("user");
                var id = updateUser.Id;
                var user = await  dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);

                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;

                await dataContext.SaveChangesAsync();
                return user;
            });

    }
}