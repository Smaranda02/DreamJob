using DreamJob.BusinessLogic.Employers.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Employers
{
    public class RegisterEmployerValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterEmployerValidator()
        {
            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("Please enter your email address")
               //.EmailAddress().WithMessage("Invalid email format")
               ;

            RuleFor(r => r.EmployerName)
                .NotEmpty().WithMessage("Please enter your first name.")
                .Length(3, 40).WithMessage("Minimum length is 3");


            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Enter a password")
                .MinimumLength(3).WithMessage("Password must be at least 3 characters long.");


            RuleFor(r => r.ConfirmPassword)
               .NotEmpty().WithMessage("Confirm your password")
                .Equal(r => r.Password).WithMessage("Passwords do not match");


            RuleFor(r => r.OfficeLocation)
               .NotEmpty().WithMessage("Enter a location");

            RuleFor(r => r.EmployerDescription)
               .NotEmpty().WithMessage("Enter a description");

        }

    }
}
