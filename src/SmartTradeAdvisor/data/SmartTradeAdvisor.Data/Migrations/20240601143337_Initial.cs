using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAdvisor.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketIndexes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketIndexes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Adx",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adx", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adx_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cmo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cmo_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Macd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Macd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Macd_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketIndexValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LowValue = table.Column<double>(type: "double precision", nullable: false),
                    HighValue = table.Column<double>(type: "double precision", nullable: false),
                    ClosingValue = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketIndexValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketIndexValues_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mfi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mfi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mfi_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NegativeDi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegativeDi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegativeDi_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositiveDi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositiveDi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositiveDi_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rsi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rsi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rsi_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    MarketIndexId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ult_MarketIndexes_MarketIndexId",
                        column: x => x.MarketIndexId,
                        principalTable: "MarketIndexes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adx_MarketIndexId",
                table: "Adx",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Cmo_MarketIndexId",
                table: "Cmo",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Macd_MarketIndexId",
                table: "Macd",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketIndexValues_MarketIndexId",
                table: "MarketIndexValues",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Mfi_MarketIndexId",
                table: "Mfi",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_NegativeDi_MarketIndexId",
                table: "NegativeDi",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_PositiveDi_MarketIndexId",
                table: "PositiveDi",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Rsi_MarketIndexId",
                table: "Rsi",
                column: "MarketIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Ult_MarketIndexId",
                table: "Ult",
                column: "MarketIndexId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adx");

            migrationBuilder.DropTable(
                name: "Cmo");

            migrationBuilder.DropTable(
                name: "Macd");

            migrationBuilder.DropTable(
                name: "MarketIndexValues");

            migrationBuilder.DropTable(
                name: "Mfi");

            migrationBuilder.DropTable(
                name: "NegativeDi");

            migrationBuilder.DropTable(
                name: "PositiveDi");

            migrationBuilder.DropTable(
                name: "Rsi");

            migrationBuilder.DropTable(
                name: "Ult");

            migrationBuilder.DropTable(
                name: "MarketIndexes");
        }
    }
}
