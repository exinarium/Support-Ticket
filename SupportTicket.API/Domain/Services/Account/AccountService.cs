using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.Account;

public interface IAccountService
{
    Task<Repository.Models.Account> GetAccount(Guid id);

    Task<PageList<Repository.Models.Account>> ListAccounts(PageInfo pageInfo, AccountFilter filter);

    Task<Repository.Models.Account> CreateAccount(Repository.Models.Account model);

    Task<Repository.Models.Account> UpdateAccount(Repository.Models.Account model);
}

public class AccountService(IDbContextFactory<DataContext> contextFactory, IHttpContextAccessor httpContextAccessor) : ServiceBase(contextFactory), IAccountService
{
    public async Task<Repository.Models.Account> GetAccount(Guid id)
    {
        var data = await Context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
        return data;
    }

    public async Task<PageList<Repository.Models.Account>> ListAccounts(PageInfo pageInfo, AccountFilter filter)
    {
        var data = Context.Accounts.AsQueryable();

        if (filter == null)
        {
            throw new ArgumentNullException("The Account filter cannot be null.");
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            data = data.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.ContactName))
        {
            data = data.Where(x => x.ContactName.ToLower().Contains(filter.ContactName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.ContactEmail))
        {
            data = data.Where(x => x.ContactEmail.ToLower().Contains(filter.ContactEmail.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.ContactTelephone))
        {
            data = data.Where(x => x.ContactTelephone.ToLower().Contains(filter.ContactTelephone.ToLower()));
        }

        if (filter.IsActive.HasValue)
        {
            data = data.Where(x => x.IsActive == filter.IsActive);
        }

        return await PageList<Repository.Models.Account>.Create(pageInfo, data);
    }

    public async Task<Repository.Models.Account> CreateAccount(Repository.Models.Account model)
    {
        if (model == null)
        {
            throw new ArgumentNullException("The Account instance cannot be null.");
        }

        model.Id = Guid.NewGuid();
        model.CreatedDateTime = DateTime.UtcNow;

        var account = await Context.Accounts.AddAsync(model);
        await Context.SaveChangesAsync();

        return account.Entity;
    }

    public async Task<Repository.Models.Account> UpdateAccount(Repository.Models.Account model)
    {
        if (model == null)
        {
            throw new ArgumentNullException("The Account instance cannot be null.");
        }

        var account = await Context.Accounts.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (account == null)
        {
            throw new ApplicationException("The Account could not be found.");
        }

        account.UpdatedDateTime = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            account.Name = model.Name;
        }

        if (!string.IsNullOrWhiteSpace(model.ContactName))
        {
            account.ContactName = model.ContactName;
        }

        if (!string.IsNullOrWhiteSpace(model.ContactEmail))
        {
            account.ContactEmail = model.ContactEmail;
        }

        if (!string.IsNullOrWhiteSpace(model.ContactTelephone))
        {
            account.ContactTelephone = model.ContactTelephone;
        }

        account.IsActive = model.IsActive;

        Context.Accounts.Update(account);
        await Context.SaveChangesAsync();

        return account;
    }
}