using Core.Result.Abstract;
using Core.Result.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{

    //auth servisi 
    public  interface IAuthService
    {
        IResult  Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
