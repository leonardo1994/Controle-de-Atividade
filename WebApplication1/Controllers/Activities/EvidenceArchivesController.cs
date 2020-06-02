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
    public class EvidenceArchivesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int evidenceId)
        {
            var evidenceArchive = db.EvidenceArchive.Include(e => e.ApplicationUser).Include(e => e.Evidence).Where(c => c.EvidenceId == evidenceId);
            return View(evidenceArchive.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidenceArchive evidenceArchive = db.EvidenceArchive.Find(id);
            if (evidenceArchive == null)
            {
                return HttpNotFound();
            }
            return View(evidenceArchive);
        }

        public ActionResult Create(int evidenceId)
        {
            return View(new EvidenceArchive()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId(),
                EvidenceId = evidenceId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvidenceArchive evidenceArchive, HttpPostedFileBase[] FileName)
        {
            for (int i = 0; i < FileName.Length; i++)
            {
                if (FileName[i] != null && FileName[i].ContentLength > 0)
                {
                    evidenceArchive.FileName = System.IO.Path.GetFileName(FileName[i].FileName);
                    evidenceArchive.ContentType = FileName[i].ContentType;
                    using (var reader = new System.IO.BinaryReader(FileName[i].InputStream))
                    {
                        evidenceArchive.Content = reader.ReadBytes(FileName[i].ContentLength);
                    }
                    db.EvidenceArchive.Add(new EvidenceArchive
                    {
                        ApplicationUserId = evidenceArchive.ApplicationUserId,
                        Content = evidenceArchive.Content,
                        ContentType = evidenceArchive.ContentType,
                        EvidenceId = evidenceArchive.EvidenceId,
                        FileName = evidenceArchive.FileName
                    });
                }
            }
            db.SaveChanges();

            return RedirectToAction("Edit", "Evidences", new { id = evidenceArchive.EvidenceId });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvidenceArchive evidenceArchive = db.EvidenceArchive.Find(id);
            if (evidenceArchive == null)
            {
                return HttpNotFound();
            }
            return View(evidenceArchive);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvidenceArchive evidenceArchive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidenceArchive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Evidences", new { id = evidenceArchive.EvidenceId });
            }
            return RedirectToAction("Edit", "Evidences", new { id = evidenceArchive.EvidenceId });
        }

        public ActionResult Delete(int? id)
        {
            EvidenceArchive evidenceArchive = db.EvidenceArchive.Find(id);
            db.EvidenceArchive.Remove(evidenceArchive);
            db.SaveChanges();
            return RedirectToAction("Edit", "Evidences", new { id = evidenceArchive.EvidenceId });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvidenceArchive evidenceArchive = db.EvidenceArchive.Find(id);
            db.EvidenceArchive.Remove(evidenceArchive);
            db.SaveChanges();
            return RedirectToAction("Edit", "Evidences", new { id = evidenceArchive.EvidenceId });
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