using System;

using Libraries.Common.Abstractions.Commands;

using Profiles.Api.Core.Dtos.FitnessProfiles.Requests;
using Profiles.Api.Core.Dtos.FitnessProfiles.Responses;

namespace Profiles.Api.Core.Features.FitnessProfiles.Requests.Commands;

public sealed record UpdateFitnessProfileCommand(Guid Id, UpdateFitnessProfileRequest Request)
    : ICommand<FitnessProfileResponse>;
