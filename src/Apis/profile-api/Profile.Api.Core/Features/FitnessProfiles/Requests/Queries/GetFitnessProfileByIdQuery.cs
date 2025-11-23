using System;

using Libraries.Common.Abstractions.Queries;

using Profile.Api.Core.Dtos.FitnessProfiles.Responses;

namespace Profile.Api.Core.Features.FitnessProfiles.Requests.Queries;

public sealed record GetFitnessProfileByIdQuery(Guid Id) : IQuery<FitnessProfileResponse>;
