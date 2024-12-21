namespace SupportTicket.API.Domain.Services.Ticket;

public interface ITicketService
{

}

public class TicketService(IDbContextFactory<DataContext> contextFactory) : ServiceBase(contextFactory), ITicketService
{

}