using System;
using System.Collections.Generic;
using api.Models;

namespace api.Services
{
    public interface ITransactionService
    {
        (DateTime? start, DateTime? end, decimal highestBalanceChange) GetHighestPositiveBalanceChange(List<Transaction> transactions);
    }
}