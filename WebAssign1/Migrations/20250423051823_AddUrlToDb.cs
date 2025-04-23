using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAssign1.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "https://regalplus.com/cdn/shop/files/monacoyellow.jpg?v=1736527560", "Monaco", 10m, 10 },
                    { 2, "https://5.imimg.com/data5/SELLER/Default/2021/5/RQ/IA/NE/34912835/bounce-choco.jpg", "Bounce", 20m, 15 },
                    { 3, "https://m.media-amazon.com/images/I/61kBRuYl3vL._AC_UF1000,1000_QL80_.jpg", "Good-Day", 25m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
