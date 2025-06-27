using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcITIProject.Migrations
{
    /// <inheritdoc />
    public partial class setNullOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Shelf",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Author",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Book",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelf_Floor",
                table: "Shelf");

            migrationBuilder.AlterColumn<int>(
                name: "Floor_num",
                table: "Shelf",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Shelf_code",
                table: "Book",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldUnicode: false,
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Publisher_id",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Cat_id",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category",
                table: "Book",
                column: "Cat_id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher",
                table: "Book",
                column: "Publisher_id",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Shelf",
                table: "Book",
                column: "Shelf_code",
                principalTable: "Shelf",
                principalColumn: "Code",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Author",
                table: "Book_Author",
                column: "Author_id",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Book",
                table: "Book_Author",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelf_Floor",
                table: "Shelf",
                column: "Floor_num",
                principalTable: "Floor",
                principalColumn: "Number",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Publisher",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Shelf",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Author",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Book",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelf_Floor",
                table: "Shelf");

            migrationBuilder.AlterColumn<int>(
                name: "Floor_num",
                table: "Shelf",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Shelf_code",
                table: "Book",
                type: "varchar(3)",
                unicode: false,
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldUnicode: false,
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Publisher_id",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cat_id",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category",
                table: "Book",
                column: "Cat_id",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Publisher",
                table: "Book",
                column: "Publisher_id",
                principalTable: "Publisher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Shelf",
                table: "Book",
                column: "Shelf_code",
                principalTable: "Shelf",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Author",
                table: "Book_Author",
                column: "Author_id",
                principalTable: "Author",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Book",
                table: "Book_Author",
                column: "Book_id",
                principalTable: "Book",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelf_Floor",
                table: "Shelf",
                column: "Floor_num",
                principalTable: "Floor",
                principalColumn: "Number");
        }
    }
}
