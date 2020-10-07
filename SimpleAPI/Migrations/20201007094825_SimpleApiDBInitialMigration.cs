using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleAPI.Migrations
{
    public partial class SimpleApiDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Company = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Company", "CreatedAt", "FirstName", "LastName" },
                values: new object[] { 1, "Company 1", new DateTime(2020, 10, 7, 12, 48, 25, 51, DateTimeKind.Local), "John", "Doe" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Company", "CreatedAt", "FirstName", "LastName" },
                values: new object[] { 2, "Company 2", new DateTime(2020, 10, 7, 12, 48, 25, 51, DateTimeKind.Local), "Mark", "Kong" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Company", "CreatedAt", "FirstName", "LastName" },
                values: new object[] { 3, "Company 3", new DateTime(2020, 10, 7, 12, 48, 25, 51, DateTimeKind.Local), "Nick", "Cave" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "Code", "CreatedAt", "IsPaid", "Status" },
                values: new object[,]
                {
                    { 3, 1, "QWE135", new DateTime(2020, 10, 7, 12, 48, 25, 52, DateTimeKind.Local), false, "Cancelled" },
                    { 4, 1, "JKL246", new DateTime(2020, 10, 7, 12, 48, 25, 52, DateTimeKind.Local), true, "Cancelled" },
                    { 1, 2, "ASD123", new DateTime(2020, 10, 7, 12, 48, 25, 52, DateTimeKind.Local), false, "New Order" },
                    { 2, 3, "ZXC456", new DateTime(2020, 10, 7, 12, 48, 25, 52, DateTimeKind.Local), true, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
