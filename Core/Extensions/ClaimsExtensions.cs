using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Extensions
{
    public  static  class ClaimsExtensions
    {
        public static void AddName(this ICollection<Claim> claims, string name)
        {

            claims.Add(new Claim(ClaimTypes.Name,name));
        }

        //roller için ekleme
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {

            roles.ToList().ForEach(roles => claims.Add(new Claim(ClaimTypes.Role, roles)));
        }
    }
}
