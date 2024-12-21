namespace SupportTicket.API.Domain.Services.Comment;

public class CommentType : ObjectGraphType<Repository.Models.Comment>
{
    public CommentType()
    {
        Field(x => x.Id).Description("The id of the comment");
    }
}