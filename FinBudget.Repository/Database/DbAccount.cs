using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Database
{
    [PrimaryKey("Id")]
    internal class DbAccount
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}