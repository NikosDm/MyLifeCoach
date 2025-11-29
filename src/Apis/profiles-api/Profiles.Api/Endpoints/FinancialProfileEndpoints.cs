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
using Profiles.Api.Core.Dtos.FinancialProfiles.Requests;
using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;
using Profiles.Api.Core.Features.FinancialProfiles.Requests.Commands;
using Profiles.Api.Core.Features.FinancialProfiles.Requests.Queries;

namespace Profiles.Api.Endpoints;

public static class FinancialProfileEndpoints
{
    public static RouteGroupBuilder MapFinancialProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.FinancialProfileApiPrefix)
            .WithTags("FinancialProfiles");

        group.MapGet("/user", async (
            IUserContext userContext,
            IQueryHandler<GetFinancialProfileByUserIdQuery, FinancialProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var userId = userContext.UserId ?? throw new InvalidUserContextException(ApiErrorLiterals.InvalidUserContext);
            var result = await handler.Handle(new GetFinancialProfileByUserIdQuery(userId), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("GetAuthenticatedUserFinancialProfile")
        .WithSummary("Returns authenticated user's Financial Profile")
        .WithDescription("Returns authenticated user's Financial Profile")
        .Produces<FinancialProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("{id}", async (
            Guid id,
            IQueryHandler<GetFinancialProfileByIdQuery, FinancialProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetFinancialProfileByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("GetFinancialProfileById")
        .WithSummary("Returns a Financial Profile by its specified Id")
        .WithDescription("Returns a Financial Profile by its specified Id")
        .Produces<FinancialProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreateFinancialProfileRequest request,
            ICommandHandler<CreateFinancialProfileCommand, FinancialProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateFinancialProfileCommand(request), token);
            return TypedResults.Created($"{RouteConstants.PersonalProfileApiPrefix}/{result.ProfileId}", new { result.ProfileId });
        })
        .RequireAuthorization(ApiConstants.ProfileApiAdminPolicy)
        .WithName("CreateFinancialProfile")
        .WithSummary("Creates a new Financial Profile")
        .WithDescription("Creates a new Financial Profile")
        .Produces<Guid>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("{id}", async (
            Guid id,
            UpdateFinancialProfileRequest request,
            ICommandHandler<UpdateFinancialProfileCommand, FinancialProfileResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateFinancialProfileCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .RequireAuthorization(ApiConstants.ProfileApiUserPolicy)
        .WithName("UpdateFinancialProfile")
        .WithSummary("Updates an existing Financial Profile")
        .WithDescription("Updates an existing Financial Profile")
        .Produces<FinancialProfileResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
