namespace SupportTicket.API.Domain.Services.User;

public interface IUserService
{
    Task<Repository.Models.User> GetUser(Guid id);

    Task<List<Repository.Models.User>> ListUsers();
}

public class UserService(IDbContextFactory<DataContext> contextFactory) : ServiceBase(contextFactory), IUserService
{
    public async Task<Repository.Models.User> GetUser(Guid id)
    {
        var data = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return data;
    }

    public async Task<List<Repository.Models.User>> ListUsers()
    {
        var data = await Context.Users.ToListAsync();

        return data;
    }
}