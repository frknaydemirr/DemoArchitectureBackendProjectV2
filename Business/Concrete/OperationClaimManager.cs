using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //Kontroller 
    //DataAccess katmanına kayıt işlemini yap
    //interface in yaptığı işlemler üzerinden işlem yapacağız
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        public void Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
           
        }
    }
}
//controller üzerinden ekleme işlemini yapalım