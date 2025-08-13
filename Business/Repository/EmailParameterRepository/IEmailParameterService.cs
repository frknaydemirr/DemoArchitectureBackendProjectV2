using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.EmailParameterRepository
{
    public  interface IEmailParameterService
    {

        IResult Add(EmailParameters emailParameters);

        IResult Update(EmailParameters emailParameters);

        IResult Delete(EmailParameters emailParameters);

        IDataResult<List<EmailParameters>> GetList();

        IDataResult<EmailParameters> GetById(int id);

        IResult SendEmail(EmailParameters emailParameters, string body,
            string subject, string emails);

    }
}
