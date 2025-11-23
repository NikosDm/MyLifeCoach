using System;

using Libraries.Common.Abstractions.Commands;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profile.Api.Core.Features.ProfessionalProfiles.Requests.Commands;

public sealed record UpdateProfessionalProfileCommand(Guid Id, UpdateProfessionalProfileRequest Request)
    : ICommand<ProfessionalProfileResponse>;
