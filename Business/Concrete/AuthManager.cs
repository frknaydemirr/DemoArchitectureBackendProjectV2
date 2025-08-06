using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttinsConcerns.Validations;
using Core.Utilities.Hashing;
using Entities.Dtos;
using FluentValidation.Results;

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

        public string Register(RegisterAuthDto registerDto)
        {

            //Cross Cutting Concerns -> Uygulamayı dikine kesmek;
            //AOP
            // FluentValidation kullanımı


            UserValidator userValidator = new UserValidator();
            ValidationTool.Validate(userValidator, registerDto);

           _userService.Add(registerDto);
          return   "User record has been completed successfully";
            

        }

       
    }
}
