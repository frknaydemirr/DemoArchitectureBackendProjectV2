using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserRepository.Validation.FluentValidation
{
    public  class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Password cannot be Empty!");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Password must be 6 character at least!");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Password must be 1 upper character at least");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Password must be 1 lower character at least");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("Password must be 1 number  character at least");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("Password must be 1 special character at least");
        }
    }
}
