using System;

using Libraries.Common.Abstractions.Commands;

using Profiles.Api.Core.Dtos.PersonalProfiles.Requests;
using Profiles.Api.Core.Dtos.PersonalProfiles.Responses;

namespace Profiles.Api.Core.Features.PersonalProfiles.Requests.Commands;

public sealed record UpdatePersonalProfileCommand(Guid Id, UpdatePersonalProfileRequest Request) : ICommand<PersonalProfileResponse>;