namespace SupportTicket.API.Domain.Services.TicketHistory;

public class TicketHistoryType : ObjectGraphType<Repository.Models.TicketHistory>
{
    public TicketHistoryType()
    {
        Field(x => x.Id).Description("The id of the ticket history entry.");
    }
}