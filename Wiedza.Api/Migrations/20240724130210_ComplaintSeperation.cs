using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class ComplaintSeperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_files_attachment_file_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_persons_author_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_persons_author_id",
                table: "publication_complaints");

            migrationBuilder.DropPrimaryKey(
                name: "pk_publication_complaints",
                table: "publication_complaints");

            migrationBuilder.DropIndex(
                name: "ix_publication_complaints_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropIndex(
                name: "ix_publication_complaints_attachment_file_id",
                table: "publication_complaints");

            migrationBuilder.DropIndex(
                name: "ix_publication_complaints_author_id",
                table: "publication_complaints");

            migrationBuilder.DropPrimaryKey(
                name: "pk_person_complaints",
                table: "person_complaints");

            migrationBuilder.DropIndex(
                name: "ix_person_complaints_administrator_id",
                table: "person_complaints");

            migrationBuilder.DropIndex(
                name: "ix_person_complaints_attachment_file_id",
                table: "person_complaints");

            migrationBuilder.DropIndex(
                name: "ix_person_complaints_author_id",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "attachment_file_id",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "description",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "finish_at",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "status",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "title",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "administrator_id",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "attachment_file_id",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "description",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "finished_at",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "status",
                table: "person_complaints");

            migrationBuilder.DropColumn(
                name: "title",
                table: "person_complaints");

            migrationBuilder.AddPrimaryKey(
                name: "PK_publication_complaints",
                table: "publication_complaints",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_person_complaints",
                table: "person_complaints",
                column: "id");

            migrationBuilder.CreateTable(
                name: "complaints",
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
                    administrator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    complaint_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints", x => x.id);
                    table.ForeignKey(
                        name: "fk_complaints_admins_administrator_id",
                        column: x => x.administrator_id,
                        principalTable: "admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_complaints_files_attachment_file_id",
                        column: x => x.attachment_file_id,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_complaints_persons_author_id",
                        column: x => x.author_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_complaints_administrator_id",
                table: "complaints",
                column: "administrator_id");

            migrationBuilder.CreateIndex(
                name: "ix_complaints_attachment_file_id",
                table: "complaints",
                column: "attachment_file_id");

            migrationBuilder.CreateIndex(
                name: "ix_complaints_author_id",
                table: "complaints",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_complaints_id",
                table: "person_complaints",
                column: "id",
                principalTable: "complaints",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_complaints_id",
                table: "publication_complaints",
                column: "id",
                principalTable: "complaints",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_complaints_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_complaints_id",
                table: "publication_complaints");

            migrationBuilder.DropTable(
                name: "complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_publication_complaints",
                table: "publication_complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person_complaints",
                table: "person_complaints");

            migrationBuilder.AddColumn<Guid>(
                name: "administrator_id",
                table: "publication_complaints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "attachment_file_id",
                table: "publication_complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                table: "publication_complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "publication_complaints",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "publication_complaints",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "finish_at",
                table: "publication_complaints",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "publication_complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "publication_complaints",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "administrator_id",
                table: "person_complaints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "attachment_file_id",
                table: "person_complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                table: "person_complaints",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_at",
                table: "person_complaints",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "person_complaints",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "finished_at",
                table: "person_complaints",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "person_complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "person_complaints",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_publication_complaints",
                table: "publication_complaints",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_person_complaints",
                table: "person_complaints",
                column: "id");

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

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_files_attachment_file_id",
                table: "person_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_persons_author_id",
                table: "person_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_persons_author_id",
                table: "publication_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
