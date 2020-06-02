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
    public class RequestMeansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestMeans
        public ActionResult Index()
        {
            var requestMeans = db.RequestMeans.Include(r => r.ApplicationUser);
            return View(requestMeans.ToList());
        }

        // GET: RequestMeans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeans requestMeans = db.RequestMeans.Find(id);
            if (requestMeans == null)
            {
                return HttpNotFound();
            }
            return View(requestMeans);
        }

        // GET: RequestMeans/Create
        public ActionResult Create(bool isModal = false)
        {
            TempData["IsModal"] = isModal;
            return View(new RequestMeans() {
                ApplicationUserId = HttpContext.User.Identity.GetUserId()
            });
        }

        // POST: RequestMeans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,ApplicationUserId")] RequestMeans requestMeans, bool isModal = false)
        {
            if (ModelState.IsValid)
            {
                db.RequestMeans.Add(requestMeans);
                db.SaveChanges();
                if (isModal)
                    return Json(new { requestMeans.Id, Name = requestMeans.Description }, JsonRequestBehavior.AllowGet);
                else
                    return RedirectToAction("Index");
            }

            
            return View(requestMeans);
        }

        // GET: RequestMeans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeans requestMeans = db.RequestMeans.Find(id);
            if (requestMeans == null)
            {
                return HttpNotFound();
            }
            return View(requestMeans);
        }

        // POST: RequestMeans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,ApplicationUserId")] RequestMeans requestMeans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestMeans).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestMeans);
        }

        // GET: RequestMeans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeans requestMeans = db.RequestMeans.Find(id);
            if (requestMeans == null)
            {
                return HttpNotFound();
            }
            return View(requestMeans);
        }

        // POST: RequestMeans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestMeans requestMeans = db.RequestMeans.Find(id);
            db.RequestMeans.Remove(requestMeans);
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
