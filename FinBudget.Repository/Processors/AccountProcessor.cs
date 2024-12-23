using FinBudget.Repository.Database;
using FinBudget.Repository.Exceptions;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Output;
using FinBudget.Repository.Processors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Processors
{
    public class AccountProcessor : IAccountProcessor
    {
        private BudgetDbContext _dbContext;

        public AccountProcessor(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account?> GetAccount(int id)
        {
            var dbResult = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == id);

            if (dbResult == null) return null;

            return AccountFromDbObject(dbResult);
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _dbContext.Accounts.Select(x => AccountFromDbObject(x)).ToListAsync();
        }

        public async Task<bool> AddAccount(CreateAccountModel model)
        {
            var newAccount = new DbAccount { Name = model.Name };

            _dbContext.Accounts.Add(newAccount);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> UpdateAccount(EditAccountModel model)
        {
            var existing = await _dbContext.Accounts.FindAsync(model.Id);

            if (existing == null)
            {
                if (string.IsNullOrWhiteSpace(model.Name)) throw new InvalidEditModelException($"Account with id {model.Id} was not found, and no new account could be created as the name was null.");

                return await AddAccount(new() { Name = model.Name });
            }

            if (model.Name != null) existing.Name = model.Name;

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        private Account AccountFromDbObject(DbAccount dbResult) => new Account
        {
            Id = dbResult.Id,
            Name = dbResult.Name,
        };
    }
}
