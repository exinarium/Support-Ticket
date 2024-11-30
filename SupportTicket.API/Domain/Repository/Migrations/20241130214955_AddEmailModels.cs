using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicket.API.Domain.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    to = table.Column<List<string>>(type: "text[]", nullable: false),
                    cc = table.Column<List<string>>(type: "text[]", nullable: false),
                    bcc = table.Column<List<string>>(type: "text[]", nullable: false),
                    replyTo = table.Column<List<string>>(type: "text[]", nullable: false),
                    htmlBody = table.Column<string>(type: "text", nullable: false),
                    emailType = table.Column<int>(type: "integer", nullable: false),
                    fromName = table.Column<string>(type: "text", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: false),
                    from = table.Column<string>(type: "text", nullable: false),
                    attachmentFileNames = table.Column<List<string>>(type: "text[]", nullable: false),
                    isHtml = table.Column<bool>(type: "boolean", nullable: false),
                    isSMTPEmail = table.Column<bool>(type: "boolean", nullable: false),
                    keyValuePairs = table.Column<Dictionary<string, string>>(type: "hstore", nullable: false),
                    createDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails", x => x.id);
                    table.ForeignKey(
                        name: "FK_emails_users_createdBy",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "account",
                keyColumn: "id",
                keyValue: new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                column: "createDateTime",
                value: new DateTime(2024, 11, 30, 21, 49, 55, 84, DateTimeKind.Utc).AddTicks(5970));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                columns: new[] { "createDateTime", "password" },
                values: new object[] { new DateTime(2024, 11, 30, 21, 49, 55, 112, DateTimeKind.Utc).AddTicks(394), "VIrV0/xvjfAJusHbeq43YQ==/DtI89V51mY2eHAYKkicSrlZIZmxMh8lO7Kq5E7u1hqs=" });

            migrationBuilder.CreateIndex(
                name: "IX_emails_createdBy",
                table: "emails",
                column: "createdBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emails");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:hstore", ",,");

            migrationBuilder.UpdateData(
                table: "account",
                keyColumn: "id",
                keyValue: new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                column: "createDateTime",
                value: new DateTime(2024, 11, 26, 19, 44, 25, 209, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                columns: new[] { "createDateTime", "password" },
                values: new object[] { new DateTime(2024, 11, 26, 19, 44, 25, 231, DateTimeKind.Utc).AddTicks(2240), "ukzdITvxeDm4kXHGlPsY+A==/8Z2WnNRcrjPgjiit7jgaxSBSYxWE5cFG4AOCEYJ5jCU=" });
        }
    }
}
