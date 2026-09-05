using System;
using System.Threading.Tasks;

using Libraries.Common.Abstractions;
using Libraries.Common.Constants;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Profiles.Api.Filters;

public sealed class ProfileInformationFilter(IUserContext userContext) : IEndpointFilter
{
    private readonly IUserContext _userContext = userContext
        ?? throw new ArgumentNullException(nameof(userContext));

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var requestedUserId = context.GetArgument<Guid>(0);

        var canAccessProfile =
            _userContext.Role == SecurityConstants.ADMIN_ROLE ||
            requestedUserId == _userContext.UserId;

        if (!canAccessProfile)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = "You are not authorized to access this resource.",
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