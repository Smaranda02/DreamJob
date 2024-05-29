using DreamJob.BusinessLogic.Candidates.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator() 
        {
            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("Please enter your email address")
               //.EmailAddress().WithMessage("Invalid email format")
               ;

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Please enter your first name.")
                .Must(firstName => firstName.All(char.IsLetter)).WithMessage("First name should contain only letters")
                .Length(3, 40).WithMessage("Minimum length is 3");

            RuleFor(r => r.Surname)
                .NotEmpty().WithMessage("Please enter your surname.")
                .Must(firstName => firstName.All(char.IsLetter)).WithMessage("First name should contain only letters")
                .Length(3, 40).WithMessage("Minimum length is 3");


            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Enter a password")
                .MinimumLength(3).WithMessage("Password must be at least 3 characters long.");


            RuleFor(r => r.ConfirmPassword)
               .NotEmpty().WithMessage("Confirm your password")
                .Equal(r => r.Password).WithMessage("Passwords do not match");

        }

    }
}
