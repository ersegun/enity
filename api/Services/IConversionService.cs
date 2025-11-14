using System.Threading.Tasks;
using api.Models;

namespace api.Services
{
    public interface IConversionService
    {
        Task<Account> GetConvertedAccount(string currency);
    }
}