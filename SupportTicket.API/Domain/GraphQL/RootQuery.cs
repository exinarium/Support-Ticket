namespace SupportTicket.API.Domain.GraphQL;

public class RootQuery : ObjectGraphType
{
    public RootQuery(UserQueries userQueries)
    {
        Field<UserQueries>("users")
            .Resolve(context => userQueries);
    }
}