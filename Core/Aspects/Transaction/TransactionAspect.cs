using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Transaction
{
    public  class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();
                }
                catch (Exception)
                {

                    transaction.Dispose(); //hata tespiti durumunda  yaptığım bütün işlemleri geri al!
                }
            }
        }


    }
}
