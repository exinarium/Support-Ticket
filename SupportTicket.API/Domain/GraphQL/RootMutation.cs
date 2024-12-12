namespace SupportTicket.API.Domain.GraphQL;

public class RootMutation : ObjectGraphType
{
    public RootMutation(IServiceProvider serviceProvider)
    {
        Field<UserMutations>("users")
            .Resolve(_ => new { });
    }
}