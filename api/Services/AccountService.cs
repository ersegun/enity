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
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountService> _logger;
        private readonly string _accountUrl;

        public AccountService(HttpClient httpClient, ILogger<AccountService> logger, IOptions<ServiceUrlsOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _accountUrl = options.Value.AccountUrl;
        }

        public async Task<Account> GetAccount()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_accountUrl);
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var account = JsonSerializer.Deserialize<Account>(response, options);
                
                if (account == null)
                {
                    _logger.LogError("Failed to deserialize account data - result was null");
                    throw new InvalidOperationException("Failed to deserialize account data.");
                }
                
                return account;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error occurred while fetching account data from {Url}", _accountUrl);
                throw new InvalidOperationException($"Failed to fetch account data from {_accountUrl}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON deserialization error occurred while parsing account data");
                throw new InvalidOperationException("Failed to parse account data response.", ex);
            }
            catch (Exception ex) when (ex is not InvalidOperationException)
            {
                _logger.LogError(ex, "Unexpected error occurred while fetching account data");
                throw new InvalidOperationException("An unexpected error occurred while fetching account data.", ex);
            }
        }
    }
}
