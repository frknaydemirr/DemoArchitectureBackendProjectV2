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
            RuleFor(p => p.UserId).Must(IsIdValid).WithMessage("Please select a user for the authorization assignment.");
            //RuleFor(p => p.OperationClaimId).GreaterThan(0).NotEmpty().WithMessage("You must select permissions for permission assignment.");
            RuleFor(p => p.OperationClaimId).Must(IsIdValid).WithMessage("You must select permissions for permission assignment.");
        }

        private bool IsIdValid(int id)
        {
            if (id > 0 && id != null)
            {
                return true;
            }
            return false;
        }
    }
}
