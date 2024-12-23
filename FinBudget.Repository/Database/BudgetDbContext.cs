using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Database
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions options) : base(options) { }

        internal DbSet<DbCategory> Categories { get; set; }

        internal DbSet<DbAccount> Accounts { get; set; }

        internal DbSet<DbFinancialTransaction> FinancialTransactions { get; set; }
    }
}
