using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserRepository.Validation.FluentValidation
{
    //fluent validationu kullanabilmem için, fluent validation kullanan classı inherit etmem lazım:
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
           .WithMessage("UserName cannot be Empty! ");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be Empty!");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Please Entry valid Email Account ");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("User's Image cannot be Empty!");
          



        }

    }
}
