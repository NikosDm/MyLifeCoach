using System;
using System.Collections.Generic;
using System.Threading;

using Libraries.Common.Abstractions;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;
using Libraries.Common.Constants;
using Libraries.Common.Exceptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Profile.Api.Constants;
using Profile.Api.Core.Dtos.FitnessProfiles.Requests;
using Profile.Api.Core.Dtos.FitnessProfiles.Responses;
using Profile.Api.Core.Features.FitnessProfiles.Requests.Commands;
using Profile.Api.Core.Features.FitnessProfiles.Requests.Queries;

namespace Profile.Api.Endpoints;

public static class FitnessProfileEndpoints
{
    public static RouteGroupBuilder MapFitnessProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.FitnessProfileApiPrefix)
            .WithTags("FitnessProfiles");

        group.MapGet("/user", async (
            IUserContext userContext,
            IQueryHandler<GetFitnessProfileByUserIdQuery, FitnessProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var userId = userContext.UserId ?? throw new InvalidUserContextException(ApiErrorLiterals.InvalidUserContext);
            var result = await handler.Handle(new GetFitnessProfileByUserIdQuery(userId), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("GetAuthenticatedUserFitnessProfile")
        .WithSummary("Returns authenticated user's Fitness Profile")
        .WithDescription("Returns authenticated user's Fitness Profile")
        .Produces<FitnessProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("{id}", async (
            Guid id,
            IQueryHandler<GetFitnessProfileByIdQuery, FitnessProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetFitnessProfileByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("GetFitnessProfileById")
        .WithSummary("Returns a Fitness Profile by its specified Id")
        .WithDescription("Returns a Fitness Profile by its specified Id")
        .Produces<FitnessProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreateFitnessProfileRequest request,
            ICommandHandler<CreateFitnessProfileCommand, FitnessProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateFitnessProfileCommand(request), token);
            return TypedResults.Created($"{RouteConstants.PersonalProfileApiPrefix}/{result.ProfileId}", new { result.ProfileId });
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("CreateFitnessProfile")
        .WithSummary("Creates a new Fitness Profile")
        .WithDescription("Creates a new Fitness Profile")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("{id}", async (
            Guid id,
            UpdateFitnessProfileRequest request,
            ICommandHandler<UpdateFitnessProfileCommand, FitnessProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateFitnessProfileCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("UpdateFitnessProfile")
        .WithSummary("Updates an existing Fitness Profile")
        .WithDescription("Updates an existing Fitness Profile")
        .Produces<FitnessProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
