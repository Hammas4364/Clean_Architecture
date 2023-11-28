using Newtonsoft.Json.Linq;

namespace Domain.ViewModels;

public record Add_Org_Dto(string OrgName, string OrgDetail, bool Active, string Token);
public record Update_Org_Dto(long Id, string OrgName, string OrgDetail, bool Active);
public record Delete_Org_Dto(long Id);
public class Get_Ord_Dto
{
    public long Id { get; init; }
    public string OrgName { get; init; } = string.Empty;
    public string OrgDetail { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public bool Active { get; init; }
    public string CreatedDate { get; init; } = string.Empty;
    public string LastModifiedDate { get; init; } = string.Empty;
}

public class Get_OrdById_Dto
{
    public long OrgId { get; init; }
    public string OrgName { get; init; } = string.Empty;
    public string OrgDetail { get; init; } = string.Empty;
    public bool Active { get; init; }
}


