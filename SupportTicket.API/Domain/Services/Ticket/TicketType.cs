namespace SupportTicket.API.Domain.Services.Ticket;

public class TicketType : ObjectGraphType<Repository.Models.Ticket>
{
    public TicketType()
    {
        Description = "A support ticket created in the system.";

        Field(x => x.Id).Description("The id of the ticket.");
        Field(x => x.Subject).Description("The subject of the ticket.");
        Field(x => x.Message).Description("The message of the ticket.");
        Field(x => x.Status).Description("The status of the ticket.");
        Field(x => x.Priority).Description("The priority of the ticket.");
        Field(x => x.Tags).Description("The tags of the ticket.");
        Field(x => x.CreatedDateTime).Description("The created date of the ticket.");
        Field(x => x.UpdatedDateTime).Description("The updated date of the ticket.");

        Field<UserType>(
            "createdBy",
            resolve: context => context.Source.CreatedBy,
            description: "The user that created the ticket.");

        Field<UserType>(
            "updatedBy",
            resolve: context => context.Source.UpdatedBy,
            description: "The user that last updated the ticket.");

        Field<UserType>(
            "assignedTo",
            resolve: context => context.Source.AssignedTo,
            description: "The current assigned user of the ticket.");

        Field<ListGraphType<CommentType>>(
            "comments",
            resolve: context => context.Source.Comments,
            description: "The comments linked to the ticket.");

        Field<ListGraphType<FileType>>(
            "attachments",
            resolve: context => context.Source.Attachments,
            description: "The attachments linked to the ticket.");

        Field<ListGraphType<FileType>>(
            "attachments",
            resolve: context => context.Source.History,
            description: "The history of the ticket.");
    }
}

public class TicketPageListType : PageListTypeBase<Repository.Models.Ticket, TicketType>
{
    public TicketPageListType() : base()
    {
        Name = "TicketPageList";
    }
}