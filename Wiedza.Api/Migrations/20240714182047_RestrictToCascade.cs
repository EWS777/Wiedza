using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wiedza.Api.Migrations
{
    /// <inheritdoc />
    public partial class RestrictToCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_categories_parent_category_id",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_chats_offers_offer_id",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_messages_message_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_persons_author_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_persons_author_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_persons_person_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_payments_persons_person_id",
                table: "payments");

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
                name: "fk_person_complaints_persons_person_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_categories_category_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_persons_author_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_persons_author_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_persons_person_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_verifications_persons_person_id",
                table: "verifications");

            migrationBuilder.DropForeignKey(
                name: "fk_withdraws_persons_person_id",
                table: "withdraws");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_chats_offers_offer_id",
                table: "chats",
                column: "offer_id",
                principalTable: "offers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_messages_message_id",
                table: "message_complaints",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_persons_author_id",
                table: "message_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_persons_author_id",
                table: "messages",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_offers_persons_person_id",
                table: "offers",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_payments_persons_person_id",
                table: "payments",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_files_attachment_file_id",
                table: "person_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_persons_author_id",
                table: "person_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_persons_person_id",
                table: "person_complaints",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publications_categories_category_id",
                table: "publications",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_publications_persons_author_id",
                table: "publications",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_persons_author_id",
                table: "reviews",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_persons_person_id",
                table: "reviews",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_verifications_persons_person_id",
                table: "verifications",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_withdraws_persons_person_id",
                table: "withdraws",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_categories_parent_category_id",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "fk_chats_offers_offer_id",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_messages_message_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_message_complaints_persons_author_id",
                table: "message_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_persons_author_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_persons_person_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers");

            migrationBuilder.DropForeignKey(
                name: "fk_payments_persons_person_id",
                table: "payments");

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
                name: "fk_person_complaints_persons_person_id",
                table: "person_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_categories_category_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_publications_persons_author_id",
                table: "publications");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_persons_author_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_persons_person_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_verifications_persons_person_id",
                table: "verifications");

            migrationBuilder.DropForeignKey(
                name: "fk_withdraws_persons_person_id",
                table: "withdraws");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_categories_parent_category_id",
                table: "categories",
                column: "parent_category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_chats_offers_offer_id",
                table: "chats",
                column: "offer_id",
                principalTable: "offers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_administrators_administrator_id",
                table: "message_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_messages_message_id",
                table: "message_complaints",
                column: "message_id",
                principalTable: "messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_message_complaints_persons_author_id",
                table: "message_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_chats_chat_id",
                table: "messages",
                column: "chat_id",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_persons_author_id",
                table: "messages",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_offers_persons_person_id",
                table: "offers",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_offers_publications_pulication_id",
                table: "offers",
                column: "pulication_id",
                principalTable: "publications",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_payments_persons_person_id",
                table: "payments",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_administrators_administrator_id",
                table: "person_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_files_attachment_file_id",
                table: "person_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_persons_author_id",
                table: "person_complaints",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_person_complaints_persons_person_id",
                table: "person_complaints",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_administrators_administrator_id",
                table: "publication_complaints",
                column: "administrator_id",
                principalTable: "administrators",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_publication_complaints_files_attachment_file_id",
                table: "publication_complaints",
                column: "attachment_file_id",
                principalTable: "files",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_publications_categories_category_id",
                table: "publications",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_publications_persons_author_id",
                table: "publications",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_persons_author_id",
                table: "reviews",
                column: "author_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_persons_person_id",
                table: "reviews",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_verifications_persons_person_id",
                table: "verifications",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_withdraws_persons_person_id",
                table: "withdraws",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
