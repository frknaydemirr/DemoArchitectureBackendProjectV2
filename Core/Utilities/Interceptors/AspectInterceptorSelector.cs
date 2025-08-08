using Castle.Core.Internal;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[]? SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var  classAttributes= type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //burada 
            //[ValidationAspect(typeof(UserValidator))]
            //[LogAspect] kaç tane varsa hepsini yakaladık!
            //birleştirme işlemi
            var methodAttribues = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttribues);

            return classAttributes.ToArray();

        }
    }
}
