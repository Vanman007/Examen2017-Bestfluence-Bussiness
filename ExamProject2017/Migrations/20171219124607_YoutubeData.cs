using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExamProject2017.Migrations
{
    public partial class YoutubeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YoutubeData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Comments = table.Column<int>(type: "int", nullable: false),
                    Dislike = table.Column<int>(type: "int", nullable: false),
                    Engagement = table.Column<double>(type: "float", nullable: false),
                    FemaleViews = table.Column<double>(type: "float", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    MaleViews = table.Column<double>(type: "float", nullable: false),
                    Subcribers = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YoutubeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YoutubeData_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YoutubeData_ApplicationUserId",
                table: "YoutubeData",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YoutubeData");
        }
    }
}
