using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    //business Crud işlemlerinin Generic yapısı yok!
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
    }
}
