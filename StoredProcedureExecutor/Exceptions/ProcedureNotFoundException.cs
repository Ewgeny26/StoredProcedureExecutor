namespace StoredProcedureExecutor.Exceptions
{
    public class ProcedureNotFoundException : BusinessLogicException
    {
        public ProcedureNotFoundException(string message) : base(message)
        {
        }
    }
}