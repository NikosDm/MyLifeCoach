using System;

using Microsoft.AspNetCore.Http;

using Profiles.Api.Core.Abstractions.Services;
using Profiles.Api.Core.Dtos;

namespace Profiles.Api.Filters;

internal sealed class ProfileActionCreateFilter(IAuthorizeAccessService authorizeAccessService) : ProfileFilterBase(authorizeAccessService)
{
    protected override Guid GetUserId(EndpointFilterInvocationContext context)
    {
        var request = context.GetArgument<BaseProfileRequest>(0);
        return request.UserId;
    }
}