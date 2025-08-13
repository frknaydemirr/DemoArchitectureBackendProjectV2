using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.EmailParameterRepository.Validation.FluentValidation
{
    public class EmailParameterValidator : AbstractValidator<EmailParameters>
    {
        public EmailParameterValidator()
        {

            RuleFor(p => p.Smtp).NotEmpty().WithMessage("SMTP adress can not be null!");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email adress can not be null!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password  can not be null!");
            RuleFor(p => p.Port).NotEmpty().WithMessage("PORT adress can not be null!");
         
        }
    }
}
