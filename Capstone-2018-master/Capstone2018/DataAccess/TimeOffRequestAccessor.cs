using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public class TimeOffRequestAccessor : ITimeOffRequestAccessor
    {
        /// <summary>
        /// Weston Olund
        /// Created on 2018/01/26
        /// 
        /// Method to use stored procedure to get list of time off requests from database
        /// </summary>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  neee to add the delete time off request implement
        public List<TimeOffRequest> RetrieveTimeOffRequestList()
        {
            List<TimeOffRequest> timeOffRequestList = new List<TimeOffRequest>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_timeoff_list";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var timeOffRequest = new TimeOffRequest();

                        timeOffRequest.TimeOffID = reader.GetInt32(0);
                        timeOffRequest.EmployeeID = reader.GetInt32(1);
                        timeOffRequest.StartTime = reader.GetDateTime(2);
                        timeOffRequest.EndTime = reader.GetDateTime(3);
                        timeOffRequest.Approved = reader.GetBoolean(4);
                        timeOffRequest.Active = reader.GetBoolean(5);

                        timeOffRequestList.Add(timeOffRequest);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving your data", ex);
            }
            finally
            {
                conn.Close();
            }
            return timeOffRequestList;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to use stored procedure to add a new time off request from database
        /// </summary>
        /// <returns></returns>
        public int CreateTimeOffRequest(TimeOffRequest timeOffRequest)
        {
            int newTimeOffRequestID = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_timeoff";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", timeOffRequest.EmployeeID);
            cmd.Parameters.AddWithValue("@StartTime", timeOffRequest.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", timeOffRequest.EndTime);
            cmd.Parameters.AddWithValue("@Active", timeOffRequest.Active);

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newTimeOffRequestID = (int)id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem adding your time off request.", ex);
            }
            finally
            {
                conn.Close();
            }
            return newTimeOffRequestID;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/01
        /// 
        /// Method to edit a current time off
        /// </summary>
        /// <param name="oldTimeOff"> The current time off object</param>
        /// <param name="newTimeOff"> The updated time off object</param>
        /// <returns>rows affected: 1 if updated 0 if not</returns>
        public int EditTimeOff(TimeOffRequest oldTimeOff, TimeOffRequest newTimeOff)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_timeoff_by_timeoffid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TimeOffID", SqlDbType.Int);
            cmd.Parameters.Add("@NewStartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewEndTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewApproved", SqlDbType.Bit);
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);
            cmd.Parameters.Add("@OldStartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldEndTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldApproved", SqlDbType.Bit);
            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);

            cmd.Parameters["@TimeOffID"].Value = oldTimeOff.TimeOffID;
            cmd.Parameters["@NewStartTime"].Value = newTimeOff.StartTime;
            cmd.Parameters["@NewEndTime"].Value = newTimeOff.EndTime;
            cmd.Parameters["@NewApproved"].Value = newTimeOff.Approved;
            cmd.Parameters["@NewActive"].Value = newTimeOff.Active;
            cmd.Parameters["@OldStartTime"].Value = oldTimeOff.StartTime;
            cmd.Parameters["@OldEndTime"].Value = oldTimeOff.EndTime;
            cmd.Parameters["@OldApproved"].Value = oldTimeOff.Approved;
            cmd.Parameters["@OldActive"].Value = oldTimeOff.Active;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Time Off update failed.");
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// John Miller
        /// Created: 2018/03/02
        /// 
        /// Deactivates the TimeOffRequest record with the given ID in the SqlServer database
        /// </summary>
        /// <param name="timeOffID">The ID of the TimeOffRequest to be deactivated</param>
        /// <returns>The number of rows affected</returns>
        public bool DeactivateTimeOffRequestByID(int timeOffID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_timeoffrequest_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TimeOffID", timeOffID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
