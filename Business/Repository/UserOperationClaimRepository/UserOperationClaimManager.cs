﻿using Business.Repository.OperationClaimRepository;
using Business.Repository.OperationClaimRepository.Constans;
using Business.Repository.UserOperationClaimRepository.Constans;
using Business.Repository.UserOperationClaimRepository.Validation.FluentValidation;
using Business.Repository.UserRepository;
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
        private readonly IOperationClaimService _operationClaimService;
        //mevcut kullanıcı var mı ?
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService,IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
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
            IResult result = BusinessRules.Run(IsUserExist(userOperationClaim.UserId),
               IsOperationClaimExist(userOperationClaim.OperationClaimId),
               IsOperationSetExistForUpdate(userOperationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessage.Updated);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
            public  IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(IsUserExist(userOperationClaim.UserId),
                IsOperationClaimExist(userOperationClaim.OperationClaimId),
                IsOperationSetExistForAdd(userOperationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(UserOperationClaimMessage.Added);
        }

        public IResult IsOperationSetExistForAdd(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(p=> p.UserId==userOperationClaim.UserId && p.OperationClaimId ==userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessage.OperationClaimSetExist);
            }
            return new SuccessResult();
        }


        public IResult IsUserExist(int userId)
        {
            var result = _userService.GetById(userId).Data;
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessage.UserNotExist);
            }
            return new SuccessResult();
        }


        public IResult IsOperationClaimExist(int operationClaimId )
        {
            var result = _operationClaimService.GetById(operationClaimId).Data;
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessage.OperationClaimNotExist);
            }
            return new SuccessResult();
        }

        private IResult IsOperationSetExistForUpdate(UserOperationClaim userOperationClaim)
        {
            var currentUserOperationClaim = _userOperationClaimDal.Get(p => p.Id == userOperationClaim.Id);
            if (currentUserOperationClaim.UserId != userOperationClaim.UserId || currentUserOperationClaim.OperationClaimId!=
              userOperationClaim.OperationClaimId  )
            {
                var result = _userOperationClaimDal.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
                if (result != null)
                {
                    return new ErrorResult(UserOperationClaimMessage.OperationClaimSetExist);
                }
               

            }
            return new SuccessResult();

        }



    }
}
