using Goals.Api.Domain.ValueObjects;
using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class GoalType : BaseEntity
{
    public EntityName Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }

    private GoalType() { }

    private GoalType(EntityName name, string description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }

    public static GoalType Create(EntityName name, string description)
        => new(name, description);

    public void Update(EntityName name, string description, bool isActive)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
    }

    // TODO: It is debatable whether in this case 
    // a domain event should be sent or not.
    public void Deactivate() => IsActive = false;
}
