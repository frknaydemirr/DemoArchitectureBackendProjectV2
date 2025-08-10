using Castle.DynamicProxy;
using Core.CrossCuttinsConcerns.Cashing;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Caching
{
    public  class CacheAspect : MethodInterception

    {

        private int _duraiton;
        private ICacheManager _cacheManager;

        public CacheAspect(int duraiton)
        {
            _duraiton = duraiton;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        //olay esnasında dahil olmalı:
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format("{0}.{1}", invocation.Method.ReflectedType.FullName, invocation.Method.Name);

            var argument = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", argument.Select(p => p?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue= _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duraiton);
        }

    }
}
