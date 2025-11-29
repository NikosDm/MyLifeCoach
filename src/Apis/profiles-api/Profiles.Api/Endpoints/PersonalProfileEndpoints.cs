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

using Profiles.Api.Constants;
using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;
using Profiles.Api.Core.Features.PersonalProfiles.Requests.Commands;
using Profiles.Api.Core.Features.PersonalProfiles.Requests.Queries;

namespace Profiles.Api.Endpoints;

public static class PersonalProfileEndpoints
{
    public static RouteGroupBuilder MapPersonalProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.PersonalProfileApiPrefix)
            .WithTags("PersonalProfiles");

        group.MapGet("/", async (
            IQueryHandler<GetPersonalProfilesQuery, IReadOnlyList<PersonalProfileResponse>> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetPersonalProfilesQuery(), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("GetPersonalProfiles")
        .WithSummary("Returns a list of all Personal Profiles")
        .WithDescription("Returns a list of all Personal Profiles")
        .Produces<IReadOnlyList<PersonalProfileResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/user", async (
            IUserContext userContext,
            IQueryHandler<GetPersonalProfileByUserIdQuery, PersonalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var userId = userContext.UserId ?? throw new InvalidUserContextException(ApiErrorLiterals.InvalidUserContext);
            var result = await handler.Handle(new GetPersonalProfileByUserIdQuery(userId), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("GetAuthenticatedUserPersonalProfile")
        .WithSummary("Returns authenticated user's Personal Profile")
        .WithDescription("Returns authenticated user's Personal Profile")
        .Produces<PersonalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("{id}", async (
            Guid id,
            IQueryHandler<GetPersonalProfileByIdQuery, PersonalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetPersonalProfileByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("GetPersonalProfileById")
        .WithSummary("Returns a Personal Profile by its specified Id")
        .WithDescription("Returns a Personal Profile by its specified Id")
        .Produces<PersonalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreatePersonalProfileRequest request,
            ICommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreatePersonalProfileCommand(request), token);
            return TypedResults.Created($"{RouteConstants.PersonalProfileApiPrefix}/{result.ProfileId}", new { result.ProfileId });
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("CreatePersonalProfile")
        .WithSummary("Creates a new Personal Profile")
        .WithDescription("Creates a new Personal Profile")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("{id}", async (
            Guid id,
            UpdatePersonalProfileRequest request,
            ICommandHandler<UpdatePersonalProfileCommand, PersonalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdatePersonalProfileCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("UpdatePersonalProfile")
        .WithSummary("Updates an existing Personal Profile")
        .WithDescription("Updates an existing Personal Profile")
        .Produces<PersonalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
