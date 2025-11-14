using System;
using System.Collections.Generic;
using api.Models;

namespace api.Contracts
{
    public class AccountResponse
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime? HighestBalanceChangeStart { get; set; }
        public DateTime? HighestBalanceChangeEndDate { get; set; }
        public decimal HighestBalanceChange { get; set; }
    }
}