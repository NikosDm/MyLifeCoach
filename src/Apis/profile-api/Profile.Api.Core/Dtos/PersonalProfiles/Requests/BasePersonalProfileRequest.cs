using System;

namespace Profile.Api.Core.Dtos.PersonalProfiles.Requests;

public record BasePersonalProfileRequest(
    string FullName,
    DateTimeOffset DateOfBirth,
    string City,
    string Country,
    string Email,
    string PhoneNumber);