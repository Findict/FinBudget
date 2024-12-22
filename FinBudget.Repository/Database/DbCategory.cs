using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Database
{
    [PrimaryKey("Id")]
    internal class DbCategory
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? ColorCode { get; set; }
    }
}