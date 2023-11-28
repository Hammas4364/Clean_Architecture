using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orgainzations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "NVARCHAR(64)", nullable: true),
                    OrgDetail = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    Active = table.Column<bool>(type: "BIT", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orgainzations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orgainzations_OrgName",
                table: "Orgainzations",
                column: "OrgName",
                unique: true,
                filter: "[OrgName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orgainzations");
        }
    }
}
