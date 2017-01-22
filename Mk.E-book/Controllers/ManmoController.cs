using Mk.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mk.E_book.Controllers
{
    public class ManmoController : Controller
    {
        public ActionResult Home(string year, string month, string no)
        {
            return View(MvcApplication.WebCache.GetManmo(year, month).Home);
        }
        
        public ActionResult Contents(string year, string month, string no)
        {
            return View(MvcApplication.WebCache.GetManmo(year, month).Contents);
        }
        
        public ActionResult Aritcles(string year, string month, string no)
        {
            return View(MvcApplication.WebCache.GetManmoAritcle(year, month, no));
        }
        
        public ActionResult Aritcle(string year, string month, string no)
        {
            return View(MvcApplication.WebCache.GetManmoAritcle(year, month, no));
        }
    }
}