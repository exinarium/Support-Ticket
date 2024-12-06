using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using SupportTicket.API.Domain.GraphQL.Type;
using SupportTicket.API.Domain.Repository.Models;

namespace SupportTicket.API.Domain.GraphQL.Query;

public class UserQuery : ObjectGraphType
{
    public UserQuery(DataContext dataContext)
    {
        Field<ListGraphType<UserType>>("users")
            .ResolveAsync(async (context) => await dataContext.Users.ToListAsync());

        Field<UserType>("user")
            .Arguments(new QueryArgument<GuidGraphType>
            {
                Name = "id",
            })
            .ResolveAsync(async (context) =>
                await dataContext.Users
                    .Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.Id == context.GetArgument<Guid>("id", Guid.Empty)));
    }
}