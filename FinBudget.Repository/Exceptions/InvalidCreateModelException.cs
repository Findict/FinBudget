namespace FinBudget.Repository.Exceptions
{
    [Serializable]
    public class InvalidCreateModelException : Exception
    {
        internal InvalidCreateModelException()
        {
        }

        internal InvalidCreateModelException(string? message) : base(message)
        {
        }

        internal InvalidCreateModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}