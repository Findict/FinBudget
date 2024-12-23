using FinBudget.Repository.Models.Create;
using FinBudget.Repository.Models.Edit;
using FinBudget.Repository.Models.Output;

namespace FinBudget.Repository.Processors.Interfaces
{
    public interface IFinancialTransactionProcessor
    {
        Task<FinancialTransaction?> GetFinancialTransaction(int id);

        Task<List<FinancialTransaction>> GetFinancialTransactions();

        Task<bool> AddFinancialTransaction(CreateFinancialTransactionModel model);

        Task<bool> UpdateFinancialTransaction(EditFinancialTransactionModel model);
    }
}