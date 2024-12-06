using SupportTicket.API.Domain.GraphQL.Mutation;
using SupportTicket.API.Domain.GraphQL.Query;

namespace SupportTicket.API.Domain.GraphQL.Schema;

public class UserSchema : global::GraphQL.Types.Schema
{
    public UserSchema(UserQuery userQuery, UserMutation userMutation)
    {
        Query = userQuery;
        Mutation = userMutation;
    }
}