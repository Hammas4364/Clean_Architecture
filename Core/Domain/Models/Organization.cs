using System.ComponentModel.DataAnnotations;
namespace Domain.Models;

public class Organization 
{
    [Key]
    public long Id { get; set; } 
    public string? OrgName { get; set; } 
    public string? OrgDetail { get; set; }
    public string? Token { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; } 
    public bool Active { get; set; } = default!;
}

