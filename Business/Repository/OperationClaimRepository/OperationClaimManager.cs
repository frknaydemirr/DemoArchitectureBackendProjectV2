using Business.Repository.OperationClaimRepository.Constans;
using Business.Repository.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.OperationClaimRepository
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

        //validate işlemi;
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameAvaible(operationClaim.Name));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);

        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);

        }


        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);

        }


        public IDataResult<List<OperationClaim>> GetList()
        {

            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());

        }

        public IDataResult<OperationClaim> GetById(int id)
        {

            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(p => p.Id == id));

        }


        private IResult IsNameAvaible(string name)
        {
            var result = _operationClaimDal.Get(p => p.Name == name);
            if (result != null)
            {
                return new ErrorResult(OperationClaimMessages.NameIsNotAvaible);
            }
            return new SuccessResult();

        }
    }
}
//controller üzerinden ekleme işlemini yapalım