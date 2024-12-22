using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinBudget.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "FinancialTransactions",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "FinancialTransactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FinancialTransactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "FinancialTransactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "FinancialTransactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Categories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");
        }
    }
}
