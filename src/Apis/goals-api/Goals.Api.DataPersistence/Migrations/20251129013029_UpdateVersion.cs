using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goals.Api.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_GoalTypes_TypeId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalStep_Goals_GoalId",
                table: "GoalStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalTypes",
                table: "GoalTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goals",
                table: "Goals");

            migrationBuilder.RenameTable(
                name: "GoalTypes",
                newName: "GoalType");

            migrationBuilder.RenameTable(
                name: "Goals",
                newName: "Goal");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_TypeId",
                table: "Goal",
                newName: "IX_Goal_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goal",
                table: "Goal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_GoalType_TypeId",
                table: "Goal",
                column: "TypeId",
                principalTable: "GoalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalStep_Goal_GoalId",
                table: "GoalStep",
                column: "GoalId",
                principalTable: "Goal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_GoalType_TypeId",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalStep_Goal_GoalId",
                table: "GoalStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalType",
                table: "GoalType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Goal",
                table: "Goal");

            migrationBuilder.RenameTable(
                name: "GoalType",
                newName: "GoalTypes");

            migrationBuilder.RenameTable(
                name: "Goal",
                newName: "Goals");

            migrationBuilder.RenameIndex(
                name: "IX_Goal_TypeId",
                table: "Goals",
                newName: "IX_Goals_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalTypes",
                table: "GoalTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Goals",
                table: "Goals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_GoalTypes_TypeId",
                table: "Goals",
                column: "TypeId",
                principalTable: "GoalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalStep_Goals_GoalId",
                table: "GoalStep",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
