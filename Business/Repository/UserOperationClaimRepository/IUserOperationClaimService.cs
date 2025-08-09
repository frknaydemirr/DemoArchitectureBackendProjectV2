using Core.Utilities.Result.Abstract;
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

        IResult Add(UserOperationClaim userOperationClaim);

        IResult Update(UserOperationClaim userOperationClaim);

        IResult Delete(UserOperationClaim userOperationClaim);

        IDataResult<List<UserOperationClaim>> GetList();

        IDataResult<UserOperationClaim> GetById(int id);
    }
}
