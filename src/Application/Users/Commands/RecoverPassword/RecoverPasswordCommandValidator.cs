using AspNetCoreAngularTemplate.Application.Users.Commands.ResetPassword;

namespace AspNetCoreAngularTemplate.Application.Users.Commands.RecoverPassword;

public class RecoverPasswordCommandValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}
