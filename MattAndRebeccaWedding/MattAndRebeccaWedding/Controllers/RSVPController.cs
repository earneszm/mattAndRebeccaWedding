using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MattAndRebeccaWedding.Models;
using MattAndRebeccaWedding.Utilities.CustomAuth;
using System.Net.Mail;
using System.Net;

namespace MattAndRebeccaWedding.Controllers
{
    public class RSVPController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: RSVP
        public ActionResult RSVP()
        {
            if (User != null)
            {
                ViewBag.UserName = User.UserName;

                List<Guests> guests = DAL.GetAllGuests();

                GuestList guestList = new GuestList(guests);                

                return View(guestList);
            }
            else
            {
                ViewBag.ErrorMessage = "You do not have access to that page.";
                return View("Error");
            }
            
        }

        public ActionResult GuestRSVP()
        {
            Guest guest = new Guest();

            return View(guest);
        }

        [HttpPost]
        public ActionResult GuestRSVP(Guest guest)
        {
            //Guest newGuest;   
            //// check if guest has already rsvped, if not, allow post and redirect to success page
            //using (DataTable dtGuest = DAL.SelectStatement(SqlStatements.CheckIfRsvpIdHasAlreadyRespondedSQL(), SqlStatements.CheckIfRsvpIdHasAlreadyRespondedParameters(guest.RsvpId)))
            //{
            //    if(dtGuest.Rows.Count > 0)
            //    {
            //        newGuest = new Guest(dtGuest.Rows[0]);

            //        if(newGuest.HasRSVPed == true)
            //        {
            //            // guest has already rsvped, tell them so
            //            ViewBag.FailureMessage = "This Code Has Already RSVPed. If you need to change your RSVP, contact us.";
            //        }
            //        else
            //        {
            //            // update guest record in database
            //            newGuest.HasRSVPed = true;
            //            newGuest.IsAttending = guest.IsAttending;
            //            newGuest.CommentFromRSVP = guest.CommentFromRSVP;
            //            DAL.InsertOrUpdate(SqlStatements.UpdateGuestSQL(), SqlStatements.InsertOrUpdateGuestParameters(newGuest));
            //            ViewBag.SuccessMessage = "Thank you for RSVPing! Your choice has been saved and you will not be able to change it without contacting us.";
            //        }
            //    }
            //    else
            //    {
            //        // rsvp id does not exist in the database, alert the user
            //        ViewBag.FailureMessage = "RSVP Code Not Found.";
            //    }
            //}
            // if already rsvped, throw an error saying so

            // return RedirectToAction("RSVP", "RSVP");
            return View();
        }

        public PartialViewResult SearchRSVPs (string firstName, string lastName)
        {
            firstName = firstName.Trim();
            lastName = lastName.Trim();
            List<searchViewModel> searchResults = DAL.SearchRSVPs(firstName, lastName);

            return PartialView("_rsvpSearch", searchResults);
        }

        public ActionResult GuestRespond (string rsvpID)
        {
            List<Guests> guestList = DAL.GetRSVPByID(rsvpID);

            if (guestList.FirstOrDefault().hasRSVPed)
                return View("AlreadyRSVPed");

            RSVPs guestsOnThisRSVP = new RSVPs(guestList);
            guestsOnThisRSVP.SetUpForInitialRSVP();

            return View(guestsOnThisRSVP);
        }

        [HttpPost]
        public ActionResult GuestRespond(RSVPs rsvps)
        {
            rsvps.hasRSVPed = true;

            if (DAL.UpdateRSVP(rsvps) == 1)
            {
                SendEmail(rsvps);
                return View("RSVPSuccess");
            }
            else
                throw new Exception();
        }

        private void SendEmail(RSVPs rsvps)
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

                List<Guests> guestList = DAL.GetGuestsByID(rsvps.RsvpID.ToString());

                string fullGuestNames = "";
                foreach(var g in guestList)
                {
                    fullGuestNames += g.firstName + " " + g.lastName + " ";
                }

                string guestRSVPing = guestList.FirstOrDefault().firstName + " " + guestList.FirstOrDefault().lastName;
                
                string subject = guestRSVPing + " RSVP!";
                string body = "Names: " + fullGuestNames + "\n" +
                              "rsvpID: " + rsvps.RsvpID + "\n" +
                              "isAttending: " + (rsvps.isAttending ? "Yes" : "No") + "\n" +
                              "numPeopleAttending: " + rsvps.NumPeopleAttending + "\n" +
                              "namesOfPeopleAttending: " + rsvps.NamesOfPeopleAttending + "\n" +
                              "rsvpComment: " + rsvps.rsvpComment;

                MailMessage mailMessage = new MailMessage("noreply@mattplusrebecca.com", "zach.earnest@gmail.com", subject, body);
                //mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
            }
        }
    }
}