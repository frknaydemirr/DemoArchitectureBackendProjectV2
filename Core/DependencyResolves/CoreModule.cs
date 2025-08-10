using Core.CrossCuttinsConcerns.Cashing;
using Core.CrossCuttinsConcerns.Cashing.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolves
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCashManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //business katmanımda :
        }
    }
}
