using MattAndRebeccaWedding.Models;
using MattAndRebeccaWedding.Utilities.CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace MattAndRebeccaWedding.Controllers
{
    [Authorize]
    public class EditGuestController : BaseController
    {
        // GET: EditGuest
        public ActionResult EditGuest(int guestID)
        {
            //using (DataTable dtGuest = DAL.SelectStatement(SqlStatements.SelectGuestByGuestIDSQL(), SqlStatements.SelectGuestByGuestIDParameters(guestID)))
            //{
            //    if (dtGuest.Rows.Count > 0)
            //    {
            //        Guest guest = new Guest(dtGuest.Rows[0]);
            //        return View(guest);
            //    }
            //    else
            //    {
            //        // guest not found in database
            //        ViewBag.FailureMessage = "Guest Not Found. Contact Your Good Buddy Zach.";
                    return View();
            //    }
            //}
        }

        [HttpPost]
        public ActionResult EditGuest(Guest guest)
        {
            if (DAL.InsertOrUpdate(SqlStatements.UpdateGuestSQL(), SqlStatements.InsertOrUpdateGuestParameters(guest)) == 1)
            {
                ViewBag.SuccessMessage = "Guest Successfully Updated!";
            }
            else
            {
                ViewBag.FailureMessage = "Update Failed! Contact Zach.";
            }

            return View(guest);
        }
    }
}