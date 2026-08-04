using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeqetariApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedJobPostAndJobApplicationEntitties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "OfferedSalary",
                table: "JobPosts",
                newName: "OfferedSalaryMin");

            migrationBuilder.AddColumn<bool>(
                name: "AccommodationProvided",
                table: "JobPosts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinimumExperienceYears",
                table: "JobPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferedSalaryMax",
                table: "JobPosts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "JobPostId",
                table: "JobApplications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "AccommodationProvided",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "MinimumExperienceYears",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "OfferedSalaryMax",
                table: "JobPosts");

            migrationBuilder.RenameColumn(
                name: "OfferedSalaryMin",
                table: "JobPosts",
                newName: "OfferedSalary");

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
                name: "CoverLetter",
                table: "JobApplications",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobPosts_JobPostId",
                table: "JobApplications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
