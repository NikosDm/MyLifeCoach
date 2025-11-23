using System;
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
using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Commands;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Queries;

namespace Profile.Api.Endpoints;

public static class ProfessionalProfileEndpoints
{
    public static RouteGroupBuilder MapProfessionalProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.ProfessionalProfileApiPrefix)
            .WithTags("ProfessionalProfiles");

        group.MapGet("/user", async (
            IUserContext userContext,
            IQueryHandler<GetProfessionalProfileByUserIdQuery, ProfessionalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var userId = userContext.UserId ?? throw new InvalidUserContextException(ApiErrorLiterals.InvalidUserContext);
            var result = await handler.Handle(new GetProfessionalProfileByUserIdQuery(userId), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("GetAuthenticatedUserProfessionalProfile")
        .WithSummary("Returns authenticated user's Professional Profile")
        .WithDescription("Returns authenticated user's Professional Profile")
        .Produces<ProfessionalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("{id}", async (
            Guid id,
            IQueryHandler<GetProfessionalProfileByIdQuery, ProfessionalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetProfessionalProfileByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("GetProfessionalProfileById")
        .WithSummary("Returns a Professional Profile by its specified Id")
        .WithDescription("Returns a Professional Profile by its specified Id")
        .Produces<ProfessionalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreateProfessionalProfileRequest request,
            ICommandHandler<CreateProfessionalProfileCommand, ProfessionalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateProfessionalProfileCommand(request), token);
            return TypedResults.Created($"{RouteConstants.PersonalProfileApiPrefix}/{result.ProfileId}", new { result.ProfileId });
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("CreateProfessionalProfile")
        .WithSummary("Creates a new Professional Profile")
        .WithDescription("Creates a new Professional Profile")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("{id}", async (
            Guid id,
            UpdateProfessionalProfileRequest request,
            ICommandHandler<UpdateProfessionalProfileCommand, ProfessionalProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateProfessionalProfileCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("UpdateProfessionalProfile")
        .WithSummary("Updates an existing Professional Profile")
        .WithDescription("Updates an existing Professional Profile")
        .Produces<ProfessionalProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
