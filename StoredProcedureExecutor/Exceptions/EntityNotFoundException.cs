namespace StoredProcedureExecutor.Exceptions
{
    public class EntityNotFoundException : BusinessLogicException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}