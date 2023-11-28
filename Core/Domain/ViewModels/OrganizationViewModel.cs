namespace Domain.ViewModels;

public record Add_Org_Dto
{
    public string? OrgName { get; set; }
    public string? OrgDetail { get; set; }
}

public record Update_Org_Dto
{
    public long Id { get; set; }
    public string? OrgName { get; set; }
    public string? OrgDetail { get; set; }
    public bool Active { get; set; }
}

public record Delete_Org_Dto
{
    public long Id { get; set; }
}

public class Get_Org_Dto
{
    public long Id { get; init; }
    public string OrgName { get; init; } = string.Empty;
    public string OrgDetail { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public bool Active { get; init; }
    public string CreatedDate { get; init; } = string.Empty;
    public string ModifiedDate { get; init; } = string.Empty;
}

public class Get_OrgById_Dto
{
    public long Id { get; init; }
    public string OrgName { get; init; } = string.Empty;
    public string OrgDetail { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
    public string CreatedDate { get; init; } = string.Empty;
    public string ModifiedDate { get; init; } = string.Empty;
    public bool Active { get; init; }
}



