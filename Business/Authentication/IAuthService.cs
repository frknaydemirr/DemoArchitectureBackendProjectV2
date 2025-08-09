using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Authentication
{

    //auth servisi 
    public  interface IAuthService
    {
        IResult  Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
