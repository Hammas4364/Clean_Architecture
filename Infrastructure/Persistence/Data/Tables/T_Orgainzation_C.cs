using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Claims;
namespace Persistence.Data.Tables;

public record T_Orgainzation_C(IClaims Claims) : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(_ => _.OrgId)
            .HasColumnName("OrgId")
            .HasColumnOrder(0);

        builder.Property(_ => _.OrgName)
            .HasColumnName("OrgName")
            .HasColumnType("NVARCHAR(64)")
            .HasColumnOrder(1);

        builder.Property(_ => _.OrgDetail)
            .HasColumnName("OrgDetail")
            .HasColumnType("NVARCHAR(200)")
            .HasColumnOrder(2);

        builder.Property(_ => _.Active)
           .HasColumnName("Active")
           .HasColumnType("BIT")
           .HasColumnOrder(3);

        //builder.HasIndex(_ => _.OrgId).IsClustered();
        builder.HasIndex(_ => _.OrgName).IsUnique();

        //builder.HasQueryFilter(o => o.OrgId == Claims.OrganizationId);
        //builder.Property(_ => _.Active)
        //    .HasConversion(_ => _.ToString(), _ => (ControllerState)Enum.Parse(typeof(ControllerState), _))
        //    .HasColumnName("State")
        //    .HasColumnType("VARCHAR(25)");
    }
}
