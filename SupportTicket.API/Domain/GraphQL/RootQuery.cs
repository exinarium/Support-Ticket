namespace SupportTicket.API.Domain.GraphQL;

public class RootQuery : ObjectGraphType
{
    public RootQuery()
    {
        Field<UserQueries>("users")
            .Resolve(_ => new { });

        Field<AccountQueries>("accounts")
            .Resolve(_ => new { });
    }
}