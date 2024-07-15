using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class PublicationDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers");

            migrationBuilder.AlterColumn<decimal>(
                name: "id",
                table: "publications",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "publication_id",
                table: "publication_complaints",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "pulication_id",
                table: "offers",
                type: "decimal(20,0)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "publications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<Guid>(
                name: "publication_id",
                table: "publication_complaints",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "pulication_id",
                table: "offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id");
        }
    }
}
