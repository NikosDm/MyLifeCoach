using System;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

public abstract record BasePersonalProfileRequest(
    Guid UserId,
    string FullName,
    DateTimeOffset? DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber) : BaseProfileRequest(UserId);