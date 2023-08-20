using AspNetCoreAngularTemplate.Application.Users.Commands.ConfirmEmail;
using AspNetCoreAngularTemplate.Application.Users.Commands.Register;
using AspNetCoreAngularTemplate.Application.Users.Queries.GetCurrentUser;

namespace AspNetCoreAngularTemplate.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet(GetCurrentUser, "Current", ResponseTypes.Ok<string>(), ResponseTypes.NotFound)
           .MapPost(Register, "Register", ResponseTypes.Ok<string>(), ResponseTypes.BadRequest, ResponseTypes.Forbidden)
           .MapPut(ConfirmEmail, "ConfirmEmail", ResponseTypes.Accepted, ResponseTypes.NotFound, ResponseTypes.BadRequest);
    }

    public async Task<string> GetCurrentUser(ISender sender)
    {
        return await sender.Send(new GetCurrentUserQuery());
    }

    public Task<string> Register(ISender sender, RegisterCommand command)
    {
        return sender.Send(command);
    }

    public Task ConfirmEmail(ISender sender, ConfirmEmailCommand command)
    {
        return sender.Send(command);
    }
}
