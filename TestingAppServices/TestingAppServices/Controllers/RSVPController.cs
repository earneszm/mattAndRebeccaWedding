using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingAppServices.Models;
using TestingAppServices.Utilities.CustomAuth;

namespace TestingAppServices.Controllers
{
    public class RSVPController : BaseController
    {
        // GET: RSVP
        public ActionResult RSVP()
        {
            if (User != null)
            {
                ViewBag.UserName = User.UserName;

                Guests guests;

                using (DataTable dtRSVPs = DAL.SelectRSVPs())
                {
                    guests = new Guests(dtRSVPs);
                }

                return View(guests);
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
            Guest newGuest;   
            // check if guest has already rsvped, if not, allow post and redirect to success page
            using (DataTable dtGuest = DAL.SelectStatement(SqlStatements.CheckIfRsvpIdHasAlreadyRespondedSQL(), SqlStatements.CheckIfRsvpIdHasAlreadyRespondedParameters(guest.RsvpId)))
            {
                if(dtGuest.Rows.Count > 0)
                {
                    newGuest = new Guest(dtGuest.Rows[0]);

                    if(newGuest.HasRSVPed == true)
                    {
                        // guest has already rsvped, tell them so
                        ViewBag.FailureMessage = "This Code Has Already RSVPed. If you need to change your RSVP, contact us.";
                    }
                    else
                    {
                        // update guest record in database
                        newGuest.HasRSVPed = true;
                        newGuest.IsAttending = guest.IsAttending;
                        newGuest.CommentFromRSVP = guest.CommentFromRSVP;
                        DAL.InsertGuest(SqlStatements.UpdateNewGuestSQL(), SqlStatements.InsertOrUpdateGuestParameters(newGuest));
                        ViewBag.SuccessMessage = "Thank you for RSVPing! Your choice has been saved and you will not be able to change it without contacting us.";
                    }
                }
                else
                {
                    // rsvp id does not exist in the database, alert the user
                    ViewBag.FailureMessage = "RSVP Code Not Found.";
                }
            }
            // if already rsvped, throw an error saying so

            // return RedirectToAction("RSVP", "RSVP");
            return View();
        }
    }
}