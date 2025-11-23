using System;

using Libraries.Common.Abstractions.Queries;

using Profile.Api.Core.Dtos.PersonalProfiles.Responses;

namespace Profile.Api.Core.Features.PersonalProfiles.Requests.Queries;

public sealed record GetPersonalProfileByIdQuery(Guid Id) : IQuery<PersonalProfileResponse>;
