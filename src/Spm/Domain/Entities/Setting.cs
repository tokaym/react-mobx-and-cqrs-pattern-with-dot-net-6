using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class Setting : Entity
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }

    public Setting()
    {

    }

    public Setting(Guid id, string name, string value, string description) : base(id)
    {
        Id = id;
        Name = name;
        Value = value;
        Description = description;
    }
}