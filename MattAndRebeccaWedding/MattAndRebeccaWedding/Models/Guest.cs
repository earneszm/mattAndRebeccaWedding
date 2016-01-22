using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MattAndRebeccaWedding.Models
{
    public class Guest
    {
        [Display(Name="Guest #:")]
        public int GuestID { get; set; }

        [Display(Name = "Rsvp ID:")]
        public string RsvpId { get; set; }
        

        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Display(Name = "Attending?")]
        public bool IsAttending { get; set; }

        public int DisplayNum { get; set; }
        public int NumPeopleAllowed { get; set; }
        public int NumPeopleAttending { get; set; }

        public string IsAttendingText
        {
            get
            {
                if (HasRSVPed == false)
                    return "--";
                else if (IsAttending == true)
                    return NumPeopleAttending + "/" + NumPeopleAllowed + " attending";
                else
                    return "Not Attending";
            }
            set
            {
            }
        }

        [Display(Name = "RSVPed?")]
        public bool HasRSVPed { get; set; }

        public string HasRSVPedText {
            get
            {
                if (HasRSVPed == true)
                    return "Received";
                else
                    return "Not Yet";
            }
            set
            {
            }
        }

        [Display(Name = "Personal Email Comment")]
        public string CommentForEmailSent { get; set; }
        public string CommentFromRSVP { get; set; }

        public Guest()
        {

        }

        public Guest(DataRow dr)
        {
            GuestID = int.Parse(dr["GuestID"].ToString());
            Name = dr["Name"].ToString();
            Email = dr["Email"].ToString();
            IsAttending = dr["IsAttending"].ToString() == "1" ? true : false;
            HasRSVPed = dr["HasRSVPed"].ToString() == "1" ? true : false;
            CommentForEmailSent = dr["CommentForEmailSent"].ToString();
            CommentFromRSVP = dr["CommentFromRSVP"].ToString();
            RsvpId = dr["RSVP_ID"].ToString();
            NumPeopleAllowed = int.Parse(dr["NumPeopleAllowed"].ToString());
            NumPeopleAttending = int.Parse(dr["NumPeopleAttending"].ToString());
        }
    }
}