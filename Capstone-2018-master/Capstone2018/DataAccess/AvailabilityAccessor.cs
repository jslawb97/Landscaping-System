using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;
using Microsoft.SqlServer.Server;

namespace DataAccess
{
    public class AvailabilityAccessor : IAvailabilityAccessor
    {

        /// <summary>
        /// Badis SAIDANI
        /// 2018/02/22
        /// 
        /// Method to deactivate an availability by ID
        /// </summary>
        /// <param name="AvailabilityID">The ID of the availability to be deactivated</param>
        /// <exception cref="SQLException">Deactivate fails</exception>
        /// <returns>Availabilities deactivated</returns>
        public int DeactivateAvailabilityByID(int AvailabilityID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_availability_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AvailabilityID", SqlDbType.Int);
            cmd.Parameters["@AvailabilityID"].Value = AvailabilityID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the availability", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        public int CreateAvailable(Availability availability)
        {
            throw new NotImplementedException();
        }

       

        public int EditAvailability(Availability oldAvailability, Availability newAvailability)
        {
            throw new NotImplementedException();
        }

        public Availability RetrieveAvailabilityByID(int availabilityID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Retrieves a list of Availability records based on a given employee's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Availability> RetrieveAvailabilityByEmployeeID(int id)
        {
            var availabilities = new List<Availability>();

            //connect
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_retrieve_availability_list_by_employee_id";

            //command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", id);


            try
            {
                //open the connection
                conn.Open();
                //execute the command
                var reader = cmd.ExecuteReader();

                // check for return rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var availability = new Availability()
                        {
                            AvailabilityID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            StartTime = reader.GetDateTime(2),
                            EndTime = reader.GetDateTime(3)
                        };
                        availabilities.Add(availability);
                    }
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

            return availabilities;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Submits a collection of Availability records to be inserted into the database based on the employeeid.
        /// It is assumed that the availibilites listed in the IEnumerable<> will overwrite all previous records.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="availabilities"></param>
        /// <returns></returns>
        public int EditAvailability(int employeeId, IEnumerable<Availability> availabilities)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_availability";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

            var list = new List<SqlDataRecord>();

            foreach (var item in availabilities)
            {
                var record = new SqlDataRecord(new SqlMetaData[] { new SqlMetaData("EmployeeID", SqlDbType.Int),
                                                                   new SqlMetaData("StartTime", SqlDbType.DateTime),
                                                                    new SqlMetaData("EndTime", SqlDbType.DateTime)});
                record.SetInt32(0, item.EmployeeID);
                record.SetDateTime(1, item.StartTime);
                record.SetDateTime(2, item.EndTime);
                list.Add(record);
            }
            if (list.Count == 0)
            {
                list = null;
            }
            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@tvpNewAvailabilities", list);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.AvailabilityTableType";
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
            return result;
        }
    }
}
