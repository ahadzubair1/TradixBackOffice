﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init0039 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MembershipId",
                schema: "dbo",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_MembershipId",
                schema: "dbo",
                table: "Purchases",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_TransactionId",
                schema: "dbo",
                table: "Purchases",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_WalletId",
                schema: "dbo",
                table: "Purchases",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Memberships_MembershipId",
                schema: "dbo",
                table: "Purchases",
                column: "MembershipId",
                principalSchema: "dbo",
                principalTable: "Memberships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                schema: "dbo",
                table: "Purchases",
                column: "TransactionId",
                principalSchema: "dbo",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Wallets_WalletId",
                schema: "dbo",
                table: "Purchases",
                column: "WalletId",
                principalSchema: "dbo",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Memberships_MembershipId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Wallets_WalletId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_MembershipId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_TransactionId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_WalletId",
                schema: "dbo",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                schema: "dbo",
                table: "Purchases");
        }
    }
}
