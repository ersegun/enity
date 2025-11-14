using System.Collections.Generic;

namespace api.Models
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}