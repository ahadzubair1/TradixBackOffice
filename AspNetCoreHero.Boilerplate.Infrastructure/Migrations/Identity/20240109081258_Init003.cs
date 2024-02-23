using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreHero.Boilerplate.Infrastructure.Migrations.Identity
{
    /// <inheritdoc />
    public partial class Init003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultDownlinePlacementPosition",
                schema: "Identity",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "KycId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkId",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                schema: "Identity",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferredBy",
                schema: "Identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deposit",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposit_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasTiers = table.Column<bool>(type: "bit", nullable: false),
                    HasCollections = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Network",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferredBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Network", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeftPoints = table.Column<double>(type: "float", nullable: false),
                    RightPoints = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    TotalDownlineRanks = table.Column<int>(type: "int", nullable: false),
                    Rewards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownlineRankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NetworkCap = table.Column<double>(type: "float", nullable: false),
                    RenewalX = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rank_Rank_DownlineRankId",
                        column: x => x.DownlineRankId,
                        principalSchema: "Identity",
                        principalTable: "Rank",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    WalletProvider = table.Column<int>(type: "int", nullable: false),
                    WalletAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionCategory = table.Column<int>(type: "int", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WalletType",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AllowTransfer = table.Column<bool>(type: "bit", nullable: false),
                    TransferLimit = table.Column<double>(type: "float", nullable: false),
                    TransferFee = table.Column<double>(type: "float", nullable: false),
                    AllowWithdrawal = table.Column<bool>(type: "bit", nullable: false),
                    WithdrawalLimit = table.Column<double>(type: "float", nullable: false),
                    WithdrawalFee = table.Column<double>(type: "float", nullable: false),
                    AllowDeposit = table.Column<bool>(type: "bit", nullable: false),
                    DepositLimit = table.Column<double>(type: "float", nullable: false),
                    DepositFee = table.Column<double>(type: "float", nullable: false),
                    AllowPurchase = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseLimit = table.Column<double>(type: "float", nullable: false),
                    PurchaseFee = table.Column<double>(type: "float", nullable: false),
                    AllowBonusDeposit = table.Column<bool>(type: "bit", nullable: false),
                    BonusDepositPercentage = table.Column<double>(type: "float", nullable: false),
                    AssignOnCreate = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Withdrawal",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdrawal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Withdrawal_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MembershipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cap = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    EnableRankVolume = table.Column<bool>(type: "bit", nullable: false),
                    EnableNetworkVolume = table.Column<bool>(type: "bit", nullable: false),
                    EnableNetworkBonus = table.Column<bool>(type: "bit", nullable: false),
                    EnableVBonus = table.Column<bool>(type: "bit", nullable: false),
                    EnableDirectReferralBonus = table.Column<bool>(type: "bit", nullable: false),
                    EnableVMastery = table.Column<bool>(type: "bit", nullable: false),
                    EnablePad22 = table.Column<bool>(type: "bit", nullable: false),
                    EnableVWards = table.Column<bool>(type: "bit", nullable: false),
                    IsMembershipRequired = table.Column<bool>(type: "bit", nullable: false),
                    EnableStaking = table.Column<bool>(type: "bit", nullable: false),
                    EnableMetaverse = table.Column<bool>(type: "bit", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpiryIntervalType = table.Column<int>(type: "int", nullable: false),
                    ExpiryInterval = table.Column<int>(type: "int", nullable: false),
                    LoyalityPercentage = table.Column<double>(type: "float", nullable: false),
                    DailyReturnPercentage = table.Column<double>(type: "float", nullable: false),
                    DirectReferralBonusPrecentage = table.Column<double>(type: "float", nullable: false),
                    RankVolume = table.Column<int>(type: "int", nullable: false),
                    NetworkVolume = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membership_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalSchema: "Identity",
                        principalTable: "MembershipTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRank",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRank_Rank_RankId",
                        column: x => x.RankId,
                        principalSchema: "Identity",
                        principalTable: "Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRank_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_WalletType_WalletTypeId",
                        column: x => x.WalletTypeId,
                        principalSchema: "Identity",
                        principalTable: "WalletType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpiryIntervalType = table.Column<int>(type: "int", nullable: false),
                    ExpiryInterval = table.Column<int>(type: "int", nullable: false),
                    ElapsedInterval = table.Column<int>(type: "int", nullable: false),
                    Cap = table.Column<double>(type: "float", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Membership_MembershipId",
                        column: x => x.MembershipId,
                        principalSchema: "Identity",
                        principalTable: "Membership",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "Identity",
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchase_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Identity",
                        principalTable: "Wallet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMembership",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvestmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMembership_Membership_MembershipId",
                        column: x => x.MembershipId,
                        principalSchema: "Identity",
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembership_Purchase_InvestmentId",
                        column: x => x.InvestmentId,
                        principalSchema: "Identity",
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMembership_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VolumeDetails",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetworkVolumeLeft = table.Column<double>(type: "float", nullable: false),
                    NetworkVolumeRight = table.Column<double>(type: "float", nullable: false),
                    RankVolumeLeft = table.Column<double>(type: "float", nullable: false),
                    RankVolumeRight = table.Column<double>(type: "float", nullable: false),
                    HighestMembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeDetails_MembershipTypes_MembershipTypeId",
                        column: x => x.MembershipTypeId,
                        principalSchema: "Identity",
                        principalTable: "MembershipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolumeDetails_Network_NetworkId",
                        column: x => x.NetworkId,
                        principalSchema: "Identity",
                        principalTable: "Network",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolumeDetails_UserMembership_HighestMembershipId",
                        column: x => x.HighestMembershipId,
                        principalSchema: "Identity",
                        principalTable: "UserMembership",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_NetworkId",
                schema: "Identity",
                table: "Users",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_ApplicationUserId",
                schema: "Identity",
                table: "Deposit",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_MembershipTypeId",
                schema: "Identity",
                table: "Membership",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ApplicationUserId",
                schema: "Identity",
                table: "Purchase",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_MembershipId",
                schema: "Identity",
                table: "Purchase",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_TransactionId",
                schema: "Identity",
                table: "Purchase",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_WalletId",
                schema: "Identity",
                table: "Purchase",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Rank_DownlineRankId",
                schema: "Identity",
                table: "Rank",
                column: "DownlineRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ApplicationUserId",
                schema: "Identity",
                table: "Transaction",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembership_ApplicationUserId",
                schema: "Identity",
                table: "UserMembership",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembership_InvestmentId",
                schema: "Identity",
                table: "UserMembership",
                column: "InvestmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembership_MembershipId",
                schema: "Identity",
                table: "UserMembership",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRank_ApplicationUserId",
                schema: "Identity",
                table: "UserRank",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRank_RankId",
                schema: "Identity",
                table: "UserRank",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDetails_HighestMembershipId",
                schema: "Identity",
                table: "VolumeDetails",
                column: "HighestMembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDetails_MembershipTypeId",
                schema: "Identity",
                table: "VolumeDetails",
                column: "MembershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDetails_NetworkId",
                schema: "Identity",
                table: "VolumeDetails",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_WalletTypeId",
                schema: "Identity",
                table: "Wallet",
                column: "WalletTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Withdrawal_ApplicationUserId",
                schema: "Identity",
                table: "Withdrawal",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Network_NetworkId",
                schema: "Identity",
                table: "Users",
                column: "NetworkId",
                principalSchema: "Identity",
                principalTable: "Network",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Network_NetworkId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Deposit",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRank",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "VolumeDetails",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Withdrawal",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Rank",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Network",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserMembership",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Membership",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Wallet",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MembershipTypes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "WalletType",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Users_NetworkId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DefaultDownlinePlacementPosition",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "KycId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Position",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReferredBy",
                schema: "Identity",
                table: "Users");
        }
    }
}
