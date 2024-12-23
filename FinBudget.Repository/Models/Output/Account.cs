using FinBudget.Repository.Database;

namespace FinBudget.Repository.Models.Output
{
    public class Account
    {
        public required int Id { get; init; }

        public required string Name { get; init; }
    }
}