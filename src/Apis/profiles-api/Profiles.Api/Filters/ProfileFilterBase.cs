using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Profiles.Api.Core.Abstractions.Services;

namespace Profiles.Api.Filters;

public abstract class ProfileFilterBase(IAuthorizeAccessService authorizeAccessService)
    : IEndpointFilter
{
    private readonly IAuthorizeAccessService _authorizeAccessService = authorizeAccessService
        ?? throw new ArgumentNullException(nameof(authorizeAccessService));

    protected abstract Guid GetUserId(
        EndpointFilterInvocationContext context);

    protected virtual string ForbiddenMessage =>
        "You are not authorized to access this resource.";

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var userId = GetUserId(context);

        if (!_authorizeAccessService.CanUserAccessProfileAction(userId))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = ForbiddenMessage,
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