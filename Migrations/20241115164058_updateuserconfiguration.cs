using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atm_sql.Migrations
{
    public partial class updateuserconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    balance = table.Column<double>(type: "float", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    max_count = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transactionInfos",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    sender_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reciever_Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationAmount = table.Column<double>(type: "float", nullable: false),
                    operationdatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    balancebefore = table.Column<double>(type: "float", nullable: false),
                    balanceafter = table.Column<double>(type: "float", nullable: false),
                    iscomplete = table.Column<bool>(type: "bit", nullable: false),
                    operation_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionInfos", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_transactionInfos_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactionInfos_UserId",
                table: "transactionInfos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionInfos");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
