using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Contract.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProposalId = table.Column<int>(type: "integer", nullable: false),
                    InsuranceNameHolder = table.Column<string>(type: "text", nullable: false),
                    ProposalStatus = table.Column<string>(type: "text", nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    MonthlyBill_Value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyBill_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "ContractDate", "InsuranceNameHolder", "ProposalId", "ProposalStatus", "CPF", "MonthlyBill_Currency", "MonthlyBill_Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "João da Silva", 1, "Approved", "07038612042", "BRL", 149.90m },
                    { 2, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria Oliveira", 2, "Approved", "20791888010", "BRL", 199.50m },
                    { 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos Pereira", 3, "Approved", "14590488060", "BRL", 99.00m },
                    { 4, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana Souza", 4, "Approved", "17448756001", "BRL", 250.00m },
                    { 5, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pedro Santos", 5, "Approved", "68456412007", "BRL", 300.75m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
