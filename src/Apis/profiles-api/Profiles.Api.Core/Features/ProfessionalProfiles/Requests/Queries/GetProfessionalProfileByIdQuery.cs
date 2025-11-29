using System;

using Libraries.Common.Abstractions.Queries;

using Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profiles.Api.Core.Features.ProfessionalProfiles.Requests.Queries;

public sealed record GetProfessionalProfileByIdQuery(Guid Id) : IQuery<ProfessionalProfileResponse>;
