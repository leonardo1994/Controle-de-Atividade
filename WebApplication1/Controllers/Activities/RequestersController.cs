using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class RequestersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requesters
        public ActionResult Index()
        {
            var requester = db.Requester.Include(r => r.ApplicationUser).Include(r => r.Company);
            return View(requester.ToList());
        }

        // GET: Requesters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requester requester = db.Requester.Find(id);
            if (requester == null)
            {
                return HttpNotFound();
            }
            return View(requester);
        }

        // GET: Requesters/Create
        public ActionResult Create(bool isModal = false)
        {
            TempData["IsModal"] = isModal;
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName");
            return View(new Requester()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId()
            });
        }

        // POST: Requesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phones,Email,Skype,CompanyId,ApplicationUserId")] Requester requester, bool isModal = false)
        {
            if (ModelState.IsValid)
            {
                db.Requester.Add(requester);
                db.SaveChanges();
                if (isModal)
                    return Json(requester, JsonRequestBehavior.AllowGet);
                else
                    return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", requester.CompanyId);
            return View(requester);
        }

        // GET: Requesters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requester requester = db.Requester.Find(id);
            if (requester == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", requester.CompanyId);
            return View(requester);
        }

        // POST: Requesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phones,Email,Skype,CompanyId,ApplicationUserId")] Requester requester)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", requester.CompanyId);
            return View(requester);
        }

        // GET: Requesters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requester requester = db.Requester.Find(id);
            if (requester == null)
            {
                return HttpNotFound();
            }
            return View(requester);
        }

        // POST: Requesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requester requester = db.Requester.Find(id);
            db.Requester.Remove(requester);
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
