namespace Domain.ViewModels;

public record Add_Org_Dto(string OrgName, string OrgDetail, bool Active);
public record Update_Org_Dto(long Id, string OrgName, string OrgDetail, bool Active);
public record Delete_Org_Dto(long Id);
public class Get_Ord_Dto
{
    public long OrgId { get; init; }
    public string OrgName { get; init; } = string.Empty;
    public string OrgDetail { get; init; } = string.Empty;
    public bool Active { get; init; }
}

