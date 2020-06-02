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
    public class LocalErrorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocalErrors
        public ActionResult Index()
        {
            var localError = db.LocalError.Include(l => l.ApplicationUser);
            return View(localError.ToList());
        }

        // GET: LocalErrors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalError localError = db.LocalError.Find(id);
            if (localError == null)
            {
                return HttpNotFound();
            }
            return View(localError);
        }

        // GET: LocalErrors/Create
        public ActionResult Create(bool isModal = false)
        {
            TempData["IsModal"] = isModal;

            return View(new LocalError()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId()
            });
        }

        // POST: LocalErrors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,ApplicationUserId")] LocalError localError, bool isModal = false)
        {
            if (ModelState.IsValid)
            {
                db.LocalError.Add(localError);
                db.SaveChanges();
                if (isModal)
                    return Json(new { localError.Id, Name = localError.Description }, JsonRequestBehavior.AllowGet);
                else
                    return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", localError.ApplicationUserId);
            return View(localError);
        }

        // GET: LocalErrors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalError localError = db.LocalError.Find(id);
            if (localError == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", localError.ApplicationUserId);
            return View(localError);
        }

        // POST: LocalErrors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,ApplicationUserId")] LocalError localError)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localError).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", localError.ApplicationUserId);
            return View(localError);
        }

        // GET: LocalErrors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalError localError = db.LocalError.Find(id);
            if (localError == null)
            {
                return HttpNotFound();
            }
            return View(localError);
        }

        // POST: LocalErrors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocalError localError = db.LocalError.Find(id);
            db.LocalError.Remove(localError);
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
