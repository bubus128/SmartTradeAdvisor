using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartTradeAdvisor.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NegativeDi_MarketIndexes_MarketIndexId",
                table: "NegativeDi");

            migrationBuilder.DropForeignKey(
                name: "FK_PositiveDi_MarketIndexes_MarketIndexId",
                table: "PositiveDi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositiveDi",
                table: "PositiveDi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NegativeDi",
                table: "NegativeDi");

            migrationBuilder.RenameTable(
                name: "PositiveDi",
                newName: "PositiveDis");

            migrationBuilder.RenameTable(
                name: "NegativeDi",
                newName: "NegativeDis");

            migrationBuilder.RenameIndex(
                name: "IX_PositiveDi_MarketIndexId",
                table: "PositiveDis",
                newName: "IX_PositiveDis_MarketIndexId");

            migrationBuilder.RenameIndex(
                name: "IX_NegativeDi_MarketIndexId",
                table: "NegativeDis",
                newName: "IX_NegativeDis_MarketIndexId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositiveDis",
                table: "PositiveDis",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NegativeDis",
                table: "NegativeDis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NegativeDis_MarketIndexes_MarketIndexId",
                table: "NegativeDis",
                column: "MarketIndexId",
                principalTable: "MarketIndexes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositiveDis_MarketIndexes_MarketIndexId",
                table: "PositiveDis",
                column: "MarketIndexId",
                principalTable: "MarketIndexes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NegativeDis_MarketIndexes_MarketIndexId",
                table: "NegativeDis");

            migrationBuilder.DropForeignKey(
                name: "FK_PositiveDis_MarketIndexes_MarketIndexId",
                table: "PositiveDis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PositiveDis",
                table: "PositiveDis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NegativeDis",
                table: "NegativeDis");

            migrationBuilder.RenameTable(
                name: "PositiveDis",
                newName: "PositiveDi");

            migrationBuilder.RenameTable(
                name: "NegativeDis",
                newName: "NegativeDi");

            migrationBuilder.RenameIndex(
                name: "IX_PositiveDis_MarketIndexId",
                table: "PositiveDi",
                newName: "IX_PositiveDi_MarketIndexId");

            migrationBuilder.RenameIndex(
                name: "IX_NegativeDis_MarketIndexId",
                table: "NegativeDi",
                newName: "IX_NegativeDi_MarketIndexId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PositiveDi",
                table: "PositiveDi",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NegativeDi",
                table: "NegativeDi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NegativeDi_MarketIndexes_MarketIndexId",
                table: "NegativeDi",
                column: "MarketIndexId",
                principalTable: "MarketIndexes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PositiveDi_MarketIndexes_MarketIndexId",
                table: "PositiveDi",
                column: "MarketIndexId",
                principalTable: "MarketIndexes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
