using Business.Abstract;
using Core.Utilities.Hashing;
using Entities.Dtos;

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

        public void Register(RegisterAuthDto registerDto)
        {

            _userService.Add(registerDto);
        }
    }
}
