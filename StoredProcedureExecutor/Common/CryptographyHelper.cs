using System.Security;

namespace StoredProcedureExecutor.Common
{
    public static class CryptographyHelper
    {
        public static SecureString ToSecureString(this string password)
        {
            var securePassword = new SecureString();
            foreach (char ch in password)
            {
                securePassword.AppendChar(ch);
            }
            return securePassword;
        }
    }
}
