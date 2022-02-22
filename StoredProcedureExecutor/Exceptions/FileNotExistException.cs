namespace StoredProcedureExecutor.Exceptions;

public class FileNotExistException : BusinessLogicException
{
    public FileNotExistException(string message) : base(message)
    {
    }
}