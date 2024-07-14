using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class PersonSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_salts");
        }
    }
}
