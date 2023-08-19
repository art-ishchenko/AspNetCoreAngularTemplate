using AspNetCoreAngularTemplate.Application.Users.Queries.GetCurrentUser;

namespace AspNetCoreAngularTemplate.Web.Endpoints;

public class Users: EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet(
            GetCurrentUser,
            "Current",
            ResponseTypes.Ok<string>(),
            ResponseTypes.NotFound);
    }

    public async Task<string> GetCurrentUser(ISender sender)
    {
        return await sender.Send(new GetCurrentUserQuery());
    }
}
