using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Etutor.DAL;
using Etutor.Models;
using EASendMail;

namespace Etutor.Controllers
{
    [Authorize]
    public class AssignController : Controller
    {
        private EtutorContext db = new EtutorContext();

        // GET: Assigns
        public ActionResult Index()
        {
            var assigns = db.Assigns.Include(a => a.Student);
            return View(assigns.ToList());
        }

        // GET: Assigns/CreateMultiple
        public ActionResult CreateMultiple()
        {
            ViewBag.Tutors = db.Tutors.ToList();
            ViewBag.Staff = db.Staffs.ToList();
            ViewBag.Students = db.Students.ToList();
            return View();
        }

        // POST: Assigns/CreateMultiple
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMultiple([Bind(Include = "Id")] List<int> stdID, int tutorID, int? staffID)
        {
            for (int i = 0; i < stdID.Count; i++)
            {
                Assign assign = new Assign();
                if (ModelState.IsValid)
                {
                    int index = stdID[i];
                    var student = db.Students.Where(m => m.Id == index).SingleOrDefault();
                    var tutor = db.Tutors.Where(m => m.Id == tutorID).SingleOrDefault();
                    var staff = db.Staffs.Where(m => m.Id == staffID).SingleOrDefault();
                    assign.Id = student.Id;
                    assign.Student = student;
                    assign.Tutor = tutor;
                    assign.Staff = staff;
                    db.Assigns.Add(assign);
                    db.SaveChanges();
                    Email(student.Email, tutor.Email, "Assign Notification", "You are assigned to new course");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Assigns/Edit/id
        public ActionResult Edit(int? id)
        {
            Assign assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = db.Tutors.ToList();
            return View(assign);
        }

        // POST: Assigns/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] int? stdID, int tutorID, int? staffID)
        {
            Assign assign = db.Assigns.Where(m => m.Id == stdID).SingleOrDefault();
            if (ModelState.IsValid)
            {
                var student = db.Students.Where(m => m.Id == stdID).SingleOrDefault();
                var tutor = db.Tutors.Where(m => m.Id == tutorID).SingleOrDefault();
                var staff = db.Staffs.Where(m => m.Id == tutorID).SingleOrDefault();
                assign.Id = student.Id;
                assign.Student = student;
                assign.Tutor = tutor;
                assign.Staff = staff;
                db.SaveChanges();
                Email(student.Email, tutor.Email, "Change Tutor", "There is an update of tutor");
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Students, "Name", "Phone", "Email", assign.Id);
            return View(assign);
        }

        // GET: Assigns/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assign assign = db.Assigns.Find(id);
            if (assign == null)
            {
                return HttpNotFound();
            }
            return View(assign);
        }

        // POST: Assigns/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Assign assign = db.Assigns.Find(id);
            var docList = db.Documents.Where(m => m.Assign.Id == id).ToList();
            var mesList = db.Messages.Where(m => m.Assign.Id == id).ToList();
            foreach (var doc in docList)
            {
                DeleteDoc(doc.Id);
            }
            foreach (var mes in mesList)
            {
                DeleteMes(mes.Id);
            }
            db.Assigns.Remove(assign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void DeleteDoc(int id)
        {
            var doc = db.Documents.Where(m => m.Id == id).FirstOrDefault();
            db.Documents.Remove(doc);
            db.SaveChanges();
        }

        public void DeleteMes(int id)
        {
            var mes = db.Messages.Where(m => m.Id == id).FirstOrDefault();
            db.Messages.Remove(mes);
            db.SaveChanges();
        }

        public void Email(string To, string Cc, string Subject, string TextBody)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Your gmail email address
                oMail.From = "etutorlearning99@gmail.com";

                // Set recipient email address
                oMail.To = To;
                oMail.Cc = Cc;

                // Set email subject
                oMail.Subject = Subject;

                // Set email bodytio
                oMail.TextBody = TextBody;

                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                // Gmail user authentication
                // For example: your email is "gmailid@gmail.com", then the user should be the same
                oServer.User = "etutorlearning99@gmail.com";
                oServer.Password = "P@ssw0rd99";

                // If you want to use direct SSL 465 port,
                // please add this line, otherwise TLS will be used.
                // oServer.Port = 465;

                // set 587 TLS port;
                oServer.Port = 587;

                // detect SSL/TLS automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over SSL ...");

                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
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