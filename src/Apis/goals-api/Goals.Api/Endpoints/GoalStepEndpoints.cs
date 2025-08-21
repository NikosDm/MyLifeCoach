using System;
using System.Threading;
using Goals.Api.Core.Dtos.GoalSteps.Requests;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Features.GoalSteps.Requests.Commands;
using Goals.Api.Core.Features.GoalSteps.Requests.Queries;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Goals.Api.Endpoints;

public static class GoalStepEndpoints
{
    public static RouteGroupBuilder MapGoalStepEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/goal-steps")
            .WithTags("Goal Steps")
            .RequireAuthorization("GoalsApiUser");

        group.MapGet("/{id:guid}", async (
            Guid id,
            IQueryHandler<GetGoalStepByIdQuery, GoalStepResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new GetGoalStepByIdQuery(id), token);
            return TypedResults.Ok(result);
        })
        .WithName("GetGoalStepById")
        .WithSummary("Returns a Goal step specified by its Id")
        .WithDescription("Returns a Goal step specified by its Id")
        .Produces<GoalStepResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateGoalStepRequest request,
            ICommandHandler<UpdateGoalStepCommand, GoalStepResponse> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new UpdateGoalStepCommand(id, request), token);
            return TypedResults.Ok(result);
        })
        .WithName("UpdateGoalStep")
        .WithSummary("Updates a goal step")
        .WithDescription("Updates a goal step")
        .Produces<GoalStepResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapDelete("/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteGoalStepCommand, Guid> handler,
            CancellationToken token = default) =>
        {
            var result = await handler.Handle(new DeleteGoalStepCommand(id), token);
            return TypedResults.Ok(result);
        })
        .WithName("DeleteGoalStep")
        .WithSummary("Deletes a goal step")
        .WithDescription("Soft deletes a goal step")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }
}
