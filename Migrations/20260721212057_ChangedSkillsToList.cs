using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeqetariApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSkillsToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "RequiredSkills",
                table: "JobPosts",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "integer");

            migrationBuilder.AddColumn<List<string>>(
                name: "SpecialInstruction",
                table: "Employers",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Skills",
                table: "Employees",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "JobCategory",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialInstruction",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredSkills",
                table: "JobPosts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AlterColumn<int>(
                name: "Skills",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);
        }
    }
}
