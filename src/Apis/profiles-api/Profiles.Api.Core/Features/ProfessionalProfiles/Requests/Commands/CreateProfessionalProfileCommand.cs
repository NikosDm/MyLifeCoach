using Libraries.Common.Abstractions.Commands;

using Profiles.Api.Core.Dtos.ProfessionalProfiles.Requests;
using Profiles.Api.Core.Dtos.ProfessionalProfiles.Responses;

namespace Profiles.Api.Core.Features.ProfessionalProfiles.Requests.Commands;

public sealed record CreateProfessionalProfileCommand(CreateProfessionalProfileRequest Request) : ICommand<ProfessionalProfileResponse>;
