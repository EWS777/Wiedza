using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class PersonDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_persons_email",
                table: "persons");

            migrationBuilder.DropIndex(
                name: "ix_persons_username",
                table: "persons");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "persons",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "persons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "ix_persons_email",
                table: "persons",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_persons_username",
                table: "persons",
                column: "username",
                unique: true,
                filter: "[username] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_persons_email",
                table: "persons");

            migrationBuilder.DropIndex(
                name: "ix_persons_username",
                table: "persons");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "persons",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "persons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_persons_email",
                table: "persons",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_persons_username",
                table: "persons",
                column: "username",
                unique: true);
        }
    }
}
