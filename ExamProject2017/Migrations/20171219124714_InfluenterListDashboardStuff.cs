using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExamProject2017.Migrations
{
    public partial class InfluenterListDashboardStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfluencerLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfluencerLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfluencerLists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListObjects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InfluencerListId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListObjects_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListObjects_InfluencerLists_InfluencerListId",
                        column: x => x.InfluencerListId,
                        principalTable: "InfluencerLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerLists_ApplicationUserId",
                table: "InfluencerLists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListObjects_ApplicationUserId",
                table: "ListObjects",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListObjects_InfluencerListId",
                table: "ListObjects",
                column: "InfluencerListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListObjects");

            migrationBuilder.DropTable(
                name: "InfluencerLists");
        }
    }
}
