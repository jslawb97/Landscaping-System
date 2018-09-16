using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class PersonalEquipmentAccessor : IPersonalEquipmentAccessor
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Adds an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int CreatePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_add_personal_equipment_assignment";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@PersonalEquipmentID", pEquipmentID);

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem adding the Personal Equipment Assignment", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Removes an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int DeletePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_personal_equipment_assignment";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@PersonalEquipmentID", pEquipmentID);

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem removing the Personal Equipment Assignment", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment assigned to an employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrieveAssignedPersonalEquipmentByEmployeeID(int employeeID)
        {
            var eqList = new List<PersonalEquipment>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_assigned_personal_equipment_by_employee_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eq = new PersonalEquipment()
                        {
                            PersonalEquipmentID = reader.GetInt32(0),
                            PersonalEquipmentType = reader.GetString(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            PersonalEquipmentStatus = reader.GetString(4),
                            Assigned = reader.GetBoolean(5)
                        };
                        eqList.Add(eq);
                    }
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

            return eqList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment with the given assignment value
        /// </summary>
        /// <param name="assigned"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrievePersonalEquipmentByAssigned(bool assigned)
        {
            var eqList = new List<PersonalEquipment>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_personal_equipment_by_assigned";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Assigned", assigned);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eq = new PersonalEquipment()
                        {
                            PersonalEquipmentID = reader.GetInt32(0),
                            PersonalEquipmentType = reader.GetString(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            PersonalEquipmentStatus = reader.GetString(4),
                            Assigned = reader.GetBoolean(5)
                        };
                        eqList.Add(eq);
                    }
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

            return eqList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Updates the assigned status of a PersonalEquipment item 
        /// </summary>
        /// <param name="pEquipmentID"></param>
        /// <param name="assigned"></param>
        /// <returns></returns>
        public int EditPersonalEquipmentAssignment(int pEquipmentID, bool assigned)
        {
            int rowCount;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_update_personal_equipment_assignment";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PersonalEquipmentID", pEquipmentID);
            cmd.Parameters.AddWithValue("@Assigned", assigned);

            try
            {
                conn.Open();
                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem removing the Personal Equipment Assignment", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowCount;
        }
    }
}

