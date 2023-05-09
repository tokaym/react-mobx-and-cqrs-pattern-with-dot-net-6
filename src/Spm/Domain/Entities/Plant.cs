using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class Plant : Entity
{
    public string? Code { get; set; }
    public string? Name { get; set; }

    public Plant()
    {

    }

    public Plant(Guid id, string code, string name) : base(id)
    {
        Id = id;
        Code = code;
        Name = name;
    }
}