using System;

using Libraries.Common.Abstractions.Queries;

using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

namespace Profiles.Api.Core.Features.PersonalProfiles.Requests.Queries;

public sealed record GetPersonalProfileByIdQuery(Guid Id) : IQuery<PersonalProfileResponse>;
