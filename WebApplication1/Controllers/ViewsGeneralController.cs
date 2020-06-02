using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers
{
    public class ViewsGeneralController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ViewSolutions
        public ActionResult Index()
        {
            return View(_db.EvidenceSolution.ToList());
        }

        [HttpGet]
        public ActionResult SearchView(string Pesquisa)
        {
            var arraySearch = Pesquisa.Split(' ');
            var search = arraySearch.Aggregate<string, IQueryable<EvidenceSolution>>(_db.EvidenceSolution, (current, s) => current.Where(p => p.Evidence.Activity.Title.Contains(s) || p.Evidence.Description.Contains(s) || p.Evidence.LocalError.Description.Contains(s) || p.Evidence.Problem.Title.Contains(s) || p.Evidence.Problem.Description.Contains(s) || p.Solution.Description.Contains(s) || p.Evidence.Activity.Company.CompanyName.Contains(s) || p.Evidence.Screen.Description.Contains(s) || p.Evidence.Screen.Name.Contains(s) || p.Evidence.Screen.Module.Name.Contains(s) || p.Evidence.Title.Contains(s)));

            return View("Index", search.ToList());
        }
    }
}