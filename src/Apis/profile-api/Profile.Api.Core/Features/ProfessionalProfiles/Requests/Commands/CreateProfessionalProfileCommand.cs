using Libraries.Common.Abstractions.Commands;

using Profile.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profile.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profile.Api.Core.Features.ProfessionalProfiles.Requests.Commands;

public sealed record CreateProfessionalProfileCommand(CreateProfessionalProfileRequest Request) : ICommand<ProfessionalProfileResponse>;
