using System;
using System.Security.Claims;
using Libraries.Common.Abstractions;
using Libraries.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace Goals.Api.Context;

public sealed class HttpContextUser : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ClaimsPrincipal _principal;

    public HttpContextUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _principal = _httpContextAccessor.HttpContext?.User;
    }

    public bool IsAuthenticated => _principal.Identity.IsAuthenticated;

    public Guid? UserId
    {
        get
        {
            var sub = _principal.FindFirstValue(SecurityConstants.SUB_CLAIM);

            if (Guid.TryParse(sub, out var userId))
                return userId;

            return null;
        }
    }

    public string Username => GetClaimStringValue(SecurityConstants.USERNAME_CLAIM);

    public string Role => GetClaimStringValue(SecurityConstants.ROLE_CLAIM);

    private string GetClaimStringValue(string claimType)
    {
        var username = _principal.FindFirstValue(claimType);
        return string.IsNullOrWhiteSpace(username) ? null : username;
    }
}
