using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data.Config;


public class OrgainzationConfig : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(_ => _.Id)
             .HasColumnName("Id")
             .HasColumnOrder(1);

        builder.Property(_ => _.OrgName)
              .HasColumnName("OrgName")
              .HasColumnType("NVARCHAR(64)")
              .HasColumnOrder(2);

        builder.Property(_ => _.OrgDetail)
            .HasColumnName("OrgDetail")
            .HasColumnType("NVARCHAR(200)")
            .HasColumnOrder(3);

        builder.Property(_ => _.Active)
           .HasColumnName("Active")
           .HasColumnType("BIT")
           .HasColumnOrder(4);

        builder.HasIndex(_ => _.OrgName).IsUnique();
    }
}


