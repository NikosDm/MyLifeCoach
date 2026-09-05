using System;
using System.Threading.Tasks;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Profiles.Api.Core.Dtos;

namespace Profiles.Api.Filters;

public sealed class ProfileActionCreateFilter(IUserContext userContext) : IEndpointFilter
{
    private readonly IUserContext _userContext = userContext
        ?? throw new ArgumentNullException(nameof(userContext));

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.GetArgument<BaseProfileRequest>(0);

        var canCreateProfile =
            _userContext.Role == SecurityConstants.ADMIN_ROLE ||
            request.UserId == _userContext.UserId;

        if (!canCreateProfile)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = "You are not authorized to create a Professional Profile for this user.",
                Status = StatusCodes.Status403Forbidden,
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.Extensions.Add(
                "traceId",
                context.HttpContext.TraceIdentifier);

            return TypedResults.Problem(problemDetails);
        }

        return await next(context);
    }
}