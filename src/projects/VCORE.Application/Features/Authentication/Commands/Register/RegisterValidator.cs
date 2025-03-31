using FluentValidation;

namespace VCORE.Application.Features.Authentication.Commands.Register;

public class RegisterValidator: AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email field cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password field cannot be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(512).WithMessage("Password can be a maximum of 512 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*\.])[A-Za-z\d!@#$%^&*\.]{8,}$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character (including '.'), and must be at least 8 characters long.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username can be a maximum of 20 characters.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Username can only contain letters and numbers."); 

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name field cannot be empty.")
            .Matches("^[a-zA-ZğüşİĞĞçş]{1,50}$").WithMessage("First name can only contain letters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name field cannot be empty.")
            .Matches("^[a-zA-ZğüşİĞĞçş]{1,50}$").WithMessage("Last name can only contain letters.");
    }
}