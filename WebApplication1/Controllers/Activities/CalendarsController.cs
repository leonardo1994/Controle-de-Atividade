using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using WebApplication1.Models.Account;
using WebApplication1.Models.Activities;

namespace WebApplication1.Controllers.Activities
{
    [Authorize]
    public class CalendarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calendars
        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var calendar = db.Calendar.Include(c => c.ApplicationUser).Include(c => c.Evidence).Where(c => c.DateInitial.Month == DateTime.Now.Month).Where(c => c.ApplicationUserId == userId);
            return View(calendar.ToList());
        }

        public ActionResult Retroceder(int mes)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var calendar = db.Calendar.Include(c => c.ApplicationUser).Include(c => c.Evidence).Where(c => c.DateInitial.Month == mes - 1).Where(c => c.ApplicationUserId == userId);
            return View("Index", calendar.ToList());
        }

        public ActionResult Avancar(int mes)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var calendar = db.Calendar.Include(c => c.ApplicationUser).Include(c => c.Evidence).Where(c => c.DateInitial.Month == mes + 1).Where(c => c.ApplicationUserId == userId);
            return View("Index", calendar.ToList());
        }

        public ActionResult Open(int evidenceId)
        {
            ViewBag.ActivityId = db.Evidence.Find(evidenceId).ActivityId;

            return View(new Calendar()
            {
                EvidenceId = evidenceId,
                DateInitial = DateTime.Now,
                TimeInitial = DateTime.Now,
                ControlTime = ControlTime.Open,
                ApplicationUserId = HttpContext.User.Identity.GetUserId()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Open(Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                var evidence = db.Evidence.Find(calendar.EvidenceId);
                evidence.FinalDate = calendar.DateFinal;
                db.Entry(evidence).State = EntityState.Modified;

                db.Calendar.Add(calendar);
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = db.Evidence.Find(calendar.EvidenceId).ActivityId });
            }
            return View(calendar);
        }

        public ActionResult Closed(int calendarId)
        {
            var calendar = db.Calendar.Find(calendarId);
            calendar.ControlTime = ControlTime.Closed;
            calendar.DateFinal = DateTime.Now;
            calendar.TimeFinal = DateTime.Now;
            return View(calendar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Closed(Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                var evidence = db.Evidence.Find(calendar.EvidenceId);
                evidence.FinalDate = calendar.DateFinal;
                db.Entry(evidence).State = EntityState.Modified;
                db.Entry(calendar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = db.Evidence.Find(calendar.EvidenceId).ActivityId });
            }
            return View(calendar);
        }

        public ActionResult Cancel(int calendarId)
        {
            var calendar = db.Calendar.Find(calendarId);
            calendar.ControlTime = ControlTime.Cancel;
            calendar.Send = false;
            return View(calendar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Activities", new { id = calendar.Evidence.ActivityId });
            }
            return View(calendar);
        }

        public ActionResult Details(int Id, DateTime data)
        {
            var evidence = db.Calendar.Find(Id).Evidence;
            var calendars = evidence.Calendars.Where(c => c.DateInitial == data);
            evidence.Calendars = calendars.ToList();
            return View(evidence);
        }

        public ActionResult Send()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(c => c.Id == userId);

            var remetenteEmail = user.Email;
            var mail = new MailMessage();
            mail.To.Add("pmo@starsoft.com.br");
            mail.CC.Add("rsalves@starsoft.com.br");
            mail.Bcc.Add(remetenteEmail);
            mail.From = new MailAddress(remetenteEmail, user.Name, Encoding.UTF8);
            mail.Subject = "Agenda";
            mail.SubjectEncoding = Encoding.UTF8;
            StringBuilder body = new StringBuilder();
            if (DateTime.Now.Hour < 12)
            {
                body.AppendLine("<p><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>Bom dia !</span></p>");
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            {
                body.AppendLine("<p><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>Boa Tarde !</span></p>");
            }
            else if (DateTime.Now.Hour >= 18)
            {
                body.AppendLine("<p><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>Boa Noite !<p></span>");
            }

            body.AppendLine("<p><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>Poderia adicionar as seguintes atividades na agenda, por favor.</span></p>");
            body.AppendLine("<br/>");
            body.AppendLine("<p>");

            var porEmpresa = db.Calendar.Where(c => !c.Send && c.ApplicationUserId == userId).OrderBy(c => c.Evidence.Activity.CompanyId).GroupBy(c => c.Evidence.Activity.Company).ToList();

            foreach (var item in porEmpresa)
            {
                body.AppendLine("<b><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>Cliente:" + item.Key.CompanyName + "</span></b>");
                body.AppendLine("<br/>");

                foreach (var itemCalendar in item)
                {
                    itemCalendar.Send = true;
                }

                var porData = item.GroupBy(c => c.DateInitial);
                foreach (var item2 in porData)
                {
                    body.AppendLine("<p><span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'; text-indent: 10px;'><b>Data:</b> " + item2.Key.ToShortDateString() + "</span></p>");
                    var porEvidencia = item2.GroupBy(c => c.Evidence);
                    body.AppendLine("<span style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'; text-indent: 15px;'><b>Atividades</b></span>");
                    body.AppendLine("<ul style = 'font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A'>");
                    foreach (var item3 in porEvidencia)
                    {
                        body.AppendLine($"<li> {item3.Key.Activity.Title} - {item3.Key.Title} </li>");
                    }
                    body.AppendLine("</ul>");
                }
            }

            body.AppendLine("</p>");

            body.AppendLine("<p style='font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A;'><br/><br/>Obrigado!<br/><br/>" +
            user.Name + "<br/>" +
            "" + user.Funcao + "<br/>" +
            "Tel: 55 11 4133-2200 <br/>" +
            "<a style='font-family: Segoe UI Semilight; font-size: 14.5; color: #5B5D5A;' href = 'www.starsoft.com.br' target = '_blank' >" +
            "" +
            "www.starsoft.com.br </a><br/> <br/> <a " +
            "target ='_blank'><img height='77.8' width='126.9' src = 'http://www.star1crm.com.br/crmstar/custom/iv2017/Logo%20Color%20Completo%20PNG.png' alt = '' border ='0'/></a></p>");

            mail.Body = MvcHtmlString.Create(Server.HtmlDecode(body.ToString())).ToHtmlString();
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(remetenteEmail, user.SenhaEmail);
            client.Host = "smtp.starsoft.com.br";
            try
            {
                client.Send(mail);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }

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
