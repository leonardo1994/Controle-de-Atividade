using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class SituationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Situations
        public ActionResult Index()
        {
            return View(db.Situation.ToList());
        }

        // GET: Situations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situation.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            return View(situation);
        }

        // GET: Situations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Situations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Description")] Situation situation)
        {
            if (ModelState.IsValid)
            {
                db.Situation.Add(situation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(situation);
        }

        // GET: Situations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situation.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            return View(situation);
        }

        // POST: Situations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Description")] Situation situation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(situation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(situation);
        }

        // GET: Situations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Situation situation = db.Situation.Find(id);
            if (situation == null)
            {
                return HttpNotFound();
            }
            return View(situation);
        }

        // POST: Situations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Situation situation = db.Situation.Find(id);
            db.Situation.Remove(situation);
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
