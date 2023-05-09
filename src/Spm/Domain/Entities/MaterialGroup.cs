using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class MaterialGroup : Entity
{
    public string? MaterialSKU { get; set; }
    public string? GroupName { get; set; }

    public MaterialGroup()
    {

    }

    public MaterialGroup(Guid id, string materialSKU, string groupName ) : base(id)
    {
        Id = id;
        MaterialSKU = materialSKU;
        GroupName = groupName;
    }
}