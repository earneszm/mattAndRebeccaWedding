using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MattAndRebeccaWedding.Models;
using System.Data.SqlClient;
using Dapper;

namespace MattAndRebeccaWedding
{
    public class DAL
    {
        public static DataTable SelectStatement(string sqlQuery, DataTable dtParameters = null)
        {
            DataTable dtSelectedInformation = new DataTable();

            try
            {

                using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
                {

                    mycon.Open();

                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dataReader;
                    cmd.Connection = mycon;
                    cmd.CommandText = sqlQuery;

                    using (cmd)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmd.Parameters.Add(new SqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        using (dataReader = cmd.ExecuteReader())
                        {
                            dtSelectedInformation.Load(dataReader);
                        }

                        // Clear parameters from command
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                //catch an log the error                
                //if (sqlException.Number == 0 || sqlException.Number == 1042)
                //Log.LogWriter(sqlException.ToString(), "MySQL Error", "2");
                throw sqlException;
            }
            catch (Exception e)
            {
                //some non my sql related exception, log it 
                //Log.LogWriter(e.ToString(), "General SQL Error", "2");
                throw e;
            }
            finally
            {

            }

            return (dtSelectedInformation);
        }

        public static List<Guests> GetAllGuests()
        {
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
            {
                return mycon.Query<Guests>
                (@"select guestID, r.rsvpID, g.firstName, g.LastName, r.hasRSVPed, r.isAttending, r.numPeopleAttending, r.namesOfPeopleAttending, r.rsvpComment from tblguests g inner join tblrsvp r on g.rsvpID = r.rsvpID 
                    order by r.rsvpID, g.lastName, g.firstName").ToList();
            }
        }

        public static List<Guests> GetRSVPByID(string rsvpID)
        {
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
            {
                return mycon.Query<Guests>
                (@"select guestID, r.rsvpID, g.firstName, g.LastName, r.hasRSVPed, r.isAttending, r.numPeopleAttending, r.namesOfPeopleAttending, r.rsvpComment from tblguests g 
inner join tblrsvp r on g.rsvpID = r.rsvpID 
where r.rsvpID = @rsvpID
                    order by r.rsvpID, g.lastName, g.firstName",
                    new { rsvpID = rsvpID }).ToList();
            }
        }

        public static List<searchViewModel> SearchRSVPs(string fName, string lName)
        {
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
            {
                return mycon.Query<searchViewModel>
                (@"select * from tblguests where  firstName like '%' + @firstName + '%' and lastName like '%' + @lastName +'%'",
                    new { firstName = fName ?? "", lastName = lName ?? "" }).ToList();
            }
        }

        public static int UpdateRSVP(RSVPs rsvp)
        {
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
            {
                return mycon.Execute
                (@"update tblrsvp set hasRSVPed = @hasRSVPed, 
                    isAttending = @isAttending, 
                    numPeopleAttending = @numPeopleAttending, 
                    namesOfPeopleAttending = @namesOfPeopleAttending,
                     rsvpComment = @rsvpComment
                    where rsvpID = @rsvpID",
                    new { rsvpID = rsvp.RsvpID, hasRSVPed = rsvp.hasRSVPed, isAttending = rsvp.isAttending, numPeopleAttending = rsvp.NumPeopleAttending,
                    namesOfPeopleAttending = rsvp.NamesOfPeopleAttending, rsvpComment = rsvp.rsvpComment});
            }
        }

        public static DataTable SelectRSVPs()
        {
            DataTable dtSelectedInformation = new DataTable();

            try
            {

                using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
                {

                    mycon.Open();

                    SqlCommand cmdMySQL = new SqlCommand();
                    SqlDataReader dataReader;
                    cmdMySQL.Connection = mycon;
                    cmdMySQL.CommandText = @"select guestID, r.rsvpID, g.firstName, g.LastName, r.hasRSVPed, r.isAttending, r.numPeopleAttending, r.rsvpComment from tblguests g inner join tblrsvp r on g.rsvpID = r.rsvpID 
order by r.rsvpID, g.lastName, g.firstName";

                    using (cmdMySQL)
                    {

                        using (dataReader = cmdMySQL.ExecuteReader())
                        {
                            dtSelectedInformation.Load(dataReader);
                        }

                        // Clear parameters from command
                        cmdMySQL.Parameters.Clear();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                //catch an log the error                
                //if (sqlException.Number == 0 || sqlException.Number == 1042)
                //Log.LogWriter(sqlException.ToString(), "MySQL Error", "2");
                throw sqlException;
            }
            catch (Exception e)
            {
                //some non my sql related exception, log it 
                //Log.LogWriter(e.ToString(), "General SQL Error", "2");
                throw e;
            }
            finally
            {

            }

            return (dtSelectedInformation);
        }

        public static int CountStatement(string sqlQuery, DataTable dtParameters = null)
        {
            int rowCount = 0;

            try
            {

                using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
                {

                    mycon.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mycon;
                    cmd.CommandText = sqlQuery;

                    using (cmd)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmd.Parameters.Add(new SqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Clear parameters from command
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                //catch an log the error
                
            }
            finally
            {

            }

            return (rowCount);
        }

        public static int InsertOrUpdate(string sqlQuery, DataTable dtParameters = null)
        {
            DataTable dtSelectedInformation = new DataTable();
            int rowsAffected = 0;

            try
            {

                using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString))
                {

                    mycon.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = mycon;
                    cmd.CommandText = sqlQuery;

                    using (cmd)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmd.Parameters.Add(new SqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        rowsAffected = cmd.ExecuteNonQuery();

                        // Clear parameters from command
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (SqlException sqlException)
            {
                //catch an log the error                
                //if (sqlException.Number == 0 || sqlException.Number == 1042)
                //Log.LogWriter(sqlException.ToString(), "MySQL Error", "2");
                throw sqlException;
            }
            catch (Exception e)
            {
                //some non my sql related exception, log it 
                //Log.LogWriter(e.ToString(), "General SQL Error", "2");
                throw e;
            }
            finally
            {

            }

            return (rowsAffected);
        }
    }
}