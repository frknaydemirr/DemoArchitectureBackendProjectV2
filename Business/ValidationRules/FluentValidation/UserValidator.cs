using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public  class UserValidator : AbstractValidator<RegisterAuthDto>
    {
        public UserValidator()
        {
            RuleFor(p=>p.Name).NotEmpty()
                .WithMessage("UserName cannot be Empty! ");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be Empty!");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("User's Image cannot be Empty!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be Empty!");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Password must be 6 character at least!");

        }
    }
}


