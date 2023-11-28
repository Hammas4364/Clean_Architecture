namespace Domain.Models;

using Domain.Behaviours.Common;
using Domain.Events;
using SharedKernel.AggregateRoot;
using SharedKernel.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


public partial record Organization : AggregateRoot<long>
{
    Organization() { }
    
    Organization(string OrgName, string OrgDetail, bool Active, string Token)
    {
        var e = new Org_Added(OrgName, OrgDetail, Active, Token);
        RegisterEvent(e);
    }

    public string? OrgName { get; private set; } = default!;
    public string? OrgDetail { get; private set; } = default!;
    public bool Active { get; private set; } = default!;
    public string Token { get; private set; } = string.Empty;
}

