using System;

using Libraries.Common.Abstractions.Queries;

using Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profiles.Api.Core.Features.ProfessionalProfiles.Requests.Queries;

public sealed record GetProfessionalProfileByUserIdQuery(Guid UserId) : IQuery<ProfessionalProfileResponse>;
