﻿using AspNetCoreAngularTemplate.Application.Common.Exceptions;
using AspNetCoreAngularTemplate.Application.Common.Interfaces;

namespace AspNetCoreAngularTemplate.Application.Users.Commands.ConfirmEmail;

public record ConfirmEmailCommand : IRequest
{
    public string? UserId { get; set; }
    public string? Code { get; set; }
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IAccountManager _accountManager;

    public ConfirmEmailCommandHandler(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await this._accountManager.ConfirmEmailAsync(request.UserId!, request.Code!);
        
        if (!result.Succeeded)
        {
            throw new AppValidationException(result.Errors);
        }
    }
}
