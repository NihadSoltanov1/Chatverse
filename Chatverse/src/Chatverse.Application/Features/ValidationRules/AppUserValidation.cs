using Chatverse.Application.Features.Command.AppUser.Login;
using Chatverse.Application.Features.Command.AppUser.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.ValidationRules
{
    public class RegisterAppUserValidation : AbstractValidator<UserRegisterCommandRequest>
    {
        public RegisterAppUserValidation()
        {
            RuleFor(a => a.FullName).NotEmpty().WithMessage("Fullname  is required.").MaximumLength(50).WithMessage("Max Length: 50.");
            RuleFor(a => a.Username).NotEmpty().WithMessage("Username is required.").MaximumLength(40).WithMessage("Max Length: 40");
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email address.");
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.PasswordConfirm)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.IsAgree).Equal(true).WithMessage("Accept the terms for the sign up");
        }
    }
}
