using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rognar.Helpers
{
    public static class LoginHelper
    {
        public static string HashPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }

        public static bool VerifyPassword(string pass, string check)
        {
            return BCrypt.Net.BCrypt.Verify(pass, check);
        }
    }
}