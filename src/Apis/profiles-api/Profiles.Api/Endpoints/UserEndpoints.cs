using System;
using System.Threading;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Profiles.Api.Constants;
using Profiles.Api.Core.Dtos.Users.Requests;
using Profiles.Api.Core.Dtos.Users.Responses;
using Profiles.Api.Core.Features.Users.Requests.Commands;

using Libraries.Common.Abstractions.Commands;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.UserApiPrefix)
            .WithTags("Users");

        group.MapPut("{id}", async (
            Guid id,
            ChangeUserStatusRequest request,
            ICommandHandler<ChangeUserStatusRequestCommand, UserResponse> handler,
            CancellationToken token = default) =>
        {
            var cmd = new ChangeUserStatusRequestCommand(id, request);
            var result = await handler.HandleAsync(cmd, token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("ChangeUserStatus")
        .WithSummary("Change a user's active status")
        .WithDescription("Activates or deactivates a user by id")
        .Produces<UserResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}