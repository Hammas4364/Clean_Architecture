using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Domain.ViewModels;


public record Add_Org_Dto(string OrgName, string OrgDetail, bool Active);
public record Update_Org_Dto(int Id, string OrgName, string OrgDetail, bool Active);
public record Delete_Org_Dto(int Id);

public class OrganizationViewModel
{
    public string? OrgName { get; set; }
    public string? OrgDetail { get; set; }
    public bool Active { get; set; }
}
