using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Services;

namespace apitests.Mocks
{
    public class MockExchangeRateService : IExchangeRateService
    {
        public Task<ExchangeRate> GetExchangeRate()
        {
            var exchangeRate = new ExchangeRate()
            {
                Name = "SEK",
                Currencies = new List<Currency>()
                {
                    new Currency()
                    {
                        Name = "DKK",
                        ExchangeRate = 1.1m,
                    }
                }
            };

            return Task.FromResult(exchangeRate);
        }
    }
}
