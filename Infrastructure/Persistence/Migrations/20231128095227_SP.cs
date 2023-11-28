using Microsoft.EntityFrameworkCore.Migrations;
using Persistence.Helpers;

#nullable disable

namespace Persistence.Migrations
{
    public partial class SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddSqlFile();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
