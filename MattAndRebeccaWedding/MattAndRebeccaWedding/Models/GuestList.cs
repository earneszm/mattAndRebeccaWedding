using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MattAndRebeccaWedding.Models
{
    public class GuestList
    {
        public List<RSVPs> rsvps { get; set; }

        public GuestList(List<Guests> guests)
        {
            rsvps = new List<RSVPs>();

            if (guests.Count > 0)
            {
                List<Guests> tempList = new List<Guests>();
                int counter = 0;
                int guestNum = 0;

                tempList.Add(guests.FirstOrDefault());

                foreach (var g in guests)
                {
                    counter++;                    

                    if (counter == 1)
                        continue;

                    if (g.rsvpID == tempList.LastOrDefault().rsvpID)
                    {
                        tempList.Add(g);
                        continue;
                    }
                    else
                    {
                        guestNum++;
                        rsvps.Add(new RSVPs(tempList, guestNum));
                        tempList.Clear();
                        tempList.Add(g);
                    }
                }
                guestNum++;
                rsvps.Add(new RSVPs(tempList, guestNum));
            }
        }
    }
}