using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class PasswordSaltHashAddedToTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AP.Announcement");
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_TeacherInfo_OwnerId",
                schema: "AP",
                table: "Announcement");

            migrationBuilder.DropTable(
                name: "TeacherInfo",
                schema: "AP");

            migrationBuilder.CreateTable(
                name: "Teacher",
                schema: "AP",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ZnuUrl = table.Column<string>(nullable: true),
                    AcademicRank = table.Column<string>(nullable: true),
                    AccountActivated = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_Teacher_OwnerId",
                schema: "AP",
                table: "Announcement",
                column: "OwnerId",
                principalSchema: "AP",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_Teacher_OwnerId",
                schema: "AP",
                table: "Announcement");

            migrationBuilder.DropTable(
                name: "Teacher",
                schema: "AP");

            migrationBuilder.CreateTable(
                name: "TeacherInfo",
                schema: "AP",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AcademicRank = table.Column<string>(nullable: true),
                    AccountActivated = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    ZnuUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherInfo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_TeacherInfo_OwnerId",
                schema: "AP",
                table: "Announcement",
                column: "OwnerId",
                principalSchema: "AP",
                principalTable: "TeacherInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
