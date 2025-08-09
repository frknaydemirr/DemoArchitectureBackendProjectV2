using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.OperationClaimRepository.Validation.FluentValidation
{
    public  class OperationClaimValidator : AbstractValidator<OperationClaim>

    {
        public OperationClaimValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Please enter the name of the authority!");
        }

    }
}
