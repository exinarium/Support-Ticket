using SupportTicket.API.Domain.Helpers.Models;

namespace SupportTicket.API.Domain.Services.User;

public interface IUserService
{
    Task<Repository.Models.User> GetUser(Guid id);

    Task<PageList<Repository.Models.User>> ListUsers(PageInfo pageInfo, UserFilter filter);

    Task<Repository.Models.User> CreateUser(Repository.Models.User model);

    Task<Repository.Models.User> UpdateUser(Repository.Models.User model);
}

public class UserService(IDbContextFactory<DataContext> contextFactory, IHttpContextAccessor httpContextAccessor) : ServiceBase(contextFactory), IUserService
{
    public async Task<Repository.Models.User> GetUser(Guid id)
    {
        var data = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return data;
    }

    public async Task<PageList<Repository.Models.User>> ListUsers(PageInfo pageInfo, UserFilter filter)
    {
        var data = Context.Users.AsQueryable();

        if (filter == null)
        {
            throw new ArgumentNullException("The user filter cannot be null.");
        }

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
        {
            data = data.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.LastName))
        {
            data = data.Where(x => x.LastName.ToLower().Contains(filter.LastName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            data = data.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        }

        if (filter.IsActive.HasValue)
        {
            data = data.Where(x => x.IsActive == filter.IsActive);
        }

        if (filter.AccountId.HasValue)
        {
            data = data.Where(x => x.AccountId == filter.AccountId);
        }

        return await PageList<Repository.Models.User>.Create(pageInfo, data);
    }

    public async Task<Repository.Models.User> CreateUser(Repository.Models.User model)
    {
        if (model == null)
        {
            throw new ArgumentNullException("The user instance cannot be null.");
        }

        model.CreatedDateTime = DateTime.UtcNow;
        model.CreatedById = Guid.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

        var user = await Context.Users.AddAsync(model);
        await Context.SaveChangesAsync();

        return user.Entity;
    }

    public async Task<Repository.Models.User> UpdateUser(Repository.Models.User model)
    {
        if (model == null)
        {
            throw new ArgumentNullException("The user instance cannot be null.");
        }

        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

        if (user == null)
        {
            throw new ApplicationException("The user could not be found.");
        }

        user.UpdatedDateTime = DateTime.UtcNow;
        user.UpdatedById = Guid.Parse(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.IsActive = model.IsActive;
        user.Password = SecurityHelper.HashPassword(model.Password);
        user.IsLockoutEnabled = model.IsLockoutEnabled;

        Context.Users.Update(user);
        await Context.SaveChangesAsync();

        return user;
    }
}