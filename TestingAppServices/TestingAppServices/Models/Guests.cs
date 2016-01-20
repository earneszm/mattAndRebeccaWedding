using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TestingAppServices.Models
{
    public class Guests
    {
        public List<Guest> guestList;


        public Guests(DataTable dtGuests)
        {
            guestList = new List<Guest>();

            foreach(DataRow dr in dtGuests.Rows)
            {
                guestList.Add(new Guest(dr));
            }
        }
    }
}