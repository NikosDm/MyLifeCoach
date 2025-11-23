using Libraries.Common.Abstractions.Commands;

using Profile.Api.Core.Dtos.PersonalProfiles.Requests;
using Profile.Api.Core.Dtos.PersonalProfiles.Responses;

namespace Profile.Api.Core.Features.PersonalProfiles.Requests.Commands;

public sealed record CreatePersonalProfileCommand(CreatePersonalProfileRequest Request) : ICommand<PersonalProfileResponse>;
