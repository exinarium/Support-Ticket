namespace SupportTicket.API.Domain.Services.File;

public class FileType : ObjectGraphType<Repository.Models.File>
{
    public FileType()
    {
        Field(x => x.Id).Description("The id of the file.");
    }
}