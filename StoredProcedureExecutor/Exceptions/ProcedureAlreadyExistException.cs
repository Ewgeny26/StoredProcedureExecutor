namespace StoredProcedureExecutor.Exceptions
{
    public class ProcedureAlreadyExistException : BusinessLogicException
    {
        public ProcedureAlreadyExistException(string message) : base(message)
        {
        }
    }
}
