namespace FinBudget.Repository.Models.Create
{
    public class CreateCategoryModel
    {
        public required string Name { get; init; }

        public string? ColorCode { get; init; }
    }
}
