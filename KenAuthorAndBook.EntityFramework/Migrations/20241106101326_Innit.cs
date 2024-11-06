using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KenAuthorAndBook.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Innit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublicationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Bio", "Name" },
                values: new object[,]
                {
                    { new Guid("5ebf1726-aea3-4004-a5e2-b2d9a763ba43"), "American writer, humorist, and lecturer.", "Mark Twain" },
                    { new Guid("ac3146eb-5112-483a-85bc-4cda6f42c966"), "English novelist known for her six major novels.", "Jane Austen" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { new Guid("39d656a7-a354-44b5-aa99-68c61204efca"), new Guid("ac3146eb-5112-483a-85bc-4cda6f42c966"), new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lambatin mo ang aking fart part one" },
                    { new Guid("c49236cd-3f94-4f14-9c46-96a0299150f8"), new Guid("5ebf1726-aea3-4004-a5e2-b2d9a763ba43"), new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keanu ang paglalakbay" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
