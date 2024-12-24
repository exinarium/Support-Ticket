namespace SupportTicket.API.Domain.Services.Ticket;

public class TicketFilter
{

}

public class TicketFilterInputType : InputObjectGraphType<TicketFilter>
{
    public TicketFilterInputType()
    {
        Description = "The filters to apply to the tickets before retrieving.";
    }
}