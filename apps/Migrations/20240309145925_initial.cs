using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apps.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promo_master",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    qty = table.Column<int>(type: "integer", nullable: false),
                    qty_remaining = table.Column<int>(type: "integer", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_remaining = table.Column<decimal>(type: "numeric", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_master", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "promo_transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_balance = table.Column<decimal>(type: "numeric", nullable: false),
                    trans_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    commited = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_transaction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promo_transaction_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    promo_transaction_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    qty = table.Column<int>(type: "integer", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active_flag = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promo_transaction_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_promo_transaction_detail_promo_transaction_promo_transactio~",
                        column: x => x.promo_transaction_id,
                        principalTable: "promo_transaction",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_promo_transaction_detail_promo_transaction_id",
                table: "promo_transaction_detail",
                column: "promo_transaction_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promo_master");

            migrationBuilder.DropTable(
                name: "promo_transaction_detail");

            migrationBuilder.DropTable(
                name: "promo_transaction");
        }
    }
}
