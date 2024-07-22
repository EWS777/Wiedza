using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class TypeInBaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_admins_administrator_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_publication_pulication_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_admins_administrator_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_admins_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_publication_publication_id",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "type",
                table: "users");

            migrationBuilder.DropColumn(
                name: "type",
                table: "publications");

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "publication_type",
                table: "publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_publications_publication_id",
                table: "publication_complaints",
                column: "publication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_publications_publication_id",
                table: "publication_complaints");

            migrationBuilder.DropColumn(
                name: "user_type",
                table: "users");

            migrationBuilder.DropColumn(
                name: "publication_type",
                table: "publications");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "publications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_admins_administrator_id",
                table: "message_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publication_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_admins_administrator_id",
                table: "person_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_admins_administrator_id",
                table: "publication_complaints",
                column: "administrator_id",
                principalTable: "admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_publication_publication_id",
                table: "publication_complaints",
                column: "publication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
