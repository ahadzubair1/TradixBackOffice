﻿using System;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Account.Models
{
    public class WalletViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ReferenceId { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public double Amount { get; set; }
        public WalletProvider WalletProvider { get; set; }
        public string? WalletAddress { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
    }
}