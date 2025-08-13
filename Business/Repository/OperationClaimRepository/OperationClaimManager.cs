﻿using Business.Aspects.Security;
using Business.Repository.OperationClaimRepository.Constans;
using Business.Repository.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Performance;
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
using System.Xml.Linq;

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
            IResult result = BusinessRules.Run(IsNameExistForAdd(operationClaim.Name));
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
            IResult result = BusinessRules.Run(IsNameExistForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);

        }


        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);

        }

        //aspecte parametre istemiyorsak  bir constructer yapısı daha kurmam lazım.
        [SecuredAspect()]
        [PerformanceAspect()]
        public IDataResult<List<OperationClaim>> GetList()
        {

            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());

        }

        public IDataResult<OperationClaim> GetById(int id)
        {

            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(p => p.Id == id));

        }


        private IResult IsNameExistForAdd(string name)
        {
            var result = _operationClaimDal.Get(p => p.Name == name);
            if (result != null)
            {
                return new ErrorResult(OperationClaimMessages.NameIsNotAvaible);
            }
            return new SuccessResult();

        }

        private IResult IsNameExistForUpdate(OperationClaim  operationClaim)
        {
            var currentOperationClaim = _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name !=operationClaim.Name)
            {
                var result = _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(OperationClaimMessages.NameIsNotAvaible);
                }
               
            }
            return new SuccessResult();

        }

    }
}
//controller üzerinden ekleme işlemini yapalım