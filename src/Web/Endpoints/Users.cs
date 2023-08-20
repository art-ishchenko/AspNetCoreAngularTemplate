using AspNetCoreAngularTemplate.Application.Users.Commands.ConfirmEmail;
using AspNetCoreAngularTemplate.Application.Users.Commands.RecoverPassword;
using AspNetCoreAngularTemplate.Application.Users.Commands.Register;
using AspNetCoreAngularTemplate.Application.Users.Commands.ResetPassword;
using AspNetCoreAngularTemplate.Application.Users.Queries.GetCurrentUser;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using YamlDotNet.Core.Events;

namespace AspNetCoreAngularTemplate.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this).MapGet(GetCurrentUser, "Current", ResponseTypes.Ok<string>(), ResponseTypes.NotFound)
           .MapPost(Register, "Register", ResponseTypes.Ok<string>(), ResponseTypes.BadRequest, ResponseTypes.Forbidden)
           .MapPut(ConfirmEmail, "ConfirmEmail", ResponseTypes.Accepted, ResponseTypes.NotFound, ResponseTypes.BadRequest)
           .MapPost(RecoverPassword, "RecoverPassword", ResponseTypes.Accepted, ResponseTypes.BadRequest)
           .MapPost(ResetPassword, "ResetPassword", ResponseTypes.NoContent, ResponseTypes.BadRequest);
    }

    public async Task<string> GetCurrentUser(ISender sender)
    {
        return await sender.Send(new GetCurrentUserQuery());
    }

    public Task<string> Register(ISender sender, RegisterCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IActionResult> ConfirmEmail(ISender sender, ConfirmEmailCommand command)
    {
        await sender.Send(command);
        return new AcceptedResult();
    }

    public async Task<IActionResult> RecoverPassword(ISender sender, RecoverPasswordCommand command)
    {
        await sender.Send(command);
        return new AcceptedResult();
    }
    
    public async Task<IActionResult> ResetPassword(ISender sender, ResetPasswordCommand command)
    {
        await sender.Send(command);
        return new NoContentResult();
    }
}
