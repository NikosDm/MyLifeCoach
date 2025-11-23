using System;
using System.Collections.Generic;
using System.Threading;

using Goals.Api.Constants;
using Goals.Api.Core.Dtos.Goals.Requests;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Extensions;
using Goals.Api.Core.Features.Goals.Requests.Commands;
using Goals.Api.Core.Features.Goals.Requests.Queries;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Goals.Api.Endpoints;

public static class GoalEndpoints
{
    public static RouteGroupBuilder MapGoalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup(RouteConstants.GoalsApiPrefix)
            .WithTags("Goals")
            .RequireAuthorization(ApiConstants.GoalsApiUserPolicy);

        group.MapGet("/", async (
            IQueryHandler<GetGoalsQuery, IReadOnlyList<GoalResponse>> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetGoalsQuery(), token);
            return TypedResults.Ok(result);
        })
        .WithName("GetGoals")
        .WithSummary("Returns a list of all Goals")
        .WithDescription("Returns a list of all Goals")
        .Produces<IReadOnlyList<GoalResponse>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapGet("/{id:guid}", async (
            Guid id,
            IQueryHandler<GetGoalByIdQuery, GoalResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetGoalByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .WithName("GetGoalById")
        .WithSummary("Returns a Goal specified by its Id")
        .WithDescription("Returns a Goal specified by its Id")
        .Produces<GoalResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/", async (
            CreateGoalRequest request,
            ICommandHandler<CreateGoalCommand, GoalResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateGoalCommand(request), token);
            return TypedResults.Created($"{RouteConstants.GoalsApiPrefix}/{result.Id}", new { result.Id });
        })
        .WithName("CreateGoal")
        .WithSummary("Creates a new goal")
        .WithDescription("Creates a new goal")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/{id:guid}/goal-step", async (
            Guid id,
            CreateGoalStepRequest request,
            ICommandHandler<CreateGoalStepForGoalCommand, GoalStepResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new CreateGoalStepForGoalCommand(request.ToRequestForGoal(id)), token);
            return TypedResults.Created($"{RouteConstants.GoalStepsApiPrefix}/{result.Id}", new { result.Id });
        })
        .WithName("CreateStepForGoal")
        .WithSummary("Creates a new step for a goal")
        .WithDescription("Creates a new step for a goal specified by its id")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateGoalRequest request,
            ICommandHandler<UpdateGoalCommand, GoalResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateGoalCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .WithName("UpdateGoal")
        .WithSummary("Updates a goal")
        .WithDescription("Updates a goal")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
