using Libraries.Common.Abstractions.Commands;

using Profile.Api.Core.Dtos.FitnessProfiles.Requests;
using Profile.Api.Core.Dtos.FitnessProfiles.Responses;

namespace Profile.Api.Core.Features.FitnessProfiles.Requests.Commands;

public sealed record CreateFitnessProfileCommand(CreateFitnessProfileRequest Request) : ICommand<FitnessProfileResponse>;
