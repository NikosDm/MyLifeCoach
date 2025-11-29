using System;

using Libraries.Common.Abstractions.Queries;

using Profiles.Api.Core.Dtos.FinancialProfiles.Responses;

namespace Profiles.Api.Core.Features.FinancialProfiles.Requests.Queries;

public sealed record GetFinancialProfileByUserIdQuery(Guid UserId) : IQuery<FinancialProfileResponse>;