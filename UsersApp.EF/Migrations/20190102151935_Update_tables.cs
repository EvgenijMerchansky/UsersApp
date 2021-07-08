using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersApp.DAL.EF.Migrations
{
    public partial class Update_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProductId",
                table: "Users",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Products_ProductId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProductId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Users");
        }
    }
}
