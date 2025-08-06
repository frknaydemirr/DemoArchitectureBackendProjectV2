using Entities.Dtos;

namespace Business.Abstract
{

    //auth servisi 
    public  interface IAuthService
    {
        string Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
