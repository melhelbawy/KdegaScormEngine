using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Application.Extensions;
public static class HttpContextExtensions
{
    public static Guid? GetUserId(this IIdentity identity) => ((ClaimsIdentity)identity)
        .GetByType(ClaimTypes.NameIdentifier).Select(x => x == null ? (Guid?)null : Guid.Parse(x)).FirstOrDefault();

    public static List<string> GetUserRoles(this IIdentity identity) => ((ClaimsIdentity)identity)
        .GetByType(ClaimTypes.Role).ToList();


    private static List<string> GetByType(this ClaimsIdentity identity, string type) => identity.Claims
        .Where(c => c.Type == type).Select(c => c.Value).ToList();
}
