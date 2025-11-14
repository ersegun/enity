using System.Threading.Tasks;
using api.Contracts;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConversionService _conversionService;
        private readonly ITransactionService _transactionService;

        public AccountController(IConversionService conversionService, ITransactionService transactionService)
        {
            _conversionService = conversionService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<AccountResponse> Get([FromQuery] string currency)
        {
            var convertedAccount = await _conversionService.GetConvertedAccount(currency);
            var (highestEarningStart, highestEarningEnd, balanceChange) =
                _transactionService.GetHighestPositiveBalanceChange(convertedAccount.Transactions);

            return new AccountResponse
            {
                AccountNumber = convertedAccount.AccountNumber,
                Balance = convertedAccount.Balance,
                Currency = convertedAccount.Currency,
                HighestBalanceChangeStart = highestEarningStart,
                HighestBalanceChangeEndDate = highestEarningEnd,
                Transactions = convertedAccount.Transactions,
                HighestBalanceChange = balanceChange
            };
        }
    }
}