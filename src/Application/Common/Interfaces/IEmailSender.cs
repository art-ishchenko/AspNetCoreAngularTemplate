using AspNetCoreAngularTemplate.Application.Common.Models;

namespace AspNetCoreAngularTemplate.Application.Common.Interfaces;

public interface IEmailSender
{
    Task<Result> SendEmailAsync(string recipientEmail, string subject, string body, bool isHtml = true);
    Task<Result> SendEmailAsync(string recipientName, string recipientEmail, string subject, string body, bool isHtml = true);
    Task<Result> SendEmailAsync(string senderName, string senderEmail, string recipientName, string recipientEmail, string subject, string body, bool isHtml = true);
}