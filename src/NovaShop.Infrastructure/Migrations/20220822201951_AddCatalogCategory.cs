using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovaShop.Infrastructure.Migrations
{
    public partial class AddCatalogCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "CatalogItems");

            migrationBuilder.AddColumn<int>(
                name: "CatalogCategoryId",
                table: "CatalogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CatalogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogCategoryId",
                table: "CatalogItems",
                column: "CatalogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_CatalogCategories",
                table: "CatalogItems",
                column: "CatalogCategoryId",
                principalTable: "CatalogCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_CatalogCategories",
                table: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CatalogCategories");

            migrationBuilder.DropIndex(
                name: "IX_CatalogItems_CatalogCategoryId",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "CatalogCategoryId",
                table: "CatalogItems");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CatalogItems",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
