﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MattAndRebeccaWedding.Models
{
    public class Guests
    {
        public Guid guestID { get; set; }
        public Guid rsvpID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool hasRSVPed { get; set; }
        public bool isAttending { get; set; }
        public int NumPeopleAttending { get; set; }
        public string NamesOfPeopleAttending { get; set; }
        public string rsvpComment { get; set; }
        public string fullName { get { return lastName + ", " + firstName; } }


        //public List<Guest> guestList;

        //public int GuestsInvited { get; set; }
        //public int GuestsAttending { get; set; }
        //public int RSVPsTotal { get; set; }
        //public int RSVPsReceived { get; set; }


        //public Guests(DataTable dtGuests)
        //{
        //    guestList = new List<Guest>();

        //    foreach (DataRow dr in dtGuests.Rows)
        //    {                
        //        guestList.Add(new Guest(dr));
        //        GuestsInvited += guestList[guestList.Count - 1].NumPeopleAllowed;
        //        GuestsAttending += guestList[guestList.Count - 1].NumPeopleAttending;

        //        RSVPsTotal++;
        //        if (guestList[guestList.Count - 1].HasRSVPed)
        //        {
        //            RSVPsReceived++;
        //        }                
        //    }
        //}
    }
}