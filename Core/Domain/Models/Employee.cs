namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public record Employee 
{
    [Key]
    public long Id { get; set; }
    public long EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public bool Active { get; set; }
}



