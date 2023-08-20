namespace AspNetCoreAngularTemplate.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.NewPassword).NotEmpty();
        RuleFor(x => x.ResetCode).NotEmpty();
    }
}
