
namespace FinBudget.Repository.Processors
{
    public class FinancialTransaction
    {
        public int Id { get; internal set; }
        public string? Description { get; internal set; }
        public DateTime? Date { get; internal set; }
        public double? Amount { get; internal set; }
        public int? FromId { get; internal set; }
        public int? ToId { get; internal set; }
    }
}