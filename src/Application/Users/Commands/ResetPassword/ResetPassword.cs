
using AspNetCoreAngularTemplate.Application.Common.Exceptions;
using AspNetCoreAngularTemplate.Application.Common.Interfaces;

namespace AspNetCoreAngularTemplate.Application.Users.Commands.ResetPassword;

public record ResetPasswordCommand : IRequest
{
    public string? Email { get; set; }
    public string? NewPassword { get; set; }
    public string? ResetCode { get; set; }
}


public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IAccountManager _accountManager;
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateService _emailTemplateService;

    public ResetPasswordCommandHandler(IAccountManager accountManager, IEmailSender emailSender, IEmailTemplateService emailTemplateService)
    {
        _accountManager = accountManager;
        _emailSender = emailSender;
        _emailTemplateService = emailTemplateService;
    }
    
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = await this._accountManager.FindUserId(request.Email!);
        if (userId == null)
        {
            // don't reveal that the user does not exist 
            return;
        }
        
        var result = await this._accountManager.ResetPasswordAsync(userId, request.ResetCode!, request.NewPassword!);

        if (!result.Succeeded)
        {
            throw new AppValidationException(result.Errors);
        }
    }
    
    private async Task SendResetPasswordAsync(string userId, string userEmail, string code)
    {
        var subject = "Reset your password";
        var message = this._emailTemplateService.GetResetPasswordTemplate(userId, code);
        await _emailSender.SendEmailAsync(userEmail, subject, message);
    }
}