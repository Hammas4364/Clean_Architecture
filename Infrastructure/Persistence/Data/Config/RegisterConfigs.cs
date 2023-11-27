using Microsoft.EntityFrameworkCore;
using Persistence.Data.Tables;
using SharedKernel.Claims;
namespace Persistence.Data.Config;

public static class RegisterConfigs
{
    public static void ApplyConfiguration(this ModelBuilder modelBuilder, IClaims Claims)
    {
        modelBuilder.ApplyConfiguration(new T_Orgainzation_C(Claims));
    }
}
