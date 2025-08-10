using Business.Authentication.Validation.FluentValidation;
using Business.Repository.UserRepository;
using Core.Aspects.Validation;

using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler  _tokenHandler;



        public AuthManager(IUserService userService ,ITokenHandler tokenHandler )
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token> Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password,user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id);
            if (result)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token);
            }
            return new ErrorDataResult<Token>("User' mail account or password is wrong!");
        }


        [ValidationAspect(typeof(AuthValidator))]
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
