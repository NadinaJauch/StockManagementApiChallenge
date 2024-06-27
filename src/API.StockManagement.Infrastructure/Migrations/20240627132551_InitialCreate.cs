using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.StockManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    LoadDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Category", "Description", "LoadDate", "Price", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "category1", "classic", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5040), 80m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5053) },
                    { 2, "category1", "gold", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5057), 120m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5058) },
                    { 3, "category1", "black", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5060), 150m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5061) },
                    { 4, "category2", "classic", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5063), 160m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5064) },
                    { 5, "category2", "gold", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5066), 230m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5067) },
                    { 6, "category2", "black", new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5068), 360m, new DateTime(2024, 6, 27, 10, 25, 49, 870, DateTimeKind.Local).AddTicks(5069) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleDescription" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "StockManager" },
                    { 3, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[] { 1, "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
