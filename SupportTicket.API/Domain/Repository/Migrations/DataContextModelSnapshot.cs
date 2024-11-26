﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SupportTicket.API.Domain.Repository.Models;

#nullable disable

namespace SupportTicket.API.Domain.Repository.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contactEmail");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("contactName");

                    b.Property<string>("ContactTelephone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("contactTelephone");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createDateTime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateDateTime");

                    b.HasKey("Id");

                    b.ToTable("account");

                    b.HasData(
                        new
                        {
                            Id = new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                            ContactEmail = "admin@creativ360.com",
                            ContactName = "Creativ360 Contact",
                            ContactTelephone = "+27741897705",
                            CreatedDateTime = new DateTime(2024, 11, 26, 19, 44, 25, 209, DateTimeKind.Utc).AddTicks(7170),
                            IsActive = true,
                            Name = "DefaultAccount"
                        });
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("content");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("createdBy");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdDateTime");

                    b.Property<bool>("IsInternal")
                        .HasColumnType("boolean")
                        .HasColumnName("isInternal");

                    b.Property<Guid?>("ParentCommentId")
                        .HasColumnType("uuid")
                        .HasColumnName("parentComment");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uuid")
                        .HasColumnName("ticket");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("updatedBy");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("TicketId")
                        .HasDatabaseName("IX_Comment_TicketId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uuid")
                        .HasColumnName("comment");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("contentType");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("fileName");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("filePath");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint")
                        .HasColumnName("fileSize");

                    b.Property<Guid?>("TicketId")
                        .HasColumnType("uuid")
                        .HasColumnName("ticket");

                    b.Property<DateTime>("UploadDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("uploadedDateTime");

                    b.Property<Guid?>("UploadedById")
                        .HasColumnType("uuid")
                        .HasColumnName("uploadedBy");

                    b.HasKey("Id");

                    b.HasIndex("CommentId")
                        .HasDatabaseName("IX_File_CommentId");

                    b.HasIndex("TicketId")
                        .HasDatabaseName("IX_File_TicketId");

                    b.HasIndex("UploadedById");

                    b.ToTable("files");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssignedToId")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("assignedTo");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("createdBy");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createDateTime");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("message");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("priority");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("subject");

                    b.Property<string[]>("Tags")
                        .HasColumnType("text[]")
                        .HasColumnName("tags");

                    b.Property<Guid?>("UpdatedById")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("updatedBy");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId")
                        .HasDatabaseName("IX_Ticket_AssignedTo");

                    b.HasIndex("CreatedById");

                    b.HasIndex("Status")
                        .HasDatabaseName("IX_Ticket_Status");

                    b.HasIndex("Tags")
                        .HasDatabaseName("IX_Ticket_Tags");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("Tags"), "GIN");

                    b.HasIndex("UpdatedById");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.TicketHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ChangedById")
                        .HasColumnType("uuid")
                        .HasColumnName("ChangedBy");

                    b.Property<DateTime>("ChangedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changedDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("description");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("field");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("newValue");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("oldValue");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uuid")
                        .HasColumnName("ticket");

                    b.HasKey("Id");

                    b.HasIndex("ChangedById");

                    b.HasIndex("ChangedDateTime")
                        .HasDatabaseName("IX_TicketHistory_ChangedDateTime");

                    b.HasIndex("TicketId")
                        .HasDatabaseName("IX_TicketHistory_TicketId");

                    b.ToTable("ticketHistory");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("accessFailedCount");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account");

                    b.Property<Guid?>("CreatedById")
                        .IsRequired()
                        .HasColumnType("uuid")
                        .HasColumnName("createBy");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("firstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isActive");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("isEmailConfirmed");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("isLocked");

                    b.Property<bool>("IsLockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("isLockoutEnabled");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<string>("ResetPasswordToken")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("resetPasswordToken");

                    b.Property<DateTime?>("ResetPasswordTokenExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("resetPasswordTokenExpirationDate");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("updateBy");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_Email");

                    b.HasIndex("UpdatedById");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                            AccessFailedCount = 0,
                            AccountId = new Guid("10dce69c-ac67-40d9-927b-86fe23206a90"),
                            CreatedById = new Guid("863e485c-196d-41e0-ad03-90e94c3890af"),
                            CreatedDateTime = new DateTime(2024, 11, 26, 19, 44, 25, 231, DateTimeKind.Utc).AddTicks(2240),
                            Email = "admin@creativ360.com",
                            FirstName = "admin",
                            IsActive = true,
                            IsEmailConfirmed = true,
                            IsLocked = false,
                            IsLockoutEnabled = false,
                            LastName = "Creativ360",
                            Password = "ukzdITvxeDm4kXHGlPsY+A==/8Z2WnNRcrjPgjiit7jgaxSBSYxWE5cFG4AOCEYJ5jCU="
                        });
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Comment", b =>
                {
                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "CreatedBy")
                        .WithMany("CreatedComments")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Ticket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "UpdatedBy")
                        .WithMany("UpdatedComments")
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatedBy");

                    b.Navigation("ParentComment");

                    b.Navigation("Ticket");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.File", b =>
                {
                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Comment", "Comment")
                        .WithMany("Attachments")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Ticket", "Ticket")
                        .WithMany("Attachments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "UploadedBy")
                        .WithMany("Files")
                        .HasForeignKey("UploadedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Comment");

                    b.Navigation("Ticket");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Ticket", b =>
                {
                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "AssignedTo")
                        .WithMany("AssignedTickets")
                        .HasForeignKey("AssignedToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "CreatedBy")
                        .WithMany("CreatedTickets")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "UpdatedBy")
                        .WithMany("UpdatedTickets")
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.TicketHistory", b =>
                {
                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "ChangedBy")
                        .WithMany()
                        .HasForeignKey("ChangedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Ticket", "Ticket")
                        .WithMany("History")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChangedBy");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.User", b =>
                {
                    b.HasOne("SupportTicket.API.Domain.Repository.Models.Account", "Account")
                        .WithMany("Users")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "CreatedByUser")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SupportTicket.API.Domain.Repository.Models.User", "UpdatedByUser")
                        .WithMany("UpdatedUsers")
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Account");

                    b.Navigation("CreatedByUser");

                    b.Navigation("UpdatedByUser");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Account", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Comment", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.Ticket", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("History");
                });

            modelBuilder.Entity("SupportTicket.API.Domain.Repository.Models.User", b =>
                {
                    b.Navigation("AssignedTickets");

                    b.Navigation("CreatedComments");

                    b.Navigation("CreatedTickets");

                    b.Navigation("CreatedUsers");

                    b.Navigation("Files");

                    b.Navigation("UpdatedComments");

                    b.Navigation("UpdatedTickets");

                    b.Navigation("UpdatedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
