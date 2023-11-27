namespace SharedKernel.Constants;

public static class Config
{
    public const bool IsDebugAutoWrapper = true;

    public const bool EnableClaimsDefaultValue = false;
    
    public const bool DoMigrate = true;

    public const int CommandTimeout = 100000;

    public const string ApiVersion = "1.0";

    public const string GitHubBranch = "4.5-b4 Build 1";

    public const string DefaultCorsPolicyName = "Default CORS Policy";
}