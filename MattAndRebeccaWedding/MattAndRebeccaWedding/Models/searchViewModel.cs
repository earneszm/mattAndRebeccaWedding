using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MattAndRebeccaWedding.Models
{
    public class searchViewModel
    {
        [Display(Name ="First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        public Guid rsvpID { get; set; }
        public Guid guestID { get; set; }
    }
}