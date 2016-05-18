using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MattAndRebeccaWedding.Models
{
    public class RSVPs
    {
        public Guid RsvpID { get; set; }
        public int DisplayNum { get; set; }
        public List<Guests> guests { get; set; }
        public bool hasRSVPed { get; set; }
        public bool isAttending { get; set; }

        [Range(1,99, ErrorMessage = "You must enter a correct number of guests.")]
        public int NumPeopleAttending { get; set; }
        public string NamesOfPeopleAttending { get; set; }
        public string rsvpComment { get; set; }

        public string IsAttendingString {
            get
            {
                if (hasRSVPed == false)
                    return "--";
                else if (isAttending == true)
                    return NumPeopleAttending + (NumPeopleAttending > 1 ? " guests" : " guest");
                else
                    return "Not Attending";
            }
        }

        public string HasRSVPedString
        {
            get
            {
                if (hasRSVPed == false)
                    return "No";
                else
                    return "Yes";
            }
        }

        public RSVPs()
        {

        }

        public RSVPs(List<Guests> g)
        {
            guests = new List<Guests>();

            if(g.Count > 0)
            {
                RsvpID = g.FirstOrDefault().rsvpID;
                hasRSVPed = g.FirstOrDefault().hasRSVPed;
                isAttending = g.FirstOrDefault().isAttending;
                NumPeopleAttending = g.FirstOrDefault().NumPeopleAttending;
                NamesOfPeopleAttending = g.FirstOrDefault().NamesOfPeopleAttending;
                rsvpComment = g.FirstOrDefault().rsvpComment;
            }

            foreach(var guest in g)
            {
                guests.Add(guest);
            }
        }

        public void SetUpForInitialRSVP()
        {
            NumPeopleAttending = guests.Count();
            isAttending = true;
        }
    }
}