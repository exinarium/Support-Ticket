namespace SupportTicket.API.Domain.Services.User;

public class UserSchema : global::GraphQL.Types.Schema
{
    public UserSchema(UserQueries userQueries, UserMutations userMutations)
    {
        Query = userQueries;
        Mutation = userMutations;
    }
}