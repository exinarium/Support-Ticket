namespace SupportTicket.API.Domain.Services.Ticket;

public interface ITicketService
{
    Task<Repository.Models.Ticket> GetTicket(Guid id);

    Task<PageList<Repository.Models.Ticket>> ListTickets(PageInfo pageInfo, TicketFilter filter);

    Task<Repository.Models.Ticket> CreateTicket(Repository.Models.Ticket model);

    Task<Repository.Models.Ticket> UpdateTicket(Repository.Models.Ticket model);
}

public class TicketService(IDbContextFactory<DataContext> contextFactory) : ServiceBase(contextFactory), ITicketService
{
    public async Task<Repository.Models.Ticket> GetTicket(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PageList<Repository.Models.Ticket>> ListTickets(PageInfo pageInfo, TicketFilter filter)
    {
        throw new NotImplementedException();
    }

    public async Task<Repository.Models.Ticket> CreateTicket(Repository.Models.Ticket model)
    {
        throw new NotImplementedException();
    }

    public async Task<Repository.Models.Ticket> UpdateTicket(Repository.Models.Ticket model)
    {
        throw new NotImplementedException();
    }
}