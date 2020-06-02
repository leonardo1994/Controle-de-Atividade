using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var activity = db.Activity.Include(a => a.ApplicationUser).Include(a => a.Company).Include(a => a.Requester).Include(a => a.RequestMeans).Where(c => c.ApplicationUserId == userId);
            return View(activity.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var activity = new Activity()
            {
                ApplicationUserId = userId,
                ApplicationUser = db.Users.Find(userId),
                RequestDate = DateTime.Now
            };

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName");
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name");
            ViewBag.RequestMeansId = new SelectList(db.RequestMeans, "Id", "Description");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Description");

            return View(activity);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Activity activity, Evidence evidence = null)
        {
            if (ModelState.IsValid)
            {
                activity.ApplicationUserId = HttpContext.User.Identity.GetUserId();
                db.Activity.Add(activity);
                db.SaveChanges();
                if (evidence != null)
                {
                    evidence.ApplicationUserId = HttpContext.User.Identity.GetUserId();
                    evidence.ActivityId = activity.Id;
                    db.Evidence.Add(evidence);
                    db.SaveChanges();
                }

                return RedirectToAction("Edit", new { id = activity.Id });
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", activity.ApplicationUserId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", activity.CompanyId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", activity.RequesterId);
            ViewBag.RequestMeansId = new SelectList(db.RequestMeans, "Id", "Description", activity.RequestMeansId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Description", activity.StatusId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", activity.ApplicationUserId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", activity.CompanyId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", activity.RequesterId);
            ViewBag.RequestMeansId = new SelectList(db.RequestMeans, "Id", "Description", activity.RequestMeansId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Code", activity.StatusId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", activity.ApplicationUserId);
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", activity.CompanyId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", activity.RequesterId);
            ViewBag.RequestMeansId = new SelectList(db.RequestMeans, "Id", "Description", activity.RequestMeansId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "Code", activity.StatusId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Closed(int id)
        {
            var activity = db.Activity.Find(id);
            activity.FinalDate = DateTime.Now;
            db.Entry(activity).State = EntityState.Modified;
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
