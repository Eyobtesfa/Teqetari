using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeqetariApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTPHToEmployers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorizedOfficerName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersonRole",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Employers",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPets",
                table: "Employers",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Industry",
                table: "Employers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIdNumber",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfFamilyMembers",
                table: "Employers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficialLetterRefNumber",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sector",
                table: "Employers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Employers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxRegistrationNumber",
                table: "Employers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeLicenseNumber",
                table: "Employers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedOfficerName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ContactPersonName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "ContactPersonRole",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "HasPets",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "NationalIdNumber",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "NumberOfFamilyMembers",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "OfficialLetterRefNumber",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Sector",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "TaxRegistrationNumber",
                table: "Employers");

            migrationBuilder.DropColumn(
                name: "TradeLicenseNumber",
                table: "Employers");
        }
    }
}
