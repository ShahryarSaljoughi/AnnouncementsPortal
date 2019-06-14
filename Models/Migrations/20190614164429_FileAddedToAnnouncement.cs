using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class FileAddedToAnnouncement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FileId",
                schema: "AP",
                table: "Announcement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_FileId",
                schema: "AP",
                table: "Announcement",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_UploadedFile_FileId",
                schema: "AP",
                table: "Announcement",
                column: "FileId",
                principalSchema: "AP",
                principalTable: "UploadedFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_UploadedFile_FileId",
                schema: "AP",
                table: "Announcement");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_FileId",
                schema: "AP",
                table: "Announcement");

            migrationBuilder.DropColumn(
                name: "FileId",
                schema: "AP",
                table: "Announcement");
        }
    }
}
