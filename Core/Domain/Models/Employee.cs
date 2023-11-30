namespace Domain.Models;
using System.ComponentModel.DataAnnotations;

public record Employee 
{
    [Key]
    public long Id { get; set; }
    public long EmployeeCode { get; set; }
    public string? EmployeeName { get; set; }
    public long OrgId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool Active { get; set; }
}



