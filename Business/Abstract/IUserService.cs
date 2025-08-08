using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public  interface IUserService
    {
        Task Add(RegisterAuthDto authDto);
        List<User> GetList();

        User GetByEmail(string email);
    }
}
