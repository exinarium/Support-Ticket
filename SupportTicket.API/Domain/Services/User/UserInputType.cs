namespace SupportTicket.API.Domain.Services.User;

public class UserInputType : InputObjectGraphType
{
    public UserInputType()
    {
        Field<GuidGraphType>("id");
        Field<StringGraphType>("firstName");
        Field<StringGraphType>("lastName");
    }
}