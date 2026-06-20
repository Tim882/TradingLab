using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradingLab.Journal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "journal_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    mood = table.Column<int>(type: "integer", nullable: false),
                    market_condition = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journal_entries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trading_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trading_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticker = table.Column<string>(type: "text", nullable: false),
                    direction = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    total_entry_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    average_entry_price = table.Column<decimal>(type: "numeric", nullable: false),
                    total_exit_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    average_exit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    realized_pn_l = table.Column<decimal>(type: "numeric", nullable: false),
                    opened_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    closed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    trading_account_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_positions_trading_accounts_trading_account_id",
                        column: x => x.trading_account_id,
                        principalTable: "trading_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticker = table.Column<string>(type: "text", nullable: false),
                    side = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    executed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    trading_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trades", x => x.id);
                    table.ForeignKey(
                        name: "fk_trades_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_trades_trading_accounts_trading_account_id",
                        column: x => x.trading_account_id,
                        principalTable: "trading_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_trade",
                columns: table => new
                {
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trades_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_trade", x => new { x.tags_id, x.trades_id });
                    table.ForeignKey(
                        name: "fk_tag_trade_tags_tags_id",
                        column: x => x.tags_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tag_trade_trades_trades_id",
                        column: x => x.trades_id,
                        principalTable: "trades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trade_notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    trade_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trade_notes", x => x.id);
                    table.ForeignKey(
                        name: "fk_trade_notes_trades_trade_id",
                        column: x => x.trade_id,
                        principalTable: "trades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_positions_trading_account_id",
                table: "positions",
                column: "trading_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_trade_trades_id",
                table: "tag_trade",
                column: "trades_id");

            migrationBuilder.CreateIndex(
                name: "ix_trade_notes_trade_id",
                table: "trade_notes",
                column: "trade_id");

            migrationBuilder.CreateIndex(
                name: "ix_trades_position_id",
                table: "trades",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_trades_trading_account_id",
                table: "trades",
                column: "trading_account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "journal_entries");

            migrationBuilder.DropTable(
                name: "tag_trade");

            migrationBuilder.DropTable(
                name: "trade_notes");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "trades");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "trading_accounts");
        }
    }
}
