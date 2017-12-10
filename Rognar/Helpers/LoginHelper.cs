using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public static bool IsLoggedIn(Controller controller)
        {
            return controller.Session["user"] != null;
        }

        public static bool LogIn(Controller controller, string username, string password)
        {
            if (IsLoggedIn(controller))
                return true;

            if (username == null || password == null)
                return false;

            var db = new Rognar.Models.MainDB();
            var user = db.Users.Where(u => u.Username.ToLower() == username.ToLower()).FirstOrDefault();

            if (user == null || !VerifyPassword(password, user.Password))
                return false;

            controller.Session["user"] = user;
            return true;
        }
    }
}