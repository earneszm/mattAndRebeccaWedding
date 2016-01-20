﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MattAndRebeccaWedding.Models;
using MattAndRebeccaWedding.Utilities;

namespace MattAndRebeccaWedding.Controllers
{
    public class AddGuestController : Controller
    {
        // GET: AddGuest
        public ActionResult AddGuest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddGuest(Guest guest)
        {
            bool shouldKeepGenerating = true;
            string newID = rsvpIdGenerator.GetNewRsvpId();
            while (shouldKeepGenerating)
            {
                if (DAL.CountStatement(SqlStatements.CheckIfRsvpIdExistsSQL(), SqlStatements.CheckIfRsvpIdExistsParameters(newID)) == 0)
                {
                    shouldKeepGenerating = false;
                }
                else
                {
                    newID = rsvpIdGenerator.GetNewRsvpId();
                }
            }
            guest.RsvpId = newID;
            DAL.InsertGuest(SqlStatements.InsertNewGuestSQL(), SqlStatements.InsertOrUpdateGuestParameters(guest));

            return RedirectToAction("RSVP", "RSVP");
        }
    }
}