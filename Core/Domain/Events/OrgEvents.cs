using Domain.Models;
using SharedKernel.Interfaces;
namespace Domain.Events;

internal record Org_Added(string OrgName, string OrgDetail, bool Active) : IDomainEvent;
internal record Org_Name_Updated(long Id, string Old, string New) : IDomainEvent;
internal record Org_Active_Updated(long Id, bool Old, bool New) : IDomainEvent;
internal record Org_Deleted(Organization Org) : IDeleteDomainEvent;
