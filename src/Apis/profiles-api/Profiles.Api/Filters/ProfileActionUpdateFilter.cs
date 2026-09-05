using System;
using System.Threading.Tasks;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Profiles.Api.Core.Dtos;

namespace Profiles.Api.Filters;

public sealed class ProfileActionUpdateFilter(IUserContext userContext) : IEndpointFilter
{
    private readonly IUserContext _userContext = userContext
        ?? throw new ArgumentNullException(nameof(userContext));

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.GetArgument<BaseProfileRequest>(1);

        var canUpdateProfile =
            _userContext.Role == SecurityConstants.ADMIN_ROLE ||
            request.UserId == _userContext.UserId;

        if (!canUpdateProfile)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = "You are not authorized to update this Professional Profile.",
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