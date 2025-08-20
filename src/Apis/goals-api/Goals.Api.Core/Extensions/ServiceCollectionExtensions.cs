using System;
using System.Collections.Generic;
using FluentValidation;
using Goals.Api.Core.Dtos.Goals.Responses;
using Goals.Api.Core.Dtos.GoalSteps.Responses;
using Goals.Api.Core.Dtos.GoalTypes.Responses;
using Goals.Api.Core.Features.Goals.Handlers.Commands;
using Goals.Api.Core.Features.Goals.Handlers.Queries;
using Goals.Api.Core.Features.Goals.Requests.Commands;
using Goals.Api.Core.Features.Goals.Requests.Queries;
using Goals.Api.Core.Features.GoalSteps.Handlers.Commands;
using Goals.Api.Core.Features.GoalSteps.Handlers.Queries;
using Goals.Api.Core.Features.GoalSteps.Requests.Commands;
using Goals.Api.Core.Features.GoalSteps.Requests.Queries;
using Goals.Api.Core.Features.GoalTypes.Handlers.Commands;
using Goals.Api.Core.Features.GoalTypes.Handlers.Queries;
using Goals.Api.Core.Features.GoalTypes.Requests.Commands;
using Goals.Api.Core.Features.GoalTypes.Requests.Queries;
using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Goals.Api.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        services
            .AddScoped<ICommandHandler<CreateGoalTypeCommand, GoalTypeResponse>, CreateGoalTypeCommandHandler>()
            .AddScoped<ICommandHandler<UpdateGoalTypeCommand, GoalTypeResponse>, UpdateGoalTypeCommandHandler>()
            .AddScoped<ICommandHandler<DeactivateGoalTypeCommand, Guid>, DeactivateGoalTypeCommandHandler>()
            .AddScoped<IQueryHandler<GetGoalTypesQuery, IReadOnlyList<GoalTypeResponse>>, GetGoalTypesQueryHandler>()
            .AddScoped<IQueryHandler<GetGoalTypeQuery, GoalTypeResponse>, GetGoalTypeQueryHandler>();

        services
            .AddScoped<ICommandHandler<CreateGoalCommand, GoalResponse>, CreateGoalCommandHandler>()
            .AddScoped<ICommandHandler<CreateGoalStepForGoalCommand, GoalStepResponse>, CreateGoalStepForGoalCommandHandler>()
            .AddScoped<ICommandHandler<UpdateGoalCommand, GoalResponse>, UpdateGoalCommandHandler>()
            .AddScoped<IQueryHandler<GetGoalsQuery, IReadOnlyList<GoalResponse>>, GetGoalsQueryHandler>()
            .AddScoped<IQueryHandler<GetGoalByIdQuery, GoalResponse>, GetGoalByIdQueryHandler>();

        services
            .AddScoped<ICommandHandler<UpdateGoalStepCommand, GoalStepResponse>, UpdateGoalStepCommandHandler>()
            .AddScoped<IQueryHandler<GetGoalStepByIdQuery, GoalStepResponse>, GetGoalStepByIdQueryHandler>();

        return services;
    }
}
