namespace Domain.ViewModels;

public class Get_EmpByOrgId_Dto
{
    public long Id { get; set; }
    public long EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public long OrgId { get; set; }
    public string? OrgName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
}
public record Emp_Dto
{
    public long Id { get; set; }
    public long EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public long OrgId { get; set; }
    public string? OrgName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
}

public record Add_Emp_Dto
{
    public int EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public long OrgId { get; set; }
}

public record Update_Emp_Dto
{
    public long Id { get; set; }
    public long EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public long OrgId { get; set; }
    public bool Active { get; set; }
}

public record Delete_Emp_Dto
{
    public long EmpCode { get; set; }
}
