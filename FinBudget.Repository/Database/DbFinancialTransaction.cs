using Microsoft.EntityFrameworkCore;

namespace FinBudget.Repository.Database
{
    [PrimaryKey("Id")]
    internal class DbFinancialTransaction
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public double? Amount { get; set; }

        public DateTime? Date { get; set; }

        public int? Category { get; set; }

        public int? FromId { get; set; }

        public int? ToId { get; set; }
    }
}