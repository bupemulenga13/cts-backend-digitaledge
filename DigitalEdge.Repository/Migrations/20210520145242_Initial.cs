using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalEdge.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "MessageTemplates",
                columns: table => new
                {
                    MessageTemplateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Timed = table.Column<bool>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.MessageTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
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
                name: "UserRoles",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 15, nullable: true),
                    IsSuperAdmin = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(nullable: true),
                    FacilityContactNumber = table.Column<string>(nullable: true),
                    FacilityTypeId = table.Column<long>(nullable: false),
                    DistrictId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateEdited = table.Column<DateTime>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                    table.ForeignKey(
                        name: "FK_Facilities_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "FacilityTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ServicePoints",
                columns: table => new
                {
                    ServicePointId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicePointName = table.Column<string>(nullable: true),
                    FacilityId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePoints", x => x.ServicePointId);
                    table.ForeignKey(
                        name: "FK_ServicePoints_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BulkMessages",
                columns: table => new
                {
                    BulkMessagesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientNumber = table.Column<string>(nullable: true),
                    FacilityId = table.Column<long>(nullable: true),
                    ServicePointId = table.Column<long>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    MessageStatus = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkMessages", x => x.BulkMessagesId);
                    table.ForeignKey(
                        name: "FK_BulkMessages_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulkMessages_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "ServicePointId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    ArtNo = table.Column<string>(nullable: false),
                    ClientPhoneNo = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateEdit = table.Column<DateTime>(nullable: true),
                    EditBy = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    PhysicalAddress = table.Column<string>(nullable: true),
                    GeneralComment = table.Column<string>(nullable: true),
                    EnrolledByName = table.Column<string>(nullable: true),
                    AlternativePhoneNumber1 = table.Column<string>(nullable: true),
                    PhoneVerifiedByAnalyst = table.Column<bool>(nullable: true),
                    PhoneVerifiedByFacilityStaff = table.Column<bool>(nullable: true),
                    EnrollmentDate = table.Column<DateTime>(nullable: false),
                    EnrolledByPhone = table.Column<string>(nullable: true),
                    FacilityId = table.Column<long>(nullable: false),
                    ClientTypeId = table.Column<long>(nullable: false),
                    ServicePointId = table.Column<long>(nullable: true),
                    LanguageId = table.Column<long>(nullable: true),
                    ClientStatusId = table.Column<long>(nullable: false),
                    StatusCommentId = table.Column<long>(nullable: false),
                    SexId = table.Column<long>(nullable: false),
                    ClientRelationship = table.Column<int>(nullable: true),
                    EnrollmentType = table.Column<int>(nullable: true),
                    AccessToPhone = table.Column<bool>(nullable: true),
                    HamornizedMobilePhone = table.Column<int>(nullable: true),
                    HarmonizedPhysicalAddress = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_ClientStatuses_ClientStatusId",
                        column: x => x.ClientStatusId,
                        principalTable: "ClientStatuses",
                        principalColumn: "ClientStatusId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Clients_ClientTypes_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientTypes",
                        principalColumn: "ClientTypeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Clients_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Clients_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "ServicePointId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Sexes_SexId",
                        column: x => x.SexId,
                        principalTable: "Sexes",
                        principalColumn: "SexId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Clients_StatusComments_StatusCommentId",
                        column: x => x.StatusCommentId,
                        principalTable: "StatusComments",
                        principalColumn: "StatusCommentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Userfacility",
                columns: table => new
                {
                    UserFacilityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: true),
                    FacilityId = table.Column<long>(nullable: true),
                    ServicePointId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userfacility", x => x.UserFacilityId);
                    table.ForeignKey(
                        name: "FK_Userfacility_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Userfacility_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "ServicePointId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Userfacility_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(nullable: false),
                    FacilityId = table.Column<long>(nullable: false),
                    ServiceTypeId = table.Column<long>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: false),
                    AppointmentStatus = table.Column<int>(nullable: false),
                    InteractionDate = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    DaysLate = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateEdited = table.Column<DateTime>(nullable: true),
                    EditedBy = table.Column<long>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdentitys",
                columns: table => new
                {
                    IdentityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FingerPrintImage = table.Column<byte[]>(nullable: true),
                    ClientId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdentitys", x => x.IdentityId);
                    table.ForeignKey(
                        name: "FK_ClientIdentitys_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessagesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(nullable: false),
                    ClientNumber = table.Column<string>(nullable: true),
                    FacilityId = table.Column<long>(nullable: true),
                    ServicePointId = table.Column<long>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    MessageStatus = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessagesId);
                    table.ForeignKey(
                        name: "FK_Messages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messages_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_ServicePoints_ServicePointId",
                        column: x => x.ServicePointId,
                        principalTable: "ServicePoints",
                        principalColumn: "ServicePointId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(nullable: false),
                    FacilityId = table.Column<long>(nullable: false),
                    ServiceTypeId = table.Column<long>(nullable: false),
                    AppointmentId = table.Column<long>(nullable: true),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    VisitType = table.Column<string>(nullable: true),
                    PriorAppointmentDate = table.Column<DateTime>(nullable: false),
                    NextAppointmentDate = table.Column<DateTime>(nullable: false),
                    AppointmentStatus = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEdited = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    ClientPhoneNo = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Age = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visits_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visits_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Visits_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_FacilityId",
                table: "Appointments",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceTypeId",
                table: "Appointments",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BulkMessages_FacilityId",
                table: "BulkMessages",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BulkMessages_ServicePointId",
                table: "BulkMessages",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdentitys_ClientId",
                table: "ClientIdentitys",
                column: "ClientId");

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
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_DistrictId",
                table: "Facilities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ClientId",
                table: "Messages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FacilityId",
                table: "Messages",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ServicePointId",
                table: "Messages",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePoints_FacilityId",
                table: "ServicePoints",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusComments_ClientStatusId",
                table: "StatusComments",
                column: "ClientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Userfacility_FacilityId",
                table: "Userfacility",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Userfacility_ServicePointId",
                table: "Userfacility",
                column: "ServicePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Userfacility_UserId",
                table: "Userfacility",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_AppointmentId",
                table: "Visits",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClientId",
                table: "Visits",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_FacilityId",
                table: "Visits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ServiceTypeId",
                table: "Visits",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BulkMessages");

            migrationBuilder.DropTable(
                name: "ClientIdentitys");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MessageTemplates");

            migrationBuilder.DropTable(
                name: "Userfacility");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "ClientTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "ServicePoints");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "StatusComments");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "ClientStatuses");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
