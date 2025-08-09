using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {

        void Add(UserOperationClaim userOperationClaim);
    }
}
