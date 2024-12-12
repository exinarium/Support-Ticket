namespace SupportTicket.API.Domain.GraphQL;

public static class GraphQLRegistration
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        #region Users

        services.AddScoped<UserQueries>();
        services.AddScoped<UserMutations>();

        #endregion
        services.AddScoped<ISchema, MainSchema>();
        services.AddScoped<RootQuery>();
        services.AddScoped<RootMutation>();

        services
            .AddGraphQL(options =>
            {
                options.AddAutoSchema<ISchema>();
                options.AddSystemTextJson();
                options.AddAuthorizationRule();
                options.AddSystemTextJson();
                options.AddGraphTypes();
            });

        return services;
    }
}