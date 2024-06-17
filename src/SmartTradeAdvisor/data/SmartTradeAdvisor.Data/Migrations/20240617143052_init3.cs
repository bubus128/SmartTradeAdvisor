using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAdvisor.Data.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Macd");

            migrationBuilder.CreateTable(
                name: "MacdSignal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacdSignal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacdSignal_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MacdSignal_MarketIndexId",
                table: "MacdSignal",
                column: "MarketIndexId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MacdSignal");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Macd",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
