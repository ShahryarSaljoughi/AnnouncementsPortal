using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class DepartmntAddeToTeache : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "AP",
                table: "Teacher",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_DepartmentId",
                schema: "AP",
                table: "Teacher",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Department_DepartmentId",
                schema: "AP",
                table: "Teacher",
                column: "DepartmentId",
                principalSchema: "AP",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Department_DepartmentId",
                schema: "AP",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_DepartmentId",
                schema: "AP",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "AP",
                table: "Teacher");
        }
    }
}
