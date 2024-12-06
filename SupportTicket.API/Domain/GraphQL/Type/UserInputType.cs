using GraphQL.Types;

namespace SupportTicket.API.Domain.GraphQL.Type;

public class UserInputType : InputObjectGraphType
{
    public UserInputType()
    {
        Field<GuidGraphType>("id");
        Field<StringGraphType>("firstName");
        Field<StringGraphType>("lastName");
    }
}