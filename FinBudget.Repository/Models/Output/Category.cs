namespace FinBudget.Repository.Models.Output
{
    public class Category
    {
        public int Id { get; init; }

        public required string Name { get; init; }

        public string? ColorCode { get; init; }
    }
}