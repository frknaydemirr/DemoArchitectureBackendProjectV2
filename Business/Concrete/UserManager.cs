using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }



        public void Add(AuthDto authDto)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePassword(authDto.Password, out  passwordHash, out  passwordSalt);

            User user = new User();
            user.Id = 0;
            user.Email=authDto.Email;   
            user.Name=authDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImageUrl = authDto.ImageUrl;
            _userDal.Add(user);
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}
