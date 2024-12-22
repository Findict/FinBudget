using FinBudget.Repository.Database;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Output;
using FinBudget.Repository.Processors.Interfaces;

namespace FinBudget.Repository.Processors
{
    internal class AccountProcessor : IAccountProcessor
    {
        private BudgetDbContext _dbContext;

        public AccountProcessor(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> AddAccount(CreateAccountModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAccounts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAccount(EditAccountModel model)
        {
            throw new NotImplementedException();
        }
    }
}
