using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Libraries.Common.Entities;

using Microsoft.AspNetCore.Http;

namespace Libraries.Api.Middleware;

public sealed class UserContextMiddleware(IUserContextAccessor accessor) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var p = context.User;

        if (p?.Identity?.IsAuthenticated == true)
        {
            var sub = p.FindFirstValue(SecurityConstants.SUB_CLAIM);
            Guid? userId = Guid.TryParse(sub, out var g) ? g : null;

            accessor.Current = new UserContextData(
                IsAuthenticated: true,
                UserId: userId,
                Username: p.FindFirstValue(SecurityConstants.USERNAME_CLAIM),
                Role: p.FindFirstValue(SecurityConstants.ROLE_CLAIM)
            );
        }
        else
        {
            accessor.Current = new UserContextData(false, null, null, null);
        }

        try
        {
            await next(context);
        }
        finally
        {
            accessor.Current = null;
        }
    }
}