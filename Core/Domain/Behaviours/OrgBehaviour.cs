namespace Domain.Models;
using Domain.Behaviours.Common;
using Domain.Events;
using Domain.ViewModels;

public partial record Organization
{
    public static Organization Create(Add_Org_Dto command) => new Organization(command.OrgName, command.OrgDetail, command.Active);

    public void Update(Update_Org_Dto dto)
    {
        if (!Active.Equals(dto.Active))
        {
            var ev = new Org_Name_Updated(Id, OrgName!, dto.OrgName);
            RegisterEvent(ev);
        }

        if (!Active.Equals(dto.Active))
        {
            var ev = new Org_Active_Updated(Id, Active, dto.Active);
            RegisterEvent(ev);
        }
    }

    public Deleted<Organization> Delete()
    {
        var e = new Org_Deleted(this);
        RegisterEvent(e);
        return new Deleted<Organization>(this, e);
    }
}

