namespace SharedKernel.Claims;

public interface IClaims
{
    long OrganizationId { get; }
    string Operator { get; }
    string IPAddress { get; }
    string TimeZone { get; }
}
