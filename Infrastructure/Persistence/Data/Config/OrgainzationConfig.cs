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

        builder.Property(_ => _.CreatedDate)
           .HasColumnName("CreatedDate")
           .HasColumnType("DATETIME")
           .HasColumnOrder(6);

        builder.Property(_ => _.ModifiedDate)
           .HasColumnName("ModifiedDate")
           .HasColumnType("DATETIME")
           .HasColumnOrder(7);

        builder.HasIndex(_ => _.OrgName).IsUnique();
    }
}

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(_ => _.Id)
             .HasColumnName("Id")
             .HasColumnOrder(1);

        builder.Property(_ => _.EmployeeCode)
              .HasColumnName("EmployeeCode")
              .HasColumnType("BITINT")
              .HasColumnOrder(2);

        builder.Property(_ => _.EmployeeName)
            .HasColumnName("EmployeeName")
            .HasColumnType("NVARCHAR(200)")
            .HasColumnOrder(3);

        builder.Property(_ => _.Active)
           .HasColumnName("Active")
           .HasColumnType("BIT")
           .HasColumnOrder(4);

        builder.HasIndex(_ => _.EmployeeName).IsUnique();
    }
}


