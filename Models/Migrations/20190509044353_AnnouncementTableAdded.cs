using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class AnnouncementTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountActivated",
                schema: "AP",
                table: "TeacherInfo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Announcement",
                schema: "AP",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcement_TeacherInfo_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "AP",
                        principalTable: "TeacherInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_OwnerId",
                schema: "AP",
                table: "Announcement",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcement",
                schema: "AP");

            migrationBuilder.DropColumn(
                name: "AccountActivated",
                schema: "AP",
                table: "TeacherInfo");
        }
    }
}
