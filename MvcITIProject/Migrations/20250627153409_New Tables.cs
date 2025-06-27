using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcITIProject.Migrations
{
    /// <inheritdoc />
    public partial class NewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                columns: table => new
                {
                    Number = table.Column<int>(type: "int", nullable: false),
                    Num_blocks = table.Column<int>(type: "int", nullable: false),
                    MG_ID = table.Column<int>(type: "int", nullable: true),
                    Hiring_Date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shelf",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Floor_num = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelf", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Shelf_Floor",
                        column: x => x.Floor_num,
                        principalTable: "Floor",
                        principalColumn: "Number");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Cat_id = table.Column<int>(type: "int", nullable: false),
                    Publisher_id = table.Column<int>(type: "int", nullable: false),
                    Shelf_code = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Category",
                        column: x => x.Cat_id,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Publisher",
                        column: x => x.Publisher_id,
                        principalTable: "Publisher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Shelf",
                        column: x => x.Shelf_code,
                        principalTable: "Shelf",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "Book_Author",
                columns: table => new
                {
                    Book_id = table.Column<int>(type: "int", nullable: false),
                    Author_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_Author", x => new { x.Book_id, x.Author_id });
                    table.ForeignKey(
                        name: "FK_Book_Author_Author",
                        column: x => x.Author_id,
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Author_Book",
                        column: x => x.Book_id,
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Cat_id",
                table: "Book",
                column: "Cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Publisher_id",
                table: "Book",
                column: "Publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Shelf_code",
                table: "Book",
                column: "Shelf_code");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Author_Author_id",
                table: "Book_Author",
                column: "Author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shelf_Floor_num",
                table: "Shelf",
                column: "Floor_num");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book_Author");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "Shelf");

            migrationBuilder.DropTable(
                name: "Floor");
        }
    }
}
