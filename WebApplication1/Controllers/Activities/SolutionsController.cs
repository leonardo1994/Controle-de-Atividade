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
    public class SolutionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Solutions
        public ActionResult Index()
        {
            var solution = db.Solution.Include(s => s.ApplicationUser).Include(s => s.Problem).Include(s => s.Situation).Include(s => s.SolutionBase).Include(s => s.SolutionOld);
            return View(solution.ToList());
        }

        public ActionResult IndexEvidence(int evidenceId)
        {
            ViewBag.EvidenceId = evidenceId;
            var solution = (from s in db.Solution
                            join es in db.EvidenceSolution on s.Id equals es.SolutionId
                            join e in db.Evidence on es.EvidenceId equals e.Id
                            where e.Id == evidenceId
                            select s).ToList();
            return View("Index", solution);
        }

        // GET: Solutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solution solution = db.Solution.Find(id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        // GET: Solutions/Create
        public ActionResult Create(int problemId, int evidenceId)
        {
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", problemId);
            ViewBag.SituationId = new SelectList(db.Situation, "Id", "Code");
            ViewBag.SolutionBaseId = new SelectList(db.Solution, "Id", "Description");
            ViewBag.SolutionOldId = new SelectList(db.Solution, "Id", "Description");
            ViewBag.EvidenceId = evidenceId;
            ViewBag.ActivityId = db.Evidence.Find(evidenceId).ActivityId;
            return View(new Solution() { ProblemId = problemId, ApplicationUserId = HttpContext.User.Identity.GetUserId(), ApplicationUser = db.Users.Find(HttpContext.User.Identity.GetUserId()) });
        }

        // POST: Solutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Solution solution, int evidenceId)
        {
            if (ModelState.IsValid)
            {
                db.Solution.Add(solution);
                db.SaveChanges();
                db.EvidenceSolution.Add(new EvidenceSolution()
                {
                    EvidenceId = evidenceId,
                    SolutionId = solution.Id,
                    ApplicationUserId = solution.ApplicationUserId
                });
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = db.Evidence.Find(evidenceId).ActivityId });
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", solution.ApplicationUserId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", solution.ProblemId);
            ViewBag.SituationId = new SelectList(db.Situation, "Id", "Code", solution.SituationId);
            ViewBag.SolutionBaseId = new SelectList(db.Solution, "Id", "Description", solution.SolutionBaseId);
            ViewBag.SolutionOldId = new SelectList(db.Solution, "Id", "Description", solution.SolutionOldId);
            ViewBag.EvidenceId = evidenceId;
            ViewBag.ActivityId = db.Evidence.Find(evidenceId).ActivityId;
            return View(solution);
        }

        // GET: Solutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solution solution = db.Solution.Find(id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", solution.ApplicationUserId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", solution.ProblemId);
            ViewBag.SituationId = new SelectList(db.Situation, "Id", "Code", solution.SituationId);
            ViewBag.SolutionBaseId = new SelectList(db.Solution, "Id", "Description", solution.SolutionBaseId);
            ViewBag.SolutionOldId = new SelectList(db.Solution, "Id", "Description", solution.SolutionOldId);
            return View(solution);
        }

        // POST: Solutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Solution solution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Activities");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Name", solution.ApplicationUserId);
            ViewBag.ProblemId = new SelectList(db.Problem, "Id", "Title", solution.ProblemId);
            ViewBag.SituationId = new SelectList(db.Situation, "Id", "Code", solution.SituationId);
            ViewBag.SolutionBaseId = new SelectList(db.Solution, "Id", "Description", solution.SolutionBaseId);
            ViewBag.SolutionOldId = new SelectList(db.Solution, "Id", "Description", solution.SolutionOldId);
            return View(solution);
        }

        // GET: Solutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solution solution = db.Solution.Find(id);
            if (solution == null)
            {
                return HttpNotFound();
            }
            return View(solution);
        }

        // POST: Solutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solution solution = db.Solution.Find(id);
            db.Solution.Remove(solution);
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
