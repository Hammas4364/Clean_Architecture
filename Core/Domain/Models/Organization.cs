namespace Domain.Models;

public class Organization
{
    public int OrgId { get; set; }
    public string? OrgName { get; set; }
    public string? OrgDetail { get; set; }
    public bool Active { get; set; }
}
