using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicket.API.Domain.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddUserResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "resetPasswordToken",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "resetPasswordTokenExpirationDate",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

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
                columns: new[] { "createDateTime", "password", "resetPasswordToken", "resetPasswordTokenExpirationDate" },
                values: new object[] { new DateTime(2024, 11, 26, 19, 44, 25, 231, DateTimeKind.Utc).AddTicks(2240), "ukzdITvxeDm4kXHGlPsY+A==/8Z2WnNRcrjPgjiit7jgaxSBSYxWE5cFG4AOCEYJ5jCU=", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resetPasswordToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resetPasswordTokenExpirationDate",
                table: "users");

            migrationBuilder.UpdateData(
                table: "account",
                keyColumn: "id",
                keyValue: new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                column: "createDateTime",
                value: new DateTime(2024, 11, 17, 21, 18, 58, 779, DateTimeKind.Utc).AddTicks(1503));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                columns: new[] { "createDateTime", "password" },
                values: new object[] { new DateTime(2024, 11, 17, 21, 18, 58, 808, DateTimeKind.Utc).AddTicks(4193), "RTNRyrtfR3CwrQY+QRMDjQ==/+lkjtCaEMLcg+DvMSN9TL1zr6mHB3XG97jMV4LwxnDs=" });
        }
    }
}
