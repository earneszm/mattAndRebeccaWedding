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
        public static string SelectGuestByGuestIDSQL()
        {
            return @"SELECT * FROM Guests WHERE GuestID = @guestID";
        }
        public static DataTable SelectGuestByGuestIDParameters(int guestID)
        {
            DataTable dtParameters = new DataTable();
            dtParameters.Columns.Add("Parameter");
            dtParameters.Columns.Add("Value");
            dtParameters.Rows.Add("@guestID", guestID);
            //dtParameters.Rows.Add("@password", Password);

            return dtParameters;
        }

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
VALUES (@Name, @Email, @emailComment, @rsvpId, @hasRSVPed, @isAttending);";
        }

        public static string UpdateGuestSQL()
        {
            return @"UPDATE Guests Set
       Name = @Name,
       Email = @Email,
       CommentForEmailSent = @emailComment,       
       HasRSVPed = @hasRSVPed,
       IsAttending = @isAttending,
       CommentFromRSVP = @rsvpComment,
       NumPeopleAllowed = @numPeopleAllowed,
       NumPeopleAttending = @numPeopleAttending
       Where RSVP_ID = @rsvpId;";
        }
        public static DataTable InsertOrUpdateGuestParameters(Guest guest)
        {
            DataTable dtParameters = new DataTable();
            //dtParameters.Columns.Add("Parameter");
            //dtParameters.Columns.Add("Value");
            //dtParameters.Rows.Add("@Name", guest.Name);
            //dtParameters.Rows.Add("@Email", guest.Email);
            //dtParameters.Rows.Add("@emailComment", guest.CommentForEmailSent);
            //dtParameters.Rows.Add("@rsvpComment", guest.CommentFromRSVP);
            //dtParameters.Rows.Add("@rsvpId", guest.RsvpId);
            //dtParameters.Rows.Add("@numPeopleAllowed", guest.NumPeopleAllowed);
            //dtParameters.Rows.Add("@numPeopleAttending", guest.NumPeopleAttending);
            //dtParameters.Rows.Add("@hasRSVPed", guest.HasRSVPed ? 1 : 0);
            //dtParameters.Rows.Add("@isAttending", guest.IsAttending ? 1 : 0);

            return dtParameters;
        }
    }
}