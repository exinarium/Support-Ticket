using GraphQL.Types;
using SupportTicket.API.Domain.Repository.Models;

namespace SupportTicket.API.Domain.GraphQL.Type;

public class AccountType : ObjectGraphType<Account>
{
    public AccountType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
    }
}