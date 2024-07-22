﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parent_category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_category_id",
                        column: x => x.parent_category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uploaded_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    file_bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "website_balances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_balance = table.Column<float>(type: "real", nullable: false),
                    net_income = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_website_balances", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.id);
                    table.ForeignKey(
                        name: "fk_admins_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    balance = table.Column<float>(type: "real", nullable: false),
                    is_verificated = table.Column<bool>(type: "bit", nullable: false),
                    rating = table.Column<float>(type: "real", nullable: true),
                    avatar_bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    account_state = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                    table.ForeignKey(
                        name: "fk_persons_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    house = table.Column<long>(type: "bigint", nullable: false),
                    postal_code = table.Column<long>(type: "bigint", nullable: false),
                    card_number = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    commission = table.Column<float>(type: "real", nullable: false),
                    received_amount = table.Column<float>(type: "real", nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_complaints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    finished_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    attachment_file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    administrator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_complaints", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_complaints_admins_administrator_id",
                        column: x => x.administrator_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_complaints_files_attachment_file_id",
                        column: x => x.attachment_file_id,
                        principalTable: "files",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_complaints_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_complaints_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_salts",
                columns: table => new
                {
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_salts", x => x.person_id);
                    table.ForeignKey(
                        name: "fk_person_salts_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.id);
                    table.ForeignKey(
                        name: "fk_publications_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_publications_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    rating = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_reviews_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "verifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pesel = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    verification_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    image_document_bytes = table.Column<byte[]>(type: "varbinary(max)", maxLength: 10485760, nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_verifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_verifications_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "withdraws",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    amount = table.Column<float>(type: "real", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    withdraw_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    card_number = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_withdraws", x => x.id);
                    table.ForeignKey(
                        name: "fk_withdraws_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "offers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    company_profit = table.Column<float>(type: "real", nullable: false),
                    freelancer_profit = table.Column<float>(type: "real", nullable: false),
                    pulication_id = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_offers", x => x.id);
                    table.ForeignKey(
                        name: "fk_offers_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_offers_publication_pulication_id",
                        column: x => x.pulication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_projects_publications_id",
                        column: x => x.id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_complaints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    finish_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    publication_id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    attachment_file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    administrator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publication_complaints", x => x.id);
                    table.ForeignKey(
                        name: "fk_publication_complaints_admins_administrator_id",
                        column: x => x.administrator_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_publication_complaints_files_attachment_file_id",
                        column: x => x.attachment_file_id,
                        principalTable: "files",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_publication_complaints_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publication_complaints_publication_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                    table.ForeignKey(
                        name: "fk_services_publications_id",
                        column: x => x.id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    offer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chats", x => x.id);
                    table.ForeignKey(
                        name: "fk_chats_offers_offer_id",
                        column: x => x.offer_id,
                        principalTable: "offers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sended_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    readed_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    chat_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "chats",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messages_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "message_complaints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    finish_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    administrator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_complaints", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_complaints_admins_administrator_id",
                        column: x => x.administrator_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_complaints_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_complaints_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "message_files",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attachment_file_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_files", x => new { x.message_id, x.attachment_file_id });
                    table.ForeignKey(
                        name: "fk_message_files_files_attachment_file_id",
                        column: x => x.attachment_file_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_files_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_chats_offer_id",
                table: "chats",
                column: "offer_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_complaints_administrator_id",
                table: "message_complaints",
                column: "administrator_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_complaints_author_id",
                table: "message_complaints",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_complaints_message_id",
                table: "message_complaints",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_files_attachment_file_id",
                table: "message_files",
                column: "attachment_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_author_id",
                table: "messages",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_chat_id",
                table: "messages",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "ix_offers_person_id",
                table: "offers",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_offers_pulication_id",
                table: "offers",
                column: "pulication_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_person_id",
                table: "payments",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_complaints_administrator_id",
                table: "person_complaints",
                column: "administrator_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_complaints_attachment_file_id",
                table: "person_complaints",
                column: "attachment_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_complaints_author_id",
                table: "person_complaints",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_complaints_person_id",
                table: "person_complaints",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_complaints_administrator_id",
                table: "publication_complaints",
                column: "administrator_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_complaints_attachment_file_id",
                table: "publication_complaints",
                column: "attachment_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_complaints_author_id",
                table: "publication_complaints",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_complaints_publication_id",
                table: "publication_complaints",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_author_id",
                table: "publications",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_category_id",
                table: "publications",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_author_id",
                table: "reviews",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_person_id",
                table: "reviews",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_verifications_person_id",
                table: "verifications",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_withdraws_person_id",
                table: "withdraws",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message_complaints");

            migrationBuilder.DropTable(
                name: "message_files");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "person_complaints");

            migrationBuilder.DropTable(
                name: "person_salts");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "publication_complaints");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "verifications");

            migrationBuilder.DropTable(
                name: "website_balances");

            migrationBuilder.DropTable(
                name: "withdraws");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "offers");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}