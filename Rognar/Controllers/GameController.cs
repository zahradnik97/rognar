using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rognar.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult UserInfo()
        {
            ViewBag.Message = ":)";
            return View();
        }
    }
}