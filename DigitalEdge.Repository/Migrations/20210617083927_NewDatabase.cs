using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalEdge.Repository.Migrations
{
    public partial class NewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HarmonizedPhysicalAddress",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HamornizedMobilePhone",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentType",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientRelationship",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "GISLocation",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Village",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GISLocation",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "HouseNo",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Village",
                table: "Clients");

            migrationBuilder.AlterColumn<int>(
                name: "HarmonizedPhysicalAddress",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HamornizedMobilePhone",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentType",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientRelationship",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
