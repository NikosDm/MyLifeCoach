using System;

using Libraries.Common.Abstractions.Queries;

using Profiles.Api.Core.Dtos.FitnessProfiles.Responses;

namespace Profiles.Api.Core.Features.FitnessProfiles.Requests.Queries;

public sealed record GetFitnessProfileByIdQuery(Guid Id) : IQuery<FitnessProfileResponse>;
