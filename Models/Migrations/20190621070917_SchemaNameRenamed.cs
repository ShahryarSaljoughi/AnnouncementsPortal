using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class SchemaNameRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Department",
                newSchema: "AP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Department",
                schema: "AP",
                newName: "Department");
        }
    }
}
