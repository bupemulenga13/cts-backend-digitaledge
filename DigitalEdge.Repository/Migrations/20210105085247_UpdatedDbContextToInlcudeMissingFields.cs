using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalEdge.Repository.Migrations
{
    public partial class UpdatedDbContextToInlcudeMissingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Userfacility");

            migrationBuilder.AddColumn<long>(
                name: "Age",
                table: "Visits",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ClientPhoneNo",
                table: "Visits",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Visits",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacilityTypeId",
                table: "facility",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativePhoneNumber1",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternativePhoneNumber2",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArtNo",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClientStatusId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ClientTypeId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EnrolledBy",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrolledByPhone",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "Clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "GeneralComment",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LanguageId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneVerifiedByAnalyst",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneVerifiedByFacilityStaff",
                table: "Clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ServicePointId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SexId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StatusCommentId",
                table: "Clients",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ClientStatuses",
                columns: table => new
                {
                    ClientStatusId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientStatuses", x => x.ClientStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    ClientTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.ClientTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    FacilityTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.FacilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Sexes",
                columns: table => new
                {
                    SexId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SexName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexes", x => x.SexId);
                });

            migrationBuilder.CreateTable(
                name: "StatusComments",
                columns: table => new
                {
                    StatusCommentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCommentName = table.Column<string>(nullable: true),
                    ClientStatusId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusComments", x => x.StatusCommentId);
                    table.ForeignKey(
                        name: "FK_StatusComments_ClientStatuses_ClientStatusId",
                        column: x => x.ClientStatusId,
                        principalTable: "ClientStatuses",
                        principalColumn: "ClientStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_facility_FacilityTypeId",
                table: "facility",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientStatusId",
                table: "Clients",
                column: "ClientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientTypeId",
                table: "Clients",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FacilityId",
                table: "Clients",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LanguageId",
                table: "Clients",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ServicePointId",
                table: "Clients",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SexId",
                table: "Clients",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StatusCommentId",
                table: "Clients",
                column: "StatusCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusComments_ClientStatusId",
                table: "StatusComments",
                column: "ClientStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientStatuses_ClientStatusId",
                table: "Clients",
                column: "ClientStatusId",
                principalTable: "ClientStatuses",
                principalColumn: "ClientStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypeId",
                table: "Clients",
                column: "ClientTypeId",
                principalTable: "ClientTypes",
                principalColumn: "ClientTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_facility_FacilityId",
                table: "Clients",
                column: "FacilityId",
                principalTable: "facility",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Languages_LanguageId",
                table: "Clients",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ServicePoints_ServicePointId",
                table: "Clients",
                column: "ServicePointId",
                principalTable: "ServicePoints",
                principalColumn: "ServicePointId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Sexes_SexId",
                table: "Clients",
                column: "SexId",
                principalTable: "Sexes",
                principalColumn: "SexId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_StatusComments_StatusCommentId",
                table: "Clients",
                column: "StatusCommentId",
                principalTable: "StatusComments",
                principalColumn: "StatusCommentId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_facility_FacilityTypes_FacilityTypeId",
                table: "facility",
                column: "FacilityTypeId",
                principalTable: "FacilityTypes",
                principalColumn: "FacilityTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientStatuses_ClientStatusId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientTypes_ClientTypeId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_facility_FacilityId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Languages_LanguageId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ServicePoints_ServicePointId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Sexes_SexId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_StatusComments_StatusCommentId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_facility_FacilityTypes_FacilityTypeId",
                table: "facility");

            migrationBuilder.DropTable(
                name: "ClientTypes");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "StatusComments");

            migrationBuilder.DropTable(
                name: "ClientStatuses");

            migrationBuilder.DropIndex(
                name: "IX_facility_FacilityTypeId",
                table: "facility");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientStatusId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ClientTypeId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_FacilityId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_LanguageId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ServicePointId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SexId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_StatusCommentId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ClientPhoneNo",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "FacilityTypeId",
                table: "facility");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AlternativePhoneNumber1",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AlternativePhoneNumber2",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ArtNo",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientStatusId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EnrolledBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EnrolledByPhone",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "GeneralComment",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneVerifiedByAnalyst",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneVerifiedByFacilityStaff",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ServicePointId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SexId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "StatusCommentId",
                table: "Clients");

            migrationBuilder.AddColumn<long>(
                name: "DistrictId",
                table: "Userfacility",
                type: "bigint",
                nullable: true);
        }
    }
}
