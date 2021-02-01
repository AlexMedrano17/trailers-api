using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace trailers_api.Data.Migrations
{
    public partial class Trailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin_user",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INT", nullable: false),
                    username = table.Column<string>(type: "VARCHAR (50)", nullable: false),
                    password = table.Column<string>(type: "VARCHAR", nullable: false),
                    shedule_date = table.Column<byte[]>(type: "DATETIME", nullable: true, defaultValueSql: "datetime('now', 'localtime')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_user", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INT", nullable: false),
                    name = table.Column<string>(type: "VARCHAR (50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trailers",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false),
                    title = table.Column<string>(type: "VARCHAR (100)", nullable: false),
                    genre = table.Column<long>(type: "INT", nullable: false),
                    year = table.Column<string>(type: "CHAR (4)", nullable: false),
                    url = table.Column<string>(type: "VARCHAR", nullable: false),
                    img_url = table.Column<string>(type: "VARCHAR", nullable: false),
                    shedule_date = table.Column<byte[]>(type: "DATETIME", nullable: true, defaultValueSql: "datetime('now', 'localtime')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trailers_Genre_genre",
                        column: x => x.genre,
                        principalTable: "Genre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_user_username",
                table: "Admin_user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_name",
                table: "Genre",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_genre",
                table: "Trailers",
                column: "genre");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_img_url",
                table: "Trailers",
                column: "img_url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_title",
                table: "Trailers",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_url",
                table: "Trailers",
                column: "url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_user");

            migrationBuilder.DropTable(
                name: "Trailers");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
