namespace FinBudget.Repository.Models
{
    public class ObjectResult<T>
    {
        public bool Success { get; internal set; } = false;

        public T? Result { get; internal set; }

        public List<string> ErrorMessages { get; internal set; } = new();
    }
}