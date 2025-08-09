using DataAccess.Repositories.UserOperationClaimRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.UserOperationClaimRepository
{
    public class IUserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal  _userOperationClaimDal;

        public IUserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public void Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
        }

       

    }
}
