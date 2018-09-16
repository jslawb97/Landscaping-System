using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccess
{
    public class EquipmentTypeAccessor : IEquipmentTypeAccessor
    {
        /// <summary>
        /// Brady Feller
        /// Created: 2018/01/31
        /// 
        /// Inserts a new Equipment Type record into the Database
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18 Throw an inner exception message in the catch block</remarks>
        public int CreateEquipmentType(EquipmentType equipmentType)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_equipment_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipmentTypeID", equipmentType.EquipmentTypeID);
            cmd.Parameters.AddWithValue("@InspectionChecklistID", equipmentType.InspectionChecklistID);
            cmd.Parameters.AddWithValue("@PrepChecklistID", equipmentType.PrepChecklistID);
            cmd.Parameters.AddWithValue("@Active", equipmentType.Active);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return newId;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/01/31
        /// 
        /// Retrieves a list of all Equipment Types from the Database
        /// </summary>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        public List<EquipmentType> RetrieveEquipmentTypeList()
        {
            var equipmentType = new List<EquipmentType>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_type";
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
                        var et = new EquipmentType()
                        {
                            EquipmentTypeID = reader.GetString(0),
                            InspectionChecklistID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                            PrepChecklistID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                            Active = reader.GetBoolean(3)
                        };
                        equipmentType.Add(et);
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
            return equipmentType;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/01/31
        /// 
        /// Updates an Equipment Type Record in the Database
        /// </summary>
        /// <param name="oldEquipmentType"></param>
        /// <param name="newEquipmentType"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18 Throw an inner exception message in the catch block</remarks>
        public int EditEquipmentType(EquipmentType oldEquipmentType, EquipmentType newEquipmentType)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_equipment_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipmentTypeID", newEquipmentType.EquipmentTypeID);
            cmd.Parameters.AddWithValue("@NewPrepChecklistID", newEquipmentType.PrepChecklistID);
            cmd.Parameters.AddWithValue("@NewInspectionChecklistID", newEquipmentType.InspectionChecklistID);

            cmd.Parameters.AddWithValue("@OldPrepChecklistID", oldEquipmentType.PrepChecklistID);
            cmd.Parameters.AddWithValue("@OldInspectionChecklistID", oldEquipmentType.InspectionChecklistID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return rows;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/01/31
        /// 
        /// Retrieves an equipment type by their ID
        /// </summary>
        /// <param name="equipmentTypeID"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        public EquipmentType RetrieveEquipmentTypeByID(string equipmentTypeID)
        {
            EquipmentType equipmentType = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_type_by_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EquipmentTypeID", equipmentTypeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    equipmentType = new EquipmentType()
                    {
                        EquipmentTypeID = reader.GetString(0),
                        PrepChecklistID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                        InspectionChecklistID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                        Active = reader.GetBoolean(3)
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
            return equipmentType;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate an EquipmentType by ID using a stored procedure
        /// </summary>
        /// <param name="equipmentTypeID"></param>
        /// <returns>Rows affected</returns>
        /// <remarks>QA Jayden T 4/20/18</remarks>
        public int DeactivateEquipmentTypeByID(string equipmentTypeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_equipmenttype_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@EquipmentTypeID"].Value = equipmentTypeID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the EquipmentType", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/09
        /// 
        /// Data access to retrieve equipment type detail list
        /// </summary>
        /// <returns></returns>
        public List<EquipmentTypeDetail> RetrieveEquipmentTypeDetailList()
        {
            var equipmentTypeDetailList = new List<EquipmentTypeDetail>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_type_detail_list";
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
                        var et = new EquipmentType()
                        {
                            EquipmentTypeID = reader.GetString(0),
                            InspectionChecklistID = reader.IsDBNull(1) ? null : (int?)reader.GetInt32(1),
                            PrepChecklistID = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2),
                            Active = reader.GetBoolean(3)
                        };
                        var ic = new InspectionChecklist();
                        if (et.InspectionChecklistID != null)
                        {
                            ic.InspectionChecklistID = (int)et.InspectionChecklistID;
                            ic.Name = reader.GetString(4);
                        }
                        var pc = new PrepChecklist();
                        if (et.PrepChecklistID != null)
                        {
                            pc.PrepChecklistID = (int)et.PrepChecklistID;
                            pc.Name = reader.GetString(5);
                        }
                        var etd = new EquipmentTypeDetail
                        {
                            EquipmentType = et,
                            InspectionChecklist = ic,
                            PrepChecklist = pc
                        };
                        equipmentTypeDetailList.Add(etd);              
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
            return equipmentTypeDetailList;
        }
    }
}
