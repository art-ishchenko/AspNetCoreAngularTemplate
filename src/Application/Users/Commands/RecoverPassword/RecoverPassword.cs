
using AspNetCoreAngularTemplate.Application.Common.Interfaces;

namespace AspNetCoreAngularTemplate.Application.Users.Commands.RecoverPassword;

public record RecoverPasswordCommand : IRequest
{
    public string? Email { get; set; }
}


public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand>
{
    private readonly IAccountManager _accountManager;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateService _emailTemplateService;

    public RecoverPasswordCommandHandler(IAccountManager accountManager, IEmailSender emailSender, IEmailTemplateService emailTemplateService)
    {
        _accountManager = accountManager;
        _emailSender = emailSender;
        _emailTemplateService = emailTemplateService;
    }
    
    public async Task Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = await this._accountManager.FindUserId(request.Email!);
        if (userId == null)
        {
            // don't reveal that the user does not exist 
            return;
        }
        
        var isEmailConfirmed = await this._accountManager.IsEmailConfirmedAsync(request.Email!);
        if (!isEmailConfirmed)
        {
            // don't reveal that the email is not confirmed 
            return;
        }
        
        var code = await _accountManager.GeneratePasswordResetTokenAsync(userId);
        await this.SendEmail(userId, request.Email!, code);
    }
    
    private async Task SendEmail(string userId, string userEmail, string code)
    {
        var subject = "Reset your password";
        var message = this._emailTemplateService.GetResetPasswordTemplate(userId, code);
        await _emailSender.SendEmailAsync(userEmail, subject, message);
    }
}