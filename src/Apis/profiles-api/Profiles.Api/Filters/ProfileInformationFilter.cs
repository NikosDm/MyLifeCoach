using System;

using Microsoft.AspNetCore.Http;

using Profiles.Api.Core.Abstractions.Services;

namespace Profiles.Api.Filters;

internal sealed class ProfileInformationFilter(IAuthorizeAccessService authorizeAccessService)
    : ProfileFilterBase(authorizeAccessService)
{
    protected override Guid GetUserId(EndpointFilterInvocationContext context)
    {
        return context.GetArgument<Guid>(0);
    }
}