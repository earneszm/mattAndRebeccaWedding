using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MattAndRebeccaWedding.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send()
        {
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPserver"];                
                smtpClient.Port = 25;
                //smtpClient.Timeout = 10000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("admin@mattplusrebecca.com", "plastik5");

                //String bodyText = fvm.ContactNumber + "\n" + fvm.EmailAddress + "\n" + fvm.FirstName + " " + fvm.LastName + "\n" + fvm.Comments;

                MailMessage mailMessage = new MailMessage("noreply@test.com", "zach.earnest@gmail.com", "subject", "testing");
                //mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                smtpClient.Send(mailMessage);
                ViewBag.Message = "success";
            }
            catch (Exception ex) {
                ViewBag.Message = "failure";
            }
            return View("Index");
        }
    }
}