using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Entity;
using SharedKernel.Interfaces;
namespace Persistence.Data.Config;

public static class StaticConfig
{
    public static void AddDefaultQConfig<T>(this EntityTypeBuilder<T> builder) where T : Entity, IMustHaveOrganization, IMustHaveToken
    {
        builder.AddDateTimeConfig();
        builder.AddToken();
        builder.AddIdOrder();
        builder.AddOrgIdOrder();
    }
    public static void AddDateTimeConfig<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.Property(_ => _.CreatedDate).HasColumnType("DATETIME");
        builder.Property(_ => _.LastModifiedDate).HasColumnType("DATETIME");
    }
    public static void AddToken<T>(this EntityTypeBuilder<T> builder) where T : Entity, IMustHaveToken
    {
        builder.Property(_ => _.Token).HasColumnName("Token").HasColumnType("NVARCHAR(64)");
    }
    public static void AddIdOrder<T>(this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.Property("Id").HasColumnOrder(-2);
    }
    public static void AddOrgIdOrder<T>(this EntityTypeBuilder<T> builder) where T : Entity, IMustHaveOrganization
    {
        builder.Property(_ => _.OrganizationId).HasColumnName("OrganizationId").HasColumnOrder(-1);
    }
}
