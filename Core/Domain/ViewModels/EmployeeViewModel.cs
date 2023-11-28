namespace Domain.ViewModels;

public record AddEmployee
{
    public int EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
}
