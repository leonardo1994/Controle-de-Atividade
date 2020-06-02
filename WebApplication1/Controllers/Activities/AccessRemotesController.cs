using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    public class AccessRemotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AccessRemotes
        public ActionResult Index()
        {
            var accessRemote = db.AccessRemote.Include(a => a.ApplicationUser);
            return View(accessRemote.ToList());
        }

        // GET: AccessRemotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRemote accessRemote = db.AccessRemote.Find(id);
            if (accessRemote == null)
            {
                return HttpNotFound();
            }
            return View(accessRemote);
        }

        [Authorize]
        // GET: AccessRemotes/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName");

            var userId = HttpContext.User.Identity.GetUserId();
            return View(new AccessRemote() {
                ApplicationUserId = userId
            });
        }

        // POST: AccessRemotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeAccess,Domain,Acesso,User,Password,DirSystem,ConnectionString,TypeBase,DataBaseName,UserSsa,PasswordSsa,ApplicationUserId, CompanyId, PhysicalAddress, Ambiente")] AccessRemote accessRemote)
        {
            if (ModelState.IsValid)
            {
                db.AccessRemote.Add(accessRemote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", accessRemote.CompanyId);
            return View(accessRemote);
        }

        [Authorize]
        // GET: AccessRemotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRemote accessRemote = db.AccessRemote.Find(id);
            if (accessRemote == null)
            {
                return HttpNotFound();
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", accessRemote.CompanyId);
            return View(accessRemote);
        }

        // POST: AccessRemotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeAccess,Domain,Acesso,User,Password,DirSystem,ConnectionString,TypeBase,DataBaseName,UserSsa,PasswordSsa,ApplicationUserId, CompanyId, PhysicalAddress, Ambiente")] AccessRemote accessRemote)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            accessRemote.ApplicationUserId = userId;

            if (ModelState.IsValid)
            {
                db.Entry(accessRemote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Company, "Id", "CompanyName", accessRemote.CompanyId);
            return View(accessRemote);
        }

        [Authorize]
        // GET: AccessRemotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessRemote accessRemote = db.AccessRemote.Find(id);
            if (accessRemote == null)
            {
                return HttpNotFound();
            }
            return View(accessRemote);
        }

        // POST: AccessRemotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessRemote accessRemote = db.AccessRemote.Find(id);
            db.AccessRemote.Remove(accessRemote);
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
