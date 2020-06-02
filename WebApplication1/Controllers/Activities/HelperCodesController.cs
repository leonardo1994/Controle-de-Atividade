using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    public class HelperCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var helperCode = db.HelperCode.Include(h => h.ApplicationUser);
            return View(helperCode.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelperCode helperCode = db.HelperCode.Find(id);
            if (helperCode == null)
            {
                return HttpNotFound();
            }
            return View(helperCode);
        }

        [Authorize]
        public ActionResult Create()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            return View(new HelperCode()
            {
                ApplicationUserId = userId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HelperCode helperCode)
        {
            if (ModelState.IsValid)
            {
                db.HelperCode.Add(helperCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(helperCode);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelperCode helperCode = db.HelperCode.Find(id);
            if (helperCode == null)
            {
                return HttpNotFound();
            }
            return View(helperCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(HelperCode helperCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(helperCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(helperCode);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelperCode helperCode = db.HelperCode.Find(id);
            if (helperCode == null)
            {
                return HttpNotFound();
            }
            return View(helperCode);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HelperCode helperCode = db.HelperCode.Find(id);
            db.HelperCode.Remove(helperCode);
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
