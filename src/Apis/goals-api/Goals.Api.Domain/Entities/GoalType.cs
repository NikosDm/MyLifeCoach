using Libraries.Common.Entities;

namespace Goals.Api.Domain.Entities;

public class GoalType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
