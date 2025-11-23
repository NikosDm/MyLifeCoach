using System;

using Libraries.Common.Abstractions.Queries;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profile.Api.Core.Features.ProfessionalProfiles.Requests.Queries;

public sealed record GetProfessionalProfileByIdQuery(Guid Id) : IQuery<ProfessionalProfileResponse>;
