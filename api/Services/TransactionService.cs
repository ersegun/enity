using System;
using System.Collections.Generic;
using api.Models;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(ILogger<TransactionService> logger)
        {
            _logger = logger;
        }

        public (DateTime? start, DateTime? end, decimal highestBalanceChange) GetHighestPositiveBalanceChange(List<Transaction> transactions)
        {
            try
            {
                if (transactions == null || transactions.Count < 2)
                {
                    _logger.LogWarning("Insufficient transactions to calculate balance change. Transaction count: {Count}", transactions?.Count ?? 0);
                    return (null, null, 0);
                }

                DateTime? bestStart = null;
                DateTime? bestEnd = null;
                decimal maxChange = 0;

                DateTime currentStart = transactions[0].Date;
                decimal currentMin = transactions[0].Balance;

                for (int i = 1; i < transactions.Count; i++)
                {
                    var change = transactions[i].Balance - currentMin;
                    
                    if (change > maxChange)
                    {
                        maxChange = change;
                        bestStart = currentStart;
                        bestEnd = transactions[i].Date;
                    }

                    if (transactions[i].Balance < currentMin)
                    {
                        currentMin = transactions[i].Balance;
                        currentStart = transactions[i].Date;
                    }
                }

                if (maxChange <= 0)
                {
                    return (null, null, 0);
                }

                return (bestStart, bestEnd, maxChange);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while calculating highest positive balance change");
                throw new InvalidOperationException("Failed to calculate highest positive balance change.", ex);
            }
        }
    }
}
