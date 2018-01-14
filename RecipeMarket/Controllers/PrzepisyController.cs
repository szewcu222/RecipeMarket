using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecipeMarket.DAL;
using RecipeMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RecipeMarket.Controllers
{
    public class PrzepisyController : Controller
    {
        protected PrzepisyContext db { get; set; }
        protected UserManager<Uzytkownik> userManager { get; set; }

        public PrzepisyController()
        {
            db = new PrzepisyContext();
            userManager = new UserManager<Uzytkownik>(new UserStore<Uzytkownik>(db));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przepis przepis = db.Przepisy.FirstOrDefault(c => c.PrzepisID == id);

            if (przepis == null)
            {
                return HttpNotFound();
            }

            return View(przepis);
        }
    }
}