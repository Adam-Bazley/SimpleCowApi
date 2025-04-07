using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleCowApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cows_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cows",
                columns: new[] { "Id", "Age", "FarmId", "Name" },
                values: new object[] { 20003, 3, null, "Daisy" });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 10001, "Texas", "Sunny Pastures" },
                    { 10002, "Kansas", "Green Valley Ranch" }
                });

            migrationBuilder.InsertData(
                table: "Cows",
                columns: new[] { "Id", "Age", "FarmId", "Name" },
                values: new object[,]
                {
                    { 20001, 4, 10001, "Bessie" },
                    { 20002, 2, 10002, "MooMoo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cows_FarmId",
                table: "Cows",
                column: "FarmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cows");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
