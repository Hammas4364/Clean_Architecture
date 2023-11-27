namespace Domain.Models;
using Domain.Events;
using SharedKernel.AggregateRoot;
using SharedKernel.Interfaces;


public partial record Organization : AggregateRoot<long>, IMustHaveToken
{
    Organization() { }

    Organization(string OrgName, string OrgDetail, bool Active)
    {
        var e = new Org_Added(OrgName, OrgDetail, Active);
        RegisterEvent(e);
    }

    public string Token { get; private set; } = string.Empty;
    public string? OrgName { get; private set; } = default!;
    public string? OrgDetail { get; private set; } = default!;
    public bool Active { get; private set; } = default!;
}

