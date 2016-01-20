using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using TestingAppServices.Models;

namespace TestingAppServices
{
    public class DAL
    {
        public static DataTable SelectStatement(string sqlQuery, DataTable dtParameters = null)
        {
            DataTable dtSelectedInformation = new DataTable();

            try
            {

                using (MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                {

                    mycon.Open();

                    MySqlCommand cmdMySQL = new MySqlCommand();
                    MySqlDataReader dataReader;
                    cmdMySQL.Connection = mycon;
                    cmdMySQL.CommandText = sqlQuery;

                    using (cmdMySQL)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmdMySQL.Parameters.Add(new MySqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        using (dataReader = cmdMySQL.ExecuteReader())
                        {
                            dtSelectedInformation.Load(dataReader);
                        }

                        // Clear parameters from command
                        cmdMySQL.Parameters.Clear();
                    }
                }
            }
            catch (MySqlException sqlException)
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

        public static DataTable SelectRSVPs()
        {
            DataTable dtSelectedInformation = new DataTable();

            try
            {

                using (MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                {

                    mycon.Open();

                    MySqlCommand cmdMySQL = new MySqlCommand();
                    MySqlDataReader dataReader;
                    cmdMySQL.Connection = mycon;
                    cmdMySQL.CommandText = @"SELECT * FROM Guests;";

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
            catch (MySqlException sqlException)
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

                using (MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString))
                {

                    mycon.Open();

                    MySqlCommand cmdMySQL = new MySqlCommand();
                    cmdMySQL.Connection = mycon;
                    cmdMySQL.CommandText = sqlQuery;

                    using (cmdMySQL)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmdMySQL.Parameters.Add(new MySqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        rowCount = Convert.ToInt32(cmdMySQL.ExecuteScalar());

                        // Clear parameters from command
                        cmdMySQL.Parameters.Clear();
                    }
                }
            }
            catch (MySqlException sqlException)
            {
                //catch an log the error
                
            }
            finally
            {

            }

            return (rowCount);
        }

        public static int InsertGuest(string sqlQuery, DataTable dtParameters = null)
        {
            DataTable dtSelectedInformation = new DataTable();
            int rowsAffected = 0;

            try
            {

                using (MySqlConnection mycon = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
                {

                    mycon.Open();

                    MySqlCommand cmdMySQL = new MySqlCommand();
                    cmdMySQL.Connection = mycon;
                    cmdMySQL.CommandText = sqlQuery;

                    using (cmdMySQL)
                    {
                        if (dtParameters != null)
                        {
                            foreach (DataRow dr in dtParameters.Rows)
                            {
                                cmdMySQL.Parameters.Add(new MySqlParameter(dr[0].ToString(), dr[1].ToString()));
                            }
                        }

                        rowsAffected = cmdMySQL.ExecuteNonQuery();

                        // Clear parameters from command
                        cmdMySQL.Parameters.Clear();
                    }
                }
            }
            catch (MySqlException sqlException)
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