using System;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IAccountService _accountService;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ConversionService> _logger;

        public ConversionService(IAccountService accountService, IExchangeRateService exchangeRateService, ILogger<ConversionService> logger)
        {
            _accountService = accountService;
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        public async Task<Account> GetConvertedAccount(string currency)
        {
            try
            {
                var account = await _accountService.GetAccount();
                var exchangeRate = await _exchangeRateService.GetExchangeRate();

                var targetCurrency = exchangeRate.Currencies.FirstOrDefault(c => c.Name == currency);
                
                if (targetCurrency == null)
                {
                    _logger.LogWarning("Currency {TargetCurrency} not found in available exchange rates. Available currencies: {AvailableCurrencies}", 
                        currency, string.Join(", ", exchangeRate.Currencies.Select(c => c.Name)));
                    throw new ArgumentException($"Currency '{currency}' not found in exchange rates.");
                }

                var rate = targetCurrency.ExchangeRate;

                var convertedAccount = new Account
                {
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance * rate,
                    Currency = currency,
                    Transactions = account.Transactions.Select(t => new Transaction
                    {
                        Date = t.Date,
                        Balance = t.Balance * rate
                    }).ToList()
                };

                return convertedAccount;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while converting account to currency {TargetCurrency}", currency);
                throw new InvalidOperationException($"Failed to convert account to currency '{currency}'.", ex);
            }
        }
    }
}
