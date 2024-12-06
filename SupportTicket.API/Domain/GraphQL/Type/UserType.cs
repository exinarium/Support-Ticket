using GraphQL.Types;
using SupportTicket.API.Domain.Repository.Models;

namespace SupportTicket.API.Domain.GraphQL.Type;

public class UserType : ObjectGraphType<User>
{
    public UserType()
    {
        Field(x => x.Id);
        Field(x => x.Email);
        Field(x => x.FirstName);
        Field(x => x.LastName);
        Field<AccountType>("account", resolve: context => context.Source.Account);
    }
}