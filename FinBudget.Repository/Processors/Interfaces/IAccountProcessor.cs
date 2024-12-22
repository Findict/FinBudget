using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Output;

namespace FinBudget.Repository.Processors.Interfaces
{
    public interface IAccountProcessor
    {
        Task<Account?> GetAccount(int id);

        Task<List<Account>> GetAccounts();

        Task<bool> AddAccount(CreateAccountModel model);

        Task<bool> UpdateAccount(EditAccountModel model);
    }
}