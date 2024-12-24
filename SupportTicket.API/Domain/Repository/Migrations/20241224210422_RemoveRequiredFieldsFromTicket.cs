using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicket.API.Domain.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredFieldsFromTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "updatedBy",
                table: "tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "assignedTo",
                table: "tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "account",
                keyColumn: "id",
                keyValue: new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                column: "createDateTime",
                value: new DateTime(2024, 12, 24, 21, 4, 22, 45, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                columns: new[] { "createDateTime", "password" },
                values: new object[] { new DateTime(2024, 12, 24, 21, 4, 22, 69, DateTimeKind.Utc).AddTicks(2880), "VqJyGxkAbt9dxbKt/N+/rA==__g89VUXhcjHwnH/cDPZ8i8IKiCSyUMi1izSFQsinstgA=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "updatedBy",
                table: "tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "assignedTo",
                table: "tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
        }
    }
}
