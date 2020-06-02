using Microsoft.AspNet.Identity;
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
    public class SolutionArchivesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SolutionArchives
        public ActionResult Index(int solutionId)
        {
            var solutionArchive = db.SolutionArchive.Include(s => s.ApplicationUser).Include(s => s.Solution).Where(c => c.SolutionId == solutionId);
            return View(solutionArchive.ToList());
        }

        // GET: SolutionArchives/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolutionArchive solutionArchive = db.SolutionArchive.Find(id);
            if (solutionArchive == null)
            {
                return HttpNotFound();
            }
            return View(solutionArchive);
        }

        // GET: SolutionArchives/Create
        public ActionResult Create(int solutionId)
        {
            return View(new SolutionArchive() {
                ApplicationUserId = HttpContext.User.Identity.GetUserId(),
                SolutionId = solutionId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SolutionArchive solutionArchive, HttpPostedFileBase[] FileName)
        {
            for (int i = 0; i < FileName.Length; i++)
            {
                if (FileName[i] != null && FileName[i].ContentLength > 0)
                {
                    solutionArchive.FileName = System.IO.Path.GetFileName(FileName[i].FileName);
                    solutionArchive.ContentType = FileName[i].ContentType;
                    using (var reader = new System.IO.BinaryReader(FileName[i].InputStream))
                    {
                        solutionArchive.Content = reader.ReadBytes(FileName[i].ContentLength);
                    }
                    db.SolutionArchive.Add(new SolutionArchive
                    {
                        ApplicationUserId = solutionArchive.ApplicationUserId,
                        Content = solutionArchive.Content,
                        ContentType = solutionArchive.ContentType,
                        SolutionId = solutionArchive.SolutionId,
                        FileName = solutionArchive.FileName
                    });
                }
            }
            db.SaveChanges();

            return RedirectToAction("Edit", "Evidences", new { id = solutionArchive.SolutionId });
        }

        // GET: SolutionArchives/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolutionArchive solutionArchive = db.SolutionArchive.Find(id);
            if (solutionArchive == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", solutionArchive.ApplicationUserId);
            ViewBag.SolutionId = new SelectList(db.Solution, "Id", "Description", solutionArchive.SolutionId);
            return View(solutionArchive);
        }

        // POST: SolutionArchives/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SolutionArchive solutionArchive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solutionArchive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", solutionArchive.ApplicationUserId);
            ViewBag.SolutionId = new SelectList(db.Solution, "Id", "Description", solutionArchive.SolutionId);
            return View(solutionArchive);
        }

        // GET: SolutionArchives/Delete/5
        public ActionResult Delete(int? id)
        {
            SolutionArchive solutionArchive = db.SolutionArchive.Find(id);
            db.SolutionArchive.Remove(solutionArchive);
            db.SaveChanges();
            return RedirectToAction("Index", "SolutionArchives", new { id = solutionArchive.SolutionId });
        }

        // POST: SolutionArchives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolutionArchive solutionArchive = db.SolutionArchive.Find(id);
            db.SolutionArchive.Remove(solutionArchive);
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
