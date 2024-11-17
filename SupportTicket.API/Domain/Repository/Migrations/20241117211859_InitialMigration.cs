using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportTicket.API.Domain.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contactName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    contactTelephone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    contactEmail = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    createDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    firstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    isEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    isLockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    isLocked = table.Column<bool>(type: "boolean", nullable: false),
                    accessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    createDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createBy = table.Column<Guid>(type: "uuid", nullable: false),
                    updateBy = table.Column<Guid>(type: "uuid", nullable: true),
                    account = table.Column<Guid>(type: "uuid", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_account_account",
                        column: x => x.account,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_users_createBy",
                        column: x => x.createBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_users_updateBy",
                        column: x => x.updateBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    priority = table.Column<string>(type: "varchar(20)", nullable: false),
                    tags = table.Column<string[]>(type: "text[]", nullable: true),
                    createDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    assignedTo = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_users_assignedTo",
                        column: x => x.assignedTo,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_users_createdBy",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_users_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    isInternal = table.Column<bool>(type: "boolean", nullable: false),
                    createdDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ticket = table.Column<Guid>(type: "uuid", nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    parentComment = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_comments_parentComment",
                        column: x => x.parentComment,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_tickets_ticket",
                        column: x => x.ticket,
                        principalTable: "tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_createdBy",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comments_users_updatedBy",
                        column: x => x.updatedBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticketHistory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    field = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    oldValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    newValue = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    changedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ticket = table.Column<Guid>(type: "uuid", nullable: false),
                    ChangedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticketHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticketHistory_tickets_ticket",
                        column: x => x.ticket,
                        principalTable: "tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticketHistory_users_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contentType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    filePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    fileSize = table.Column<long>(type: "bigint", nullable: false),
                    uploadedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    uploadedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ticket = table.Column<Guid>(type: "uuid", nullable: true),
                    comment = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_files_comments_comment",
                        column: x => x.comment,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_files_tickets_ticket",
                        column: x => x.ticket,
                        principalTable: "tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_files_users_uploadedBy",
                        column: x => x.uploadedBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "account",
                columns: new[] { "id", "contactEmail", "contactName", "contactTelephone", "createDateTime", "isActive", "name", "updateDateTime" },
                values: new object[] { new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"), "admin@creativ360.com", "Creativ360 Contact", "+27741897705", new DateTime(2024, 11, 17, 21, 18, 58, 779, DateTimeKind.Utc).AddTicks(1503), true, "DefaultAccount", null });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "accessFailedCount", "account", "createBy", "createDateTime", "email", "firstName", "isActive", "isEmailConfirmed", "isLocked", "isLockoutEnabled", "lastName", "password", "updateBy", "updateDateTime" },
                values: new object[] { new Guid("863e485c-196d-41e0-ad03-90e94c3890af"), 0, new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"), new Guid("863e485c-196d-41e0-ad03-90e94c3890af"), new DateTime(2024, 11, 17, 21, 18, 58, 808, DateTimeKind.Utc).AddTicks(4193), "admin@creativ360.com", "admin", true, true, false, false, "Creativ360", "RTNRyrtfR3CwrQY+QRMDjQ==/+lkjtCaEMLcg+DvMSN9TL1zr6mHB3XG97jMV4LwxnDs=", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_TicketId",
                table: "comments",
                column: "ticket");

            migrationBuilder.CreateIndex(
                name: "IX_comments_createdBy",
                table: "comments",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_comments_parentComment",
                table: "comments",
                column: "parentComment");

            migrationBuilder.CreateIndex(
                name: "IX_comments_updatedBy",
                table: "comments",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_File_CommentId",
                table: "files",
                column: "comment");

            migrationBuilder.CreateIndex(
                name: "IX_File_TicketId",
                table: "files",
                column: "ticket");

            migrationBuilder.CreateIndex(
                name: "IX_files_uploadedBy",
                table: "files",
                column: "uploadedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ticketHistory_ChangedBy",
                table: "ticketHistory",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_ChangedDateTime",
                table: "ticketHistory",
                column: "changedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_TicketHistory_TicketId",
                table: "ticketHistory",
                column: "ticket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedTo",
                table: "tickets",
                column: "assignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Status",
                table: "tickets",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_Tags",
                table: "tickets",
                column: "tags")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_createdBy",
                table: "tickets",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_updatedBy",
                table: "tickets",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_account",
                table: "users",
                column: "account");

            migrationBuilder.CreateIndex(
                name: "IX_users_createBy",
                table: "users",
                column: "createBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_updateBy",
                table: "users",
                column: "updateBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "ticketHistory");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
