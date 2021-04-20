using Etutor.DAL;
using Etutor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Etutor.Controllers
{
    public class ChatController : Controller
    {
        private EtutorContext db = new EtutorContext();
        // GET: Chat
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["S_ID"]);
            var found = db.Messages.Where(m => m.Assign.Id == id).ToList();
            return View(found);
        }

        //public ActionResult Chat(int id, int From, string Text)
        //{
        //    Assign assign = db.Assigns.Find(id);
        //    Message mes = new Message();
        //    mes.Assign = assign;
        //    mes.BodyText = Text;
        //    mes.From = From;
        //    mes.Time = DateTime.Now;
        //    db.Messages.Add(mes);
        //    db.SaveChanges();
        //    return View("Index",new { id=id});
        //}

        public ActionResult Chat()
        {
            var name = Session["S_ID"];
            return View();
        }

        public ActionResult SendMessage(Message data)
        {
            var idString = Session["S_ID"].ToString();
            int id = int.Parse(idString);
            var account = db.Accounts.FirstOrDefault(h => h.Id == id);

            Assign myAssign = null;
             switch (account.Role)
            {
                case 2:  myAssign = db.Assigns.FirstOrDefault(h => h.Student.Id == id);
                    break;
                case 3:  myAssign = db.Assigns.FirstOrDefault(h => h.Student.Id == id);
                    break;
                default:
                    break;
            }
            Message newMessage = new Message();
            newMessage.BodyText = data.BodyText;
            newMessage.Time = DateTime.Now;
            newMessage.From = id;
            newMessage.Assign = db.Assigns.Where(m => m.Id == id).SingleOrDefault();
            db.Messages.Add(newMessage);
            db.SaveChanges();
            return RedirectToAction("Index","Chat", new { id = myAssign.Id });
        }


    }
}
