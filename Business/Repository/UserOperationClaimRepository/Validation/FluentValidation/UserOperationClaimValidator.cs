using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserOperationClaimRepository.Validation.FluentValidation
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().WithMessage("Please select a user for the authorization assignment.");
            RuleFor(p => p.OperationClaimId).NotEmpty().WithMessage("You must select permissions for permission assignment.");
        }
    }
}
