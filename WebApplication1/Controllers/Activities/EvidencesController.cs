using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class EvidencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evidences
        public ActionResult Index(int activityId)
        {
            var evidence = db.Evidence.Include(e => e.Activity).Include(e => e.ApplicationUser).Include(e => e.EvidenceOld).Include(e => e.LocalError).Include(e => e.Problem).Include(e => e.Requester).Include(e => e.Screen).Where(e => e.ActivityId == activityId);
            return View(evidence.ToList());
        }

        // GET: Evidences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidence evidence = db.Evidence.Find(id);
            if (evidence == null)
            {
                return HttpNotFound();
            }
            return View(evidence);
        }

        // GET: Evidences/Create
        public ActionResult Create(int activityId = 0, int requesterId = 0)
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Title");
            ViewBag.EvidenceOldId = new SelectList(db.Evidence.Where(e => e.ActivityId == activityId), "Id", "Title");
            ViewBag.LocalErrorId = new SelectList(db.LocalError, "Id", "Description");
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title");
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name");
            ViewBag.ScreenId = new SelectList(from screen in db.Screen select new { screen.Id, Name = screen.Name + " - " + screen.Description }, "Id", "Name");
            ViewBag.SolutionId = new SelectList(db.Solution.Where(c => c.EvidenceSolutions.Any(e => e.Evidence.ActivityId == activityId)), "Id", "Description");
            return View(new Evidence()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId(),
                InitalDate = DateTime.Now,
                ActivityId = activityId,
                RequesterId = requesterId
            });
        }

        // POST: Evidences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Evidence evidence)
        {
            if (ModelState.IsValid)
            {
                db.Evidence.Add(evidence);
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = evidence.ActivityId });
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Title", evidence.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", HttpContext.User.Identity.GetUserId());
            ViewBag.EvidenceOldId = new SelectList(db.Evidence.Where(e => e.ActivityId == evidence.ActivityId), "Id", "Title");
            ViewBag.LocalErrorId = new SelectList(db.LocalError, "Id", "Description", evidence.LocalErrorId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", evidence.ProblemId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", evidence.RequesterId);
            ViewBag.ScreenId = new SelectList(from screen in db.Screen select new { screen.Id, Name = screen.Name + " - " + screen.Description }, "Id", "Name", evidence.ScreenId);
            ViewBag.SolutionId = new SelectList(db.Solution.Where(c => c.EvidenceSolutions.Any(e => e.Evidence.ActivityId == evidence.ActivityId)), "Id", "Description");
            return View(evidence);
        }

        // GET: Evidences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidence evidence = db.Evidence.Find(id);
            if (evidence == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Title", evidence.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", evidence.ApplicationUserId);
            ViewBag.EvidenceOldId = new SelectList(db.Evidence.Where(e => e.ActivityId == evidence.ActivityId), "Id", "Title", evidence.EvidenceOldId);
            ViewBag.LocalErrorId = new SelectList(db.LocalError, "Id", "Description", evidence.LocalErrorId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", evidence.ProblemId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", evidence.RequesterId);
            ViewBag.ScreenId = new SelectList(from screen in db.Screen select new { screen.Id, Name = screen.Name + " - " + screen.Description }, "Id", "Name", evidence.ScreenId);
            HtmlHelper htmlHelper = new HtmlHelper(new ViewContext(), new ViewPage());
            var solucoes = db.Solution.Where(c => c.EvidenceSolutions.Any(e => e.Evidence.ActivityId == evidence.ActivityId)).ToList();
            ViewBag.SolutionId = new SelectList(
                (from ev in solucoes
                 select new { Id = ev.Id, Description = htmlHelper.Raw(MvcHtmlString.Create(HttpContext.Server.HtmlDecode(ev.Description)).ToHtmlString()) }).ToList(), "Id", "Description", evidence.SolutionId);
            return View(evidence);
        }

        // POST: Evidences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Evidence evidence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evidence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = evidence.ActivityId });
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Title", evidence.ActivityId);
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", evidence.ApplicationUserId);
            ViewBag.EvidenceOldId = new SelectList(db.Evidence.Where(e => e.ActivityId == evidence.ActivityId), "Id", "Title", evidence.EvidenceOldId);
            ViewBag.LocalErrorId = new SelectList(db.LocalError, "Id", "Description", evidence.LocalErrorId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", evidence.ProblemId);
            ViewBag.RequesterId = new SelectList(db.Requester, "Id", "Name", evidence.RequesterId);
            ViewBag.ScreenId = new SelectList(from screen in db.Screen select new { screen.Id, Name = screen.Name + " - " + screen.Description }, "Id", "Name", evidence.ScreenId);
            ViewBag.SolutionId = new SelectList(db.Solution.Where(c => c.EvidenceSolutions.Any(e => e.Evidence.ActivityId == evidence.ActivityId)), "Id", "Description", evidence.SolutionId);
            return View(evidence);
        }

        // GET: Evidences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evidence evidence = db.Evidence.Find(id);
            if (evidence == null)
            {
                return HttpNotFound();
            }
            return View(evidence);
        }

        // POST: Evidences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evidence evidence = db.Evidence.Find(id);
            db.Evidence.Remove(evidence);
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
