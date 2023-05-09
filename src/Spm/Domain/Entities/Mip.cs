using Core.Persistence;
using Core.Security.Entities;

namespace Domain.Entities;

public class Mip : Entity
{
    public string? Code { get; set; }
    public string? CD { get; set; }

    public Mip()
    {

    }

    public Mip(Guid id, string code, string cd) : base(id)
    {
        Id = id;
        Code = code;
        CD = cd;
    }
}