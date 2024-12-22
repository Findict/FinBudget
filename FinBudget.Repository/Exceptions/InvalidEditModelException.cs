namespace FinBudget.Repository.Exceptions
{
    [Serializable]
    public class InvalidEditModelException : Exception
    {
        internal InvalidEditModelException()
        {
        }

        internal InvalidEditModelException(string? message) : base(message)
        {
        }

        internal InvalidEditModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}