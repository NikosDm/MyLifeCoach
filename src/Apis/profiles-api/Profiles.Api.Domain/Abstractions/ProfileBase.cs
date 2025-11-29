using System;
using System.ComponentModel.DataAnnotations.Schema;

using Libraries.Common.Entities;

using Profiles.Api.Domain.Enums;

namespace Profiles.Api.Domain.Abstractions;

public abstract class ProfileBase : BaseEntity
{
    public Guid UserId { get; set; }

    [NotMapped]
    public ProfileType Type { get; protected set; }
}
