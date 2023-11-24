namespace SharedKernel.Helpers;

public record Audit
{
    public Audit() { }
    public string Operator { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public long OrganizationId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Module { get; set; } = string.Empty;
    public object Data { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
}