using BCrypt.Net;
using Microsoft.CodeAnalysis.Scripting;

namespace IntraManage.Utils
{
    public static class BcryptUtils
    {
        public static string HashPassword (string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword (string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
