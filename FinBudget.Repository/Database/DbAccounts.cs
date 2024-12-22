using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Database
{
    [PrimaryKey("Id")]
    internal class DbAccounts
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}