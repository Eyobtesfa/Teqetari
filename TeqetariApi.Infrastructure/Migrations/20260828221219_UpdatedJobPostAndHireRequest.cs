using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeqetariApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedJobPostAndHireRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccommodationProvided",
                table: "JobPosts");

            migrationBuilder.AddColumn<int>(
                name: "WorkingMode",
                table: "JobPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HireRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployerId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HireRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HireRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HireRequests_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HireRequests_EmployeeId",
                table: "HireRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HireRequests_EmployerId",
                table: "HireRequests",
                column: "EmployerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HireRequests");

            migrationBuilder.DropColumn(
                name: "WorkingMode",
                table: "JobPosts");

            migrationBuilder.AddColumn<bool>(
                name: "AccommodationProvided",
                table: "JobPosts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
