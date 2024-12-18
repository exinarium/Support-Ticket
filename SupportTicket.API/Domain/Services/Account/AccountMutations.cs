namespace SupportTicket.API.Domain.Services.User;

public class AccountMutations : ObjectGraphType
{
    public AccountMutations(IAccountService accountService)
    {
        Description = "Mutations in the account domain.";

        Field<AccountType>("update")
            .Authorize()
            .Arguments(new QueryArgument<AccountInputType>
            {
                Name = "account",
            })
            .ResolveAsync(async (context) =>
            {
                return await accountService.UpdateAccount(context.GetArgument<Repository.Models.Account>("account"));
            })
            .Description("Update the account's details.");

        Field<AccountType>("create")
            .Authorize()
            .Arguments(new QueryArgument<AccountInputType>
            {
                Name = "account",
            })
            .ResolveAsync(async (context) =>
            {
                return await accountService.CreateAccount(context.GetArgument<Repository.Models.Account>("account"));
            })
            .Description("Create a new account.");

    }
}