using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class SolutionHelperCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int solutionId)
        {
            var solutionHelperCode = db.SolutionHelperCode.Include(s => s.ApplicationUser).Include(s => s.HelperCode).Include(s => s.Solution).Where(c => c.SolutionId == solutionId);
            return View(solutionHelperCode.ToList());
        }

        public ActionResult Create(int solutionId, int helperCodeId)
        {
            return View(new SolutionHelperCode()
            {
                ApplicationUserId = HttpContext.User.Identity.GetUserId(),
                SolutionId = solutionId,
                HelperCodeId = helperCodeId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SolutionHelperCode solutionHelperCode)
        {
            db.SolutionHelperCode.Add(solutionHelperCode);
            db.SaveChanges();
            return RedirectToAction("Index", "Solution",
                new { Id = solutionHelperCode.SolutionId });
        }

        public ActionResult Delete(int? id)
        {
            SolutionHelperCode solutionHelperCode = db.SolutionHelperCode.Find(id);
            db.SolutionHelperCode.Remove(solutionHelperCode);
            db.SaveChanges();
            return RedirectToAction("Index", "Solution",
                new { Id = solutionHelperCode.SolutionId });
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
