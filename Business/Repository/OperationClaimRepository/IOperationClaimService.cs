using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.OperationClaimRepository
{
    //business Crud işlemlerinin Generic yapısı yok!
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);

        IResult Update(OperationClaim operationClaim);

        IResult Delete(OperationClaim operationClaim);

        IDataResult<List<OperationClaim>> GetList();

        IDataResult<OperationClaim> GetById(int id);
    }
}
