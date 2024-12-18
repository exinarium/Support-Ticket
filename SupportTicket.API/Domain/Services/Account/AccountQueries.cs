using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.User;

public class AccountQueries : ObjectGraphType
{
    public AccountQueries(IAccountService accountService)
    {
        Description = "Queries in the account domain.";

        Field<AccountType.AccountPageListType>("list")
            .Authorize()
            .Arguments(
                new QueryArgument<PageInfoInputType?>
                {
                    Name = "pageInfo",
                    Description = "The page information for the query."
                },
                new QueryArgument<AccountFilterInputType>
                {
                    Name = "filter",
                    Description = "The filter to apply to the query."
                })
            .ResolveAsync(async (context) => await accountService.ListAccounts(context.GetArgument<PageInfo?>("pageInfo"), context.GetArgument<AccountFilter>("filter")))
            .Description("Get a list of accounts.");

        Field<AccountType>("get")
            .Authorize()
            .Arguments(new QueryArgument<GuidGraphType>
            {
                Name = "id",
            })
            .ResolveAsync(async (context) => await accountService.GetAccount(context.GetArgument<Guid>("id", Guid.Empty)))
            .Description("Get an account by the account's ID.");
    }
}