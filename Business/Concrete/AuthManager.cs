using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Validation;
using Core.CrossCuttinsConcerns.Validations;
using Core.Result.Abstract;
using Core.Result.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Entities.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
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
        //dependency ile programa bunu çağıracağımı anlatmam lazım:
        
        //log aspect işlem bittikten sonra çalışacakken validatioın işlemden önce çalışmalı!
        public IResult Register(RegisterAuthDto registerDto)
        {

            //Cross Cutting Concerns -> Uygulamayı dikine kesmek;
            //AOP
            // FluentValidation kullanımı


            IResult result = BusinessRules.Run(CheckIfEmailExists(registerDto.Email),
                CheckIfImageExtensionsAllow(registerDto.Image.FileName),
                CheckIfImageSizeIsLessThenOneMb(registerDto.Image.Length)

                );


            if (result !=null)
            {
                return result;
            }

            //dosya ismini criptolayacağız



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

        private IResult CheckIfImageSizeIsLessThenOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new ErrorResult("This  Uploaded Image shoul be less then 1 MG ");
            }
            return new SuccessResult("User record has been completed successfully");
        }

        private IResult CheckIfImageExtensionsAllow(string fileName)
        {
          
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("The uploaded image must be in \".jpg\", \".jpeg\", \".gif\", or \".png\" format!");
            }

            return new SuccessResult();
        }
       
    }
}
