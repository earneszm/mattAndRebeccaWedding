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
            List<RSVPs> tempRsvps = new List<RSVPs>();

            if (guests.Count > 0)
            {
                List<Guests> tempList = new List<Guests>();
                int counter = 0;

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
                        tempRsvps.Add(new RSVPs(tempList));
                        tempList.Clear();
                        tempList.Add(g);
                    }
                }
                tempRsvps.Add(new RSVPs(tempList));

                rsvps = tempRsvps.OrderBy(x => x.guests.FirstOrDefault().lastName).ThenBy(x => x.guests.FirstOrDefault().firstName)
                    .ToList();

                for (int i = 0; i < rsvps.Count; i++)
                {
                    rsvps[i].DisplayNum = i + 1;
                }
            }
        }
    }
}