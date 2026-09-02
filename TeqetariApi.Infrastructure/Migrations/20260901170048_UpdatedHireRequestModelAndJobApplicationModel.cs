using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeqetariApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedHireRequestModelAndJobApplicationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_PlacementContracts_PlacementContractId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_JobPosts_JobPostId",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_JobPostId",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_PlacementContractId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobPostId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "PlacementContractId",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "HireRequestId",
                table: "PlacementContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobApplicationId",
                table: "PlacementContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobPostId",
                table: "JobApplications",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverMessage",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeclineReason",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "JobApplications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeclineReason",
                table: "HireRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "HireRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferedSalary",
                table: "HireRequests",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "HireRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateFrom",
                table: "HireRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateTo",
                table: "HireRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_HireRequestId",
                table: "PlacementContracts",
                column: "HireRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_JobApplicationId",
                table: "PlacementContracts",
                column: "JobApplicationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_HireRequests_HireRequestId",
                table: "PlacementContracts",
                column: "HireRequestId",
                principalTable: "HireRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_JobApplications_JobApplicationId",
                table: "PlacementContracts",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_HireRequests_HireRequestId",
                table: "PlacementContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_PlacementContracts_JobApplications_JobApplicationId",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_HireRequestId",
                table: "PlacementContracts");

            migrationBuilder.DropIndex(
                name: "IX_PlacementContracts_JobApplicationId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "HireRequestId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "JobApplicationId",
                table: "PlacementContracts");

            migrationBuilder.DropColumn(
                name: "CoverMessage",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "DeclineReason",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "DeclineReason",
                table: "HireRequests");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "HireRequests");

            migrationBuilder.DropColumn(
                name: "OfferedSalary",
                table: "HireRequests");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "HireRequests");

            migrationBuilder.DropColumn(
                name: "StartDateFrom",
                table: "HireRequests");

            migrationBuilder.DropColumn(
                name: "StartDateTo",
                table: "HireRequests");

            migrationBuilder.AddColumn<int>(
                name: "JobPostId",
                table: "PlacementContracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "JobPostId",
                table: "JobApplications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PlacementContractId",
                table: "JobApplications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContracts_JobPostId",
                table: "PlacementContracts",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_PlacementContractId",
                table: "JobApplications",
                column: "PlacementContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_PlacementContracts_PlacementContractId",
                table: "JobApplications",
                column: "PlacementContractId",
                principalTable: "PlacementContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacementContracts_JobPosts_JobPostId",
                table: "PlacementContracts",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
