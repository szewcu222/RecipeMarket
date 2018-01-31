using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipeMarket.Models;
using System.Data.Entity;
using RecipeMarket.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using System.Net.Mime;

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



        #region         // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Akcje dla PRZEPISOW - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

       

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przepis przepis = db.Przepisy.First(c => c.PrzepisID == id);

            if (przepis == null)
            {
                return HttpNotFound();
            }

            return View(przepis);
        }

        // GET: Przepisy
        public ActionResult Moje()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            IQueryable<Przepis> listaPrzepisowUzytkownika = db.Przepisy.Where(m => m.UzytkownikID == user.Id);

            if (listaPrzepisowUzytkownika.Count() > 0)
                return View(listaPrzepisowUzytkownika);
            else
                return View();
        }

        public PrzepisyViewModel PopulateSelectList(PrzepisyViewModel przepisyViewModel)
        {
            List<SelectListItem> listSelectListItem = new List<SelectListItem>();

            foreach (Kategoria kat in db.Kategorie)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = kat.NazwaKategorii,
                    Value = kat.KategoriaID.ToString()
                };
                listSelectListItem.Add(selectListItem);
            }


            przepisyViewModel.ListaKategorii = listSelectListItem;

            return przepisyViewModel;

        }

        public ActionResult Create()
        {
            return View(PopulateSelectList(new PrzepisyViewModel()));
        }

        [HttpPost]
        public ActionResult Create(PrzepisyViewModel przepisyViewModel)
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            var przepis = przepisyViewModel.Przepis;

            przepis.DataDodania = DateTime.Now;
            przepis.UzytkownikID = user.Id;

            var kategorie = db.Kategorie.Where(m => m.KategoriaID == przepisyViewModel.Kategoria.KategoriaID).ToList();

            przepisyViewModel.Przepis.Kategorie = kategorie;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(przepis, null, null);

            if (Validator.TryValidateObject(przepis, context, results))
            {
                try
                {
                    db.Przepisy.Add(przepisyViewModel.Przepis);
                    db.SaveChanges();
                    return RedirectToAction("Moje");
                }
                catch (Exception e)
                {
                    return View(PopulateSelectList(przepisyViewModel));
                }

            }

            else
            {


                return View(PopulateSelectList(przepisyViewModel));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var przepis = db.Przepisy.Find(id);
            if (przepis == null)
            {
                return HttpNotFound();
            }

            var user = userManager.FindById(User.Identity.GetUserId());
            if (user.Id == przepis.UzytkownikID)
            {


                return View(PopulateSelectList(new PrzepisyViewModel() { Przepis = przepis }));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmation(PrzepisyViewModel przepisyViewModel)
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            var przepis = przepisyViewModel.Przepis;

            przepis.DataDodania = DateTime.Now;
            przepis.UzytkownikID = user.Id;

            var kategorie = db.Kategorie.Where(m => m.KategoriaID == przepisyViewModel.Kategoria.KategoriaID).ToList();

            przepis.Kategorie.Clear();
            przepis.Kategorie = kategorie;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(przepis, null, null);

            if (Validator.TryValidateObject(przepis, context, results))
            {
                try
                {
                    db.Entry(przepis).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Moje");
                }
                catch (Exception e)
                {
                    return View(PopulateSelectList(przepisyViewModel));
                }

            }

            else
            {


                return View(PopulateSelectList(przepisyViewModel));
            }

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var przepis = db.Przepisy.Find(id);
            if (przepis == null)
            {
                return HttpNotFound();
            }

            var user = userManager.FindById(User.Identity.GetUserId());
            if (user.Id == przepis.UzytkownikID)
            {
                return View(przepis);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            var przepis = db.Przepisy.Find(id);
            db.Przepisy.Remove(przepis);
            db.SaveChanges();
            return RedirectToAction("Moje");
        }



        #endregion

        #region         // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Akcje dla KOMENTARZY - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public ActionResult CreateComment(int id)
        {
            var komentarz = new Komentarz { PrzepisID = id };

            return View(komentarz);
        }


        [HttpPost]
        public ActionResult CreateComment(Komentarz komentarz)
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            komentarz.DataDodania = DateTime.Now;
            komentarz.UzytkownikID = user.Id;

            if (ModelState.IsValid)
            {
                db.Komentarze.Add(komentarz);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = komentarz.PrzepisID });
            }
            else
            {
                return View(komentarz);
            }
        }

        public ActionResult EditComment(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            var komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user.Id == komentarz.UzytkownikID)
            {
                return View(komentarz);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }

        [HttpPost, ActionName("EditComment")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCommentConfirmation(Komentarz komentarz)
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            komentarz.DataDodania = DateTime.Now;
            komentarz.UzytkownikID = user.Id;

            if (ModelState.IsValid)
            {
                db.Entry(komentarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = komentarz.PrzepisID });
            }
            else
            {
                return View(komentarz);
            }
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user.Id == komentarz.UzytkownikID)
            {
                return View(komentarz);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }

        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmation(int id)
        {
            var komentarz = db.Komentarze.Find(id);
            db.Komentarze.Remove(komentarz);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = komentarz.PrzepisID });
        }



        #endregion

    }
}