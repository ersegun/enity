using System.Threading.Tasks;
using api.Models;

namespace api.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccount();
    }
}