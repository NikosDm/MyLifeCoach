using System;

namespace Profiles.Api.Core.Dtos.PersonalProfiles.Requests;

public record BasePersonalProfileRequest(
    string FullName,
    DateTimeOffset DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber);