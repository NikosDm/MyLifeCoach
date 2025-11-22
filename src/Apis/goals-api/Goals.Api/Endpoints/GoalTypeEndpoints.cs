using System;
using System.Collections.Generic;
using System.Threading;

using Goals.Api.Constants;
using Goals.Api.Core.Dtos.GoalTypes.Requests;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Core.Features.GoalTypes.Requests.Commands;
using Goals.Api.Core.Features.GoalTypes.Requests.Queries;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Goals.Api.Endpoints;

public static class GoalTypeEndpoints
{
    public static RouteGroupBuilder MapGoalTypeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.GoalTypesApiPrefix)
            .WithTags("Goal Types")
            .RequireAuthorization(ApiConstants.GoalsApiUserPolicy);

        group.MapGet("/", async (
            IQueryHandler<GetGoalTypesQuery, IReadOnlyList<GoalTypeResponse>> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetGoalTypesQuery(), token);
            return TypedResults.Ok(result);
        })
        .WithName("GetGoalTypes")
        .WithSummary("Returns a list of all Goal Types")
        .WithDescription("Returns a list of all Goal Types")
        .Produces<IReadOnlyList<GoalTypeResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:guid}", async (
            Guid id,
            IQueryHandler<GetGoalTypeQuery, GoalTypeResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetGoalTypeQuery(id), token);
            return TypedResults.Ok(result);
        })
        .WithName("GetGoalTypeById")
        .WithSummary("Returns a goal type specified by Id")
        .WithDescription("Returns a goal type specified by Id")
        .Produces<GoalTypeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreateGoalTypeRequest request,
            ICommandHandler<CreateGoalTypeCommand, GoalTypeResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateGoalTypeCommand(request), token);
            return TypedResults.Created($"/api/goal-types/{result.Id}", new { result.Id });
        })
        .WithName("CreateGoalType")
        .WithSummary("Creates a goal type")
        .WithDescription("Creates a new goal type")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateGoalTypeRequest request,
            ICommandHandler<UpdateGoalTypeCommand, GoalTypeResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateGoalTypeCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .WithName("UpdateGoalType")
        .WithSummary("Updates a goal type")
        .WithDescription("Updates a goal type")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}", async (
            Guid id,
            ICommandHandler<DeactivateGoalTypeCommand, Guid> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new DeactivateGoalTypeCommand(id), token);
            return TypedResults.Ok(result);
        })
        .WithName("DeactivateGoalType")
        .WithSummary("Deactivates a goal type")
        .WithDescription("Deactivates / soft deletes a goal type")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
