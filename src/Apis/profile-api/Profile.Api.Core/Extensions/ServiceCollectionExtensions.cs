using FluentValidation;

using Libraries.Common.Abstractions.Commands;
using Libraries.Common.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;

using Profile.Api.Core.Features.PersonalProfiles.Handlers.Commands;
using Profile.Api.Core.Features.PersonalProfiles.Handlers.Queries;
using Profile.Api.Core.Features.PersonalProfiles.Requests.Commands;
using Profile.Api.Core.Features.PersonalProfiles.Requests.Queries;
using Profile.Api.Core.Dtos.PersonalProfiles.Responses;

using Profile.Api.Core.Features.FitnessProfiles.Handlers.Commands;
using Profile.Api.Core.Features.FitnessProfiles.Handlers.Queries;
using Profile.Api.Core.Features.FitnessProfiles.Requests.Commands;
using Profile.Api.Core.Features.FitnessProfiles.Requests.Queries;
using Profile.Api.Core.Dtos.FitnessProfiles.Responses;

using Profile.Api.Core.Features.FinancialProfiles.Handlers.Commands;
using Profile.Api.Core.Features.FinancialProfiles.Handlers.Queries;
using Profile.Api.Core.Features.FinancialProfiles.Requests.Commands;
using Profile.Api.Core.Features.FinancialProfiles.Requests.Queries;
using Profile.Api.Core.Dtos.FinancialProfiles.Responses;

using Profile.Api.Core.Features.ProfessionalProfiles.Handlers.Commands;
using Profile.Api.Core.Features.ProfessionalProfiles.Handlers.Queries;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Commands;
using Profile.Api.Core.Features.ProfessionalProfiles.Requests.Queries;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profile.Api.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        services
            .AddScoped<ICommandHandler<CreatePersonalProfileCommand, PersonalProfileResponse>, CreatePersonalProfileCommandHandler>()
            .AddScoped<ICommandHandler<UpdatePersonalProfileCommand, PersonalProfileResponse>, UpdatePersonalProfileCommandHandler>()
            .AddScoped<IQueryHandler<GetPersonalProfilesQuery, IReadOnlyList<PersonalProfileResponse>>, GetPersonalProfilesQueryHandler>()
            .AddScoped<IQueryHandler<GetPersonalProfileByUserIdQuery, PersonalProfileResponse>, GetPersonalProfileByUserIdQueryHandler>()
            .AddScoped<IQueryHandler<GetPersonalProfileByIdQuery, PersonalProfileResponse>, GetPersonalProfileByIdQueryHandler>();

        services
            .AddScoped<ICommandHandler<CreateFitnessProfileCommand, FitnessProfileResponse>, CreateFitnessProfileCommandHandler>()
            .AddScoped<ICommandHandler<UpdateFitnessProfileCommand, FitnessProfileResponse>, UpdateFitnessProfileCommandHandler>()
            .AddScoped<IQueryHandler<GetFitnessProfileByUserIdQuery, FitnessProfileResponse>, GetFitnessProfileByUserIdQueryHandler>()
            .AddScoped<IQueryHandler<GetFitnessProfileByIdQuery, FitnessProfileResponse>, GetFitnessProfileByIdQueryHandler>();

        services
            .AddScoped<ICommandHandler<CreateFinancialProfileCommand, FinancialProfileResponse>, CreateFinancialProfileCommandHandler>()
            .AddScoped<ICommandHandler<UpdateFinancialProfileCommand, FinancialProfileResponse>, UpdateFinancialProfileCommandHandler>()
            .AddScoped<IQueryHandler<GetFinancialProfileByUserIdQuery, FinancialProfileResponse>, GetFinancialProfileByUserIdQueryHandler>()
            .AddScoped<IQueryHandler<GetFinancialProfileByIdQuery, FinancialProfileResponse>, GetFinancialProfileByIdQueryHandler>();

        services
            .AddScoped<ICommandHandler<CreateProfessionalProfileCommand, ProfessionalProfileResponse>, CreateProfessionalProfileCommandHandler>()
            .AddScoped<ICommandHandler<UpdateProfessionalProfileCommand, ProfessionalProfileResponse>, UpdateProfessionalProfileCommandHandler>()
            .AddScoped<IQueryHandler<GetProfessionalProfileByUserIdQuery, ProfessionalProfileResponse>, GetProfessionalProfileByUserIdQueryHandler>()
            .AddScoped<IQueryHandler<GetProfessionalProfileByIdQuery, ProfessionalProfileResponse>, GetProfessionalProfileByIdQueryHandler>();

        return services;
    }
}