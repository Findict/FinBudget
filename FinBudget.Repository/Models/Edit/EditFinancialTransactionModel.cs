namespace FinBudget.Repository.Models.Edit
{
    public class EditFinancialTransactionModel
    {
        public required int Id { get; init; }

        public string? Description { get; init; }

        public DateTime? Date { get; init; }

        public double? Amount { get; init; }

        public int? FromId { get; init; }

        public int? ToId { get; init; }

        public bool IsEmpty
            => Description == null &&
               Date == null &&
               Amount == null &&
               FromId == null &&
               ToId == null;
    }
}