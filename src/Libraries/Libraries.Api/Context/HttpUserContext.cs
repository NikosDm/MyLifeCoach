using System;
using System.Security.Claims;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;

using Microsoft.AspNetCore.Http;

namespace Libraries.Api.Context;


public sealed class HttptUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal _principal;

    public HttptUserContext(IHttpContextAccessor httpContextAccessor)
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

            return Guid.TryParse(sub, out var userId) ? userId : null;
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

