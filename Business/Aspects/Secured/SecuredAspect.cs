using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspects.Security
{
    public class SecuredAspect : MethodInterception
    {

        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();


        }

        //işlem başlamadan önce benim güvenlik kontrolüm yapılsın:
        protected override void OnBefore(IInvocation invocation)
        {
            if (_roles != null && _roles.Length > 0)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimsRoles();

                bool hasRole = _roles.Any(role => roleClaims.Contains(role));
                if (!hasRole)
                {
                    throw new Exception("You are not authorized to perform this operation.");
                }
            }
            else
            {
                // sadece token var mı yok mu kontrol et
                var claims = _httpContextAccessor.HttpContext.User.Claims;
                if (!claims.Any())
                {
                    throw new Exception("You are not authorized to perform this operation.");
                }
            }
        }


    }
}
