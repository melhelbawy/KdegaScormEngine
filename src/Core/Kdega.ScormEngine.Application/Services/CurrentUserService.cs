using Kdega.ScormEngine.Application.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdega.ScormEngine.Application.Services;
public class CurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid? UserId => _contextAccessor.HttpContext?.User.Identity?.GetUserId();
}
