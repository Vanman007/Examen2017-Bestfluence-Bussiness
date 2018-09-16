using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExamProject2017.Migrations
{
    public partial class updatedInfluencerP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerCategory_AspNetUsers_ApplicationUserId",
                table: "InfluencerCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerPlatform_AspNetUsers_ApplicationUserId",
                table: "InfluencerPlatform");

            migrationBuilder.DropIndex(
                name: "IX_InfluencerPlatform_ApplicationUserId",
                table: "InfluencerPlatform");

            migrationBuilder.DropIndex(
                name: "IX_InfluencerCategory_ApplicationUserId",
                table: "InfluencerCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "InfluencerPlatform");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "InfluencerCategory");

            migrationBuilder.AddColumn<string>(
                name: "InfluencerProfileId",
                table: "InfluencerPlatform",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfluencerProfileId",
                table: "InfluencerCategory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerPlatform_InfluencerProfileId",
                table: "InfluencerPlatform",
                column: "InfluencerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerCategory_InfluencerProfileId",
                table: "InfluencerCategory",
                column: "InfluencerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerCategory_InfluencerProfile_InfluencerProfileId",
                table: "InfluencerCategory",
                column: "InfluencerProfileId",
                principalTable: "InfluencerProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerPlatform_InfluencerProfile_InfluencerProfileId",
                table: "InfluencerPlatform",
                column: "InfluencerProfileId",
                principalTable: "InfluencerProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerCategory_InfluencerProfile_InfluencerProfileId",
                table: "InfluencerCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_InfluencerPlatform_InfluencerProfile_InfluencerProfileId",
                table: "InfluencerPlatform");

            migrationBuilder.DropIndex(
                name: "IX_InfluencerPlatform_InfluencerProfileId",
                table: "InfluencerPlatform");

            migrationBuilder.DropIndex(
                name: "IX_InfluencerCategory_InfluencerProfileId",
                table: "InfluencerCategory");

            migrationBuilder.DropColumn(
                name: "InfluencerProfileId",
                table: "InfluencerPlatform");

            migrationBuilder.DropColumn(
                name: "InfluencerProfileId",
                table: "InfluencerCategory");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "InfluencerPlatform",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "InfluencerCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerPlatform_ApplicationUserId",
                table: "InfluencerPlatform",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InfluencerCategory_ApplicationUserId",
                table: "InfluencerCategory",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerCategory_AspNetUsers_ApplicationUserId",
                table: "InfluencerCategory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InfluencerPlatform_AspNetUsers_ApplicationUserId",
                table: "InfluencerPlatform",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
