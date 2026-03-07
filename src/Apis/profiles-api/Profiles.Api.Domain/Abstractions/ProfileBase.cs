using System;
using System.ComponentModel.DataAnnotations.Schema;

using Libraries.Common.Entities;

using Profiles.Api.Domain.Enums;
using Profiles.Api.Domain.Models;

namespace Profiles.Api.Domain.Abstractions;

public abstract class ProfileBase : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    [NotMapped]
    public ProfileType Type { get; protected set; }
}
