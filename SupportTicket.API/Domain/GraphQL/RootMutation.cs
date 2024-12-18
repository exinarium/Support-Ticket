namespace SupportTicket.API.Domain.GraphQL;

public class RootMutation : ObjectGraphType
{
    public RootMutation()
    {
        Field<UserMutations>("users")
            .Resolve(_ => new { });

        Field<AccountMutations>("accounts")
            .Resolve(_ => new { });
    }
}