using Microsoft.AspNetCore.Http;
using SharedKernel.Constants;
using SharedKernel.Exceptions;
namespace SharedKernel.Claims;

public record Claims(IHttpContextAccessor HttpContextAccessor) : IClaims
{
    private long _organizationId;
    string? _operator;
    string? _ipAddress;
    string? _timeZone;

    public long OrganizationId
    {
        get
        {
            var claimOrgId = HttpContextAccessor.HttpContext?.User.Claims?.Where(x => x.Type == "zoneinfo")?.FirstOrDefault()?.Value;
            if (claimOrgId is null)
            {
                if (Config.EnableClaimsDefaultValue)
                    claimOrgId = "1";
                else
                    throw new AppException("There must be an Organization Id in claims");
            }
            _organizationId = long.Parse(claimOrgId);
            return _organizationId;
        }
    }
    public string Operator
    {
        get
        {
            var claimOperator = HttpContextAccessor.HttpContext?.User.Claims?.Where(x => x.Type == "givenname")?.FirstOrDefault()?.Value;
            if (claimOperator is null)
            {
                if (Config.EnableClaimsDefaultValue)
                    claimOperator = "op1";
                else
                    throw new AppException("There must be an Given Name (Operator) in claims");
            }
            return _operator = claimOperator;
        }
    }
    public string IPAddress
    {
        get
        {
            var claimOperatorIP = HttpContextAccessor.HttpContext?.User.Claims?.Where(x => x.Type == "ipAddress")?.FirstOrDefault()?.Value;
            if (claimOperatorIP is null)
            {
                if (Config.EnableClaimsDefaultValue)
                    claimOperatorIP = "op1:local";
                else
                    throw new AppException("There must be an Given Name (Operator) in claims");
            }
            return _ipAddress = claimOperatorIP;
        }
    }
    public string TimeZone
    {
        get
        {
            var TimeZone = HttpContextAccessor.HttpContext?.User.Claims?.Where(x => x.Type == "time_zone")?.FirstOrDefault()?.Value;
            if (TimeZone is null)
            {
                if (Config.EnableClaimsDefaultValue)
                    TimeZone = "PKT-5";
                else
                    throw new AppException("There must be an Given Time Zone in claims");
            }
            if (!string.IsNullOrEmpty(TimeZone) && TimeZone.Contains(":"))
            {
                var splited = TimeZone!.Split(":");
                return _timeZone = splited.Last();

            }
            return TimeZone;
        }
    }
}
