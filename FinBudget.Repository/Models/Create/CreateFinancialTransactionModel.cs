﻿
namespace FinBudget.Repository.Models.Create
{
    public class CreateFinancialTransactionModel
    {
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
