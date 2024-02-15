using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GulfHrBackend.DLL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationScheduledOns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ScheduleOn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationScheduledOns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSchedules",
                columns: table => new
                {
                    NotificationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TillEndDate = table.Column<bool>(type: "bit", nullable: false),
                    IfHoliday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSchedules", x => x.NotificationScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    period = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportSchedules",
                columns: table => new
                {
                    ReportScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExportFileType = table.Column<int>(type: "int", nullable: false),
                    ReportFrequency = table.Column<int>(type: "int", nullable: false),
                    EndsOn = table.Column<int>(type: "int", nullable: false),
                    EndsOnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsOnAfter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSchedules", x => x.ReportScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportScheduleRecipients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportScheduleRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportScheduleRecipients_ReportSchedules_ReportScheduleId",
                        column: x => x.ReportScheduleId,
                        principalTable: "ReportSchedules",
                        principalColumn: "ReportScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReportScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_NotificationSchedules_NotificationScheduleId",
                        column: x => x.NotificationScheduleId,
                        principalTable: "NotificationSchedules",
                        principalColumn: "NotificationScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_ReportSchedules_ReportScheduleId",
                        column: x => x.ReportScheduleId,
                        principalTable: "ReportSchedules",
                        principalColumn: "ReportScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportScheduleRecipients_ReportScheduleId",
                table: "ReportScheduleRecipients",
                column: "ReportScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_NotificationScheduleId",
                table: "Schedules",
                column: "NotificationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ReportScheduleId",
                table: "Schedules",
                column: "ReportScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "NotificationScheduledOns");

            migrationBuilder.DropTable(
                name: "NotificationTimes");

            migrationBuilder.DropTable(
                name: "ReportScheduleRecipients");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NotificationSchedules");

            migrationBuilder.DropTable(
                name: "ReportSchedules");
        }
    }
}
