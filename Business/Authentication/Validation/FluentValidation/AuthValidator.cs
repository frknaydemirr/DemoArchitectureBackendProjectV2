using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication.Validation.FluentValidation
{
    public  class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p=>p.Name).NotEmpty()
                .WithMessage("UserName cannot be Empty! ");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be Empty!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Entry valid Email Account ");
            RuleFor(p => p.Image.FileName).NotEmpty().WithMessage("User's Image cannot be Empty!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be Empty!");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Password must be 6 character at least!");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password must be 1 upper character at least");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password must be 1 lower character at least");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Password must be 1 number  character at least");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password must be 1 special character at least");



        }
    }
}


