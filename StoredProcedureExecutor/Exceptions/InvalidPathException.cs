namespace StoredProcedureExecutor.Exceptions
{
    public class InvalidPathException : BusinessLogicException
    {
        public InvalidPathException(string message) : base(message)
        {
        }
    }
}