﻿namespace AspNetCoreAngularTemplate.Application.Common.Interfaces;

public interface IEmailTemplateService
{
    string GetConfirmEmailTemplate(string userId, string token);
    string GetResetPasswordTemplate(string userId, string token);
}
