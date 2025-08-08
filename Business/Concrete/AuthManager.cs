using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttinsConcerns.Validations;
using Core.Result.Abstract;
using Core.Result.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Entities.Dtos;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
   
          

public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password,user.PasswordHash, user.PasswordSalt);
            if (result)
            {
              return  "User has entered succesfully ";

            }
            else
            {
                return "User's info  wrong ";
            }
        }


        [ValidationAspect(typeof(UserValidator))]
        [LogAspect]
        //log aspect işlem bittikten sonra çalışacakken validatioın işlemden önce çalışmalı!
        public IResult Register(RegisterAuthDto registerDto)
        {

            //Cross Cutting Concerns -> Uygulamayı dikine kesmek;
            //AOP
            // FluentValidation kullanımı

            int imgSize = 2;
          

            IResult result = BusinessRules.Run(CheckIfEmailExists(registerDto.Email),
                CheckIfImageSizeIsLessThenOneMb(imgSize)
                );


            if (!result.Success)
            {
                return result;
            }

            _userService.Add(registerDto);
            return new SuccessResult("User record has been completed successfully");

        }


        

        
        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("This Email Account already  has been used");
            }
            return new SuccessResult("User record has been completed successfully");
        }

        private IResult CheckIfImageSizeIsLessThenOneMb(int imgSize)
        {
            if (imgSize > 1)
            {
                return new ErrorResult("This  Uploaded Image shoul be less then 1 MG ");
            }
            return new SuccessResult("User record has been completed successfully");
        }
       
    }
}
