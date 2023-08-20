using AspNetCoreAngularTemplate.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace AspNetCoreAngularTemplate.Infrastructure.Email.Templates;

public class EmailTemplateService: IEmailTemplateService
{
    private readonly EmailTemplateServiceOptions _options;
    
    public EmailTemplateService(IOptions<EmailTemplateServiceOptions> options)
    {
        this._options = options.Value;
    }
    
    public string GetConfirmEmailTemplate(string userId, string token)
    {
        var template = $"Please confirm your email <a href='{this._options.AppUrl}/api/users/confirmEmail?userId={userId}&code={token}'>here</a>";
        return template;
    }
    
    public string GetResetPasswordTemplate(string userId, string token)
    {
        var template = $"Please reset your password <a href='{this._options.AppUrl}/api/users/resetPassword?userId={userId}&code={token}'>here</a>";
        return template;
    }
}

