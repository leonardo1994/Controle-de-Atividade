using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class ScreensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Screens
        public ActionResult Index()
        {
            var screen = db.Screen.Include(s => s.ApplicationUser).Include(s => s.Module);
            return View(screen.ToList());
        }

        // GET: Screens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // GET: Screens/Create
        public ActionResult Create(bool isModal = false)
        {
            TempData["IsModal"] = isModal;
            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name");
            return View(new Screen()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId()
            });
        }

        // POST: Screens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ModuleId,ApplicationUserId")] Screen screen, bool isModal = false)
        {
            if (ModelState.IsValid)
            {
                db.Screen.Add(screen);
                db.SaveChanges();
                if (isModal)
                    return Json(new { screen.Id, Name = $"{screen.Name} - {screen.Description}" });
                else
                    return RedirectToAction("Index");
            }

            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name", screen.ModuleId);
            return View(screen);
        }

        // GET: Screens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name", screen.ModuleId);
            return View(screen);
        }

        // POST: Screens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ModuleId,ApplicationUserId")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(screen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name", screen.ModuleId);
            return View(screen);
        }

        // GET: Screens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Screen screen = db.Screen.Find(id);
            if (screen == null)
            {
                return HttpNotFound();
            }
            return View(screen);
        }

        // POST: Screens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Screen screen = db.Screen.Find(id);
            db.Screen.Remove(screen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
