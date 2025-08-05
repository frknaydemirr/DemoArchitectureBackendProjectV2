using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        void Add(User user);

        void Update(User user);


        void Delete(User user);

        List<User> GetAll();// ->dönüştipi List olmalı
        User Get(int id);
        //Get işlenmi bittiğinde user classı vermek zorundayız->dönüştipi

    }
}
