using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using api.Configuration;
using api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly string _currenciesUrl;

        public ExchangeRateService(HttpClient httpClient, ILogger<ExchangeRateService> logger, IOptions<ServiceUrlsOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _currenciesUrl = options.Value.CurrenciesUrl;
        }

        public async Task<ExchangeRate> GetExchangeRate()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_currenciesUrl);
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var exchangeRate = JsonSerializer.Deserialize<ExchangeRate>(response, options);
                
                if (exchangeRate == null)
                {
                    _logger.LogError("Failed to deserialize exchange rate data - result was null");
                    throw new InvalidOperationException("Failed to deserialize exchange rate data.");
                }
                
                return exchangeRate;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error occurred while fetching exchange rate data from {Url}", _currenciesUrl);
                throw new InvalidOperationException($"Failed to fetch exchange rate data from {_currenciesUrl}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while parsing exchange rate data");
                throw new InvalidOperationException("Failed to parse exchange rate data response.", ex);
            }
            catch (Exception ex) when (ex is not InvalidOperationException)
            {
                _logger.LogError(ex, "Unexpected error occurred while fetching exchange rate data");
                throw new InvalidOperationException("An unexpected error occurred while fetching exchange rate data.", ex);
            }
        }
    }
}
