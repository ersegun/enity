using System.Threading.Tasks;
using api.Models;
using api.Services;

namespace apitests.Mocks
{
    public class MockAccountService : IAccountService
    {
        public Task<Account> GetAccount()
        {
            var account = new Account()
            {
                AccountNumber = "123456-1234",
                Balance = 100,
                Currency = "SEK"
            };

            return Task.FromResult(account);
        }
    }
}