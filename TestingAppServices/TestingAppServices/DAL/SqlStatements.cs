using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MattAndRebeccaWedding.Models;

namespace MattAndRebeccaWedding
{
    public class SqlStatements
    {
        public static string CheckIfRsvpIdExistsSQL()
        {
            return @"SELECT COUNT(*) FROM Guests WHERE RSVP_ID = @rsvpId";
        }
        public static DataTable CheckIfRsvpIdExistsParameters(string rsvpId)
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("Parameter");
            dtParameters.Columns.Add("Value");
            dtParameters.Rows.Add("@rsvpId", rsvpId.ToUpper());
            //dtParameters.Rows.Add("@password", Password);

            return dtParameters;
        }

        public static string CheckIfRsvpIdHasAlreadyRespondedSQL()
        {
            return @"SELECT * FROM Guests WHERE RSVP_ID = @rsvpId";
        }
        public static DataTable CheckIfRsvpIdHasAlreadyRespondedParameters(string rsvpId)
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("Parameter");
            dtParameters.Columns.Add("Value");
            dtParameters.Rows.Add("@rsvpId", rsvpId.ToUpper());
            //dtParameters.Rows.Add("@password", Password);

            return dtParameters;
        }

        public static string InsertNewGuestSQL()
        {
            return @"INSERT INTO Guests 
       (Name, Email, CommentForEmailSent, RSVP_ID, HasRSVPed, IsAttending) 
VALUES (@Name, @Email, @Comment, @rsvpId, @hasRSVPed, @isAttending);";
        }

        public static string UpdateNewGuestSQL()
        {
            return @"UPDATE Guests Set
       Name = @Name,
       Email = @Email,
       CommentForEmailSent = @Comment,       
       HasRSVPed = @hasRSVPed,
       IsAttending = @isAttending
       Where RSVP_ID = @rsvpId;";
        }
        public static DataTable InsertOrUpdateGuestParameters(Guest guest)
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("Parameter");
            dtParameters.Columns.Add("Value");
            dtParameters.Rows.Add("@Name", guest.Name);
            dtParameters.Rows.Add("@Email", guest.Email);
            dtParameters.Rows.Add("@Comment", guest.CommentForEmailSent);
            dtParameters.Rows.Add("@rsvpId", guest.RsvpId);
            dtParameters.Rows.Add("@hasRSVPed", guest.HasRSVPed ? 1 : 0);
            dtParameters.Rows.Add("@isAttending", guest.IsAttending ? 1 : 0);

            return dtParameters;
        }
    }
}