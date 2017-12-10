using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Rognar.Helpers;

namespace Rognar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InvalidLogin()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (LoginHelper.LogIn(this, Request["username"], Request["password"]))
                return new RedirectResult("/Home/Index");

            return new RedirectResult("/Home/InvalidLogin");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return new RedirectResult("/Home/Index");
        }

        public ActionResult Register()
        {
            bool submit = Request["submit"] != null;

            if (submit)
            {
                var db = new Rognar.Models.MainDB();
                db.Database.Connection.Open();
                bool success = true;
                string Username = !string.IsNullOrEmpty(Request["username"]) ? Request["username"].Trim() : string.Empty;
                string Email = !string.IsNullOrEmpty(Request["email"]) ? Request["email"].Trim() : string.Empty;

                if (string.IsNullOrEmpty(Request["username"]) || Request["username"].Length < 3)
                {
                    success = false;
                    ViewBag.UnameError = "Username must be at least 3 characters long.";
                }
                else if (db.Users.Where(u => u.Username.ToLower() == Username.ToLower()).FirstOrDefault() != null)
                {
                    success = false;
                    ViewBag.UnameError = "Username is already taken.";
                }

                if (string.IsNullOrEmpty(Request["password"]) || Request["password"].Length < 6)
                {
                    success = false;
                    ViewBag.PassError = "Password must be at least 6 inches long.";
                }

                if (string.IsNullOrEmpty(Request["email"]) || !(new System.ComponentModel.DataAnnotations.EmailAddressAttribute()).IsValid(Request["email"]))
                {
                    success = false;
                    ViewBag.MailError = "Invalid email address.";
                }
                else if (db.Users.Where(u => u.Email.ToLower() == Email.ToLower()).FirstOrDefault() != null)
                {
                    success = false;
                    ViewBag.MailError = "Email is already being used.";
                }

                if (success)
                {
                    try
                    {
                        db.Users.Add(new Rognar.Models.User() { Username = Request["username"].Trim(), Password = Rognar.Helpers.LoginHelper.HashPassword(Request["password"]), Email = Request["email"] });
                        db.SaveChanges();
                        ViewBag.Message = "Account successfully created.";
                        return new RedirectResult("/Home/Index");
                    }
                    catch
                    {
                        ViewBag.Message = "An error has occurred while creating your account.";
                        ViewBag.Error = true;
                    }
                }
            }
            return View();
        }
    }
}