using Castle.DynamicProxy;
using Core.CrossCuttinsConcerns.Cashing;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Caching
{
    public  class RemoveCacheAspect : MethodInterception
    {

        private string  _pattern;
        private ICacheManager _cacheManager;

        public RemoveCacheAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {

            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
