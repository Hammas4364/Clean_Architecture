namespace Domain.Models;
using Domain.Events;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;

public partial record Organization
{
    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case
                Org_Added e:
                Apply(e);
                break;
            case
                Org_Name_Updated e:
                Apply(e);
                break;
            case
                Org_Active_Updated e:
                Apply(e);
                break;
            case
                Org_Deleted e:
                break;
            default:
                throw AppExceptions.EventsExceptions.EventCantBeAddedInWhenMethod;
        }
    }

    private void Apply(Org_Added e)
    {
        OrgName = e.OrgName;
        OrgDetail = e.OrgDetail;
        Active = e.Active;
        Token = e.Token;
    }

    private void Apply(Org_Name_Updated ev)
    {
        OrgName = ev.New;
    }

    private void Apply(Org_Active_Updated ev)
    {
        Active = ev.New;
    }
}
