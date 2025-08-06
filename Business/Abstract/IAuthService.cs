using Entities.Dtos;

namespace Business.Abstract
{

    //auth servisi 
    public  interface IAuthService
    {
        void Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
