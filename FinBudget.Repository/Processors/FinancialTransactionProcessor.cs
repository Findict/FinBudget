using FinBudget.Repository.Database;
using FinBudget.Repository.Exceptions;
using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Edit;
using FinBudget.Repository.Processors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Processors
{
    public class FinancialTransactionProcessor : IFinancialTransactionProcessor
    {
        private BudgetDbContext _dbContext;

        public FinancialTransactionProcessor(BudgetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FinancialTransaction?> GetFinancialTransaction(int id)
        {
            var dbResult = await _dbContext.FinancialTransactions.FirstOrDefaultAsync(x => x.Id == id);

            if (dbResult == null) return null;

            return FinancialTransactionFromDbObject(dbResult);
        }

        public async Task<List<FinancialTransaction>> GetFinancialTransactions()
        {
            return await _dbContext.FinancialTransactions.Select(x => FinancialTransactionFromDbObject(x)).ToListAsync();
        }

        public async Task<bool> AddFinancialTransaction(CreateFinancialTransactionModel model)
        {
            if (model.IsEmpty) throw new InvalidCreateModelException("No new financial transaction could be created as the model was empty.");

            var newFinancialTransaction = new DbFinancialTransaction 
            {
                Description = model.Description,
                Date = model.Date,
                Amount = model.Amount,
                FromId = model.FromId,
                ToId = model.ToId,
            };

            _dbContext.FinancialTransactions.Add(newFinancialTransaction);

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> UpdateFinancialTransaction(EditFinancialTransactionModel model)
        {
            var existing = await _dbContext.FinancialTransactions.FindAsync(model.Id);

            if (existing == null)
            {
                if (model.IsEmpty) throw new InvalidEditModelException($"Financial transaction with id {model.Id} was not found, and no new financial transaction could be created as the model was empty.");

                return await AddFinancialTransaction(new() 
                {
                    Description = model.Description,
                    Date = model.Date,
                    Amount = model.Amount,
                    FromId = model.FromId,
                    ToId = model.ToId
                });
            }

            existing.Description = model.Description;
            existing.Date = model.Date;
            existing.Amount = model.Amount;
            existing.FromId = model.FromId;
            existing.ToId = model.ToId;

            var changes = await _dbContext.SaveChangesAsync();

            return changes > 0;
        }

        private FinancialTransaction FinancialTransactionFromDbObject(DbFinancialTransaction dbResult) => new FinancialTransaction
        {
            Id = dbResult.Id,
            Description = dbResult.Description,
            Date = dbResult.Date,
            Amount = dbResult.Amount,
            FromId = dbResult.FromId,
            ToId = dbResult.ToId,
        };
    }
}
