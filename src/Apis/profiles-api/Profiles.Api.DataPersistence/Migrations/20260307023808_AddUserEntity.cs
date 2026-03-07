using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profiles.Api.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProfessionalProfile");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PersonalProfile");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FitnessProfile");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FinancialProfile");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialProfile_User_UserId",
                table: "FinancialProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FitnessProfile_User_UserId",
                table: "FitnessProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalProfile_User_UserId",
                table: "PersonalProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalProfile_User_UserId",
                table: "ProfessionalProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialProfile_User_UserId",
                table: "FinancialProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_FitnessProfile_User_UserId",
                table: "FitnessProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalProfile_User_UserId",
                table: "PersonalProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalProfile_User_UserId",
                table: "ProfessionalProfile");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProfessionalProfile",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PersonalProfile",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FitnessProfile",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FinancialProfile",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
