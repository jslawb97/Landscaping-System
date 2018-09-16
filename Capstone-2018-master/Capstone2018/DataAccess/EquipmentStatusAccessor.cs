using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class EquipmentStatusAccessor : IEquipmentStatusAccessor
    {
        /// <summary>
        /// Jacob Slaubaugh
        /// 2018/02/15
        /// 
        /// Method to create an equipment status
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public string CreateEquipmentStatus(EquipmentStatus equipmentStatus)
        {
            string newId = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_equipmentstatus";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipmentStatusID", equipmentStatus.EquipmentStatusID);

            try
            {
                conn.Open();
                cmd.ExecuteReader();
                newId = "New";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return newId;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// 2018/02/15
        /// 
        /// Method to delete an equipment status by ID
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public int DeleteEquipmentStatus(string EquipmentStatusID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_equipmentstatus_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentStatusID", SqlDbType.NVarChar);
            cmd.Parameters["@EquipmentStatusID"].Value = EquipmentStatusID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            { 
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the equipment status", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowcount;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/15
        /// 
        /// Method uses the stored procedure to retrieve the equipment status list
        /// </summary>
        /// <returns></returns>
        public List<EquipmentStatus> RetrieveEquipmentStatusList()
        {
            var equipmentStatusList = new List<EquipmentStatus>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipmentstatus_list";
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
                        var equipmentStatus = new EquipmentStatus()
                        {
                            EquipmentStatusID = reader.GetString(0)
                        };
                        equipmentStatusList.Add(equipmentStatus);
                    }
                }
                else
                {
                    throw new ApplicationException("No data found");
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
            return equipmentStatusList;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// 2018/03/01
        /// 
        /// Method to edit an equipment status by ID
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public int EditEquipmentStatus(EquipmentStatus oldEquipmentStatus, EquipmentStatus newEquipmentStatus)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_equipmentstatus_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewEquipmentStatusID", newEquipmentStatus.EquipmentStatusID);
            cmd.Parameters.AddWithValue("@OldEquipmentStatusID", oldEquipmentStatus.EquipmentStatusID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// 2018/02/15
        /// 
        /// Method to retrieve an equipment status by ID
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public EquipmentStatus RetrieveEquipmentStatusByID(string equipmentStatusID)
        {
            EquipmentStatus equipmentStatus = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipmentstatus_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EquipmentStatusID", equipmentStatusID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    equipmentStatus = new EquipmentStatus()
                    {
                        EquipmentStatusID = reader.GetString(0)
                    };
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
            return equipmentStatus;
        }
    }
}
