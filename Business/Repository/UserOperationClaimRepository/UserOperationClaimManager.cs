using Business.Repository.OperationClaimRepository.Constans;
using Business.Repository.UserOperationClaimRepository.Constans;
using Business.Repository.UserOperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserOperationClaimRepository;
using Entities.Concrete;

namespace Business.Repository.UserOperationClaimRepository
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal  _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

       
        
        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessage.Deleted);
        }
       
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(p=>p.Id==id));
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessage.Updated);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
            public  IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(IsOperationSetAvaible(userOperationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessage.Added);
        }

        public IResult IsOperationSetAvaible(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(p=> p.UserId==userOperationClaim.UserId && p.OperationClaimId ==userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessage.OperationClaimSetExist);
            }
            return new SuccessResult();
        }
    }
}
