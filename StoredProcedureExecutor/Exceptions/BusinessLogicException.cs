using System;

namespace StoredProcedureExecutor.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message) : base(message)
        {

        }
    }
}
