using AspNetCoreAngularTemplate.Application.Common.Interfaces;
using ValidationException = AspNetCoreAngularTemplate.Application.Common.Exceptions.ValidationException;

namespace AspNetCoreAngularTemplate.Application.Users.Commands.Register;

public record RegisterCommand : IRequest<string>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAccountManager _accountManager;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateService _emailTemplateService;

    public RegisterCommandHandler(IAccountManager accountManager, IEmailSender emailSender, IEmailTemplateService emailTemplateService)
    {
        _accountManager = accountManager;
        _emailSender = emailSender;
        _emailTemplateService = emailTemplateService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _accountManager.CreateUserAsync(request.Email!, request.Password!);

        if (!response.Result.Succeeded)
        {
            throw new ValidationException(response.Result.Errors);
        }

        await SendVerificationEmail(response.UserId, request.Email!);

        return response.UserId;
    }

    private async Task SendVerificationEmail(string userId, string userEmail)
    {
        var subject = "Confirm your email";
        var code = await _accountManager.GenerateEmailConfirmationTokenAsync(userId);
        var message = this._emailTemplateService.GetConfirmEmailTemplate(userId, code);
        await _emailSender.SendEmailAsync(userEmail, subject, message);
    }
}
