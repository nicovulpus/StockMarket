using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockSymbol = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "text", nullable: false),
                    StockSector = table.Column<string>(type: "text", nullable: false),
                    StockIndustry = table.Column<string>(type: "text", nullable: false),
                    StockMarket = table.Column<string>(type: "text", nullable: false),
                    StockCompanyName = table.Column<string>(type: "text", nullable: false),
                    StockPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    MarketCap = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: true),
                    ChangePercent = table.Column<float>(type: "real", nullable: true),
                    PreviousClose = table.Column<decimal>(type: "numeric", nullable: true),
                    PERatio = table.Column<float>(type: "real", nullable: true),
                    SharesOutstanding = table.Column<long>(type: "bigint", nullable: true),
                    DividendYield = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
