namespace SupportTicket.API.Domain.GraphQL;

public class MainSchema : Schema
{
    public MainSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RootQuery>();
        Mutation = serviceProvider.GetRequiredService<RootMutation>();
    }
}