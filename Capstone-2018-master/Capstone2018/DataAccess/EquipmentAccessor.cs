using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EquipmentAccessor : IEquipmentAccessor
    {

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/01
        /// 
        /// calls a store procedure to retrieve an equipment by their ID
        /// </summary>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public Equipment RetrieveEquipmentByID(int equipmentID)
        {
            Equipment equipment = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EquipmentID", equipmentID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    equipment = new Equipment()
                    {
                        EquipmentID = reader.GetInt32(0),
                        EquipmentTypeID = reader.GetString(1),
                        Name = reader.GetString(2),
                        MakeModelID = reader.GetInt32(3),
                        DatePurchased = reader.GetDateTime(4),
                        //DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                        PriceAtPurchase = reader.GetDecimal(6),
                        CurrentValue = reader.GetDecimal(7),
                        //WarrantyUntil = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
                        EquipmentStatusID = reader.GetString(9),
                        EquipmentDetails = reader.GetString(11),
                        Active = reader.GetBoolean(12)
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
            return equipment;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/07
        /// 
        /// Retrieves a list of Equipment
        /// </summary>
        /// <remarks>
        /// Noah Davison
        /// 2018/29/2018 
        /// 
        /// Method is currently broken, throws an exception saying cannot convert and int to a bool.
        ///
        /// James McPherson
        /// 2018/04/03
        /// 
        /// - Fixed exception from converting int to bool
        /// - Fixed problem with getting dates from reader
        /// </remarks>
        /// <returns></returns>
        public List<Equipment> RetrieveEquipmentList()
        {
            var equipment = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_list";
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
                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2),
                            MakeModelID = reader.GetInt32(3),
                            DatePurchased = reader.GetDateTime(4),
                            DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?) reader.GetDateTime(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            CurrentValue = reader.GetDecimal(7),
                            WarrantyUntil = reader.IsDBNull(8) ? null : (DateTime?) reader.GetDateTime(8),
                            EquipmentStatusID = reader.GetString(9),
                            EquipmentDetails = reader.GetString(10),
                            Active = reader.GetBoolean(11)
                        };
                        equipment.Add(eq);
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
            return equipment;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/01/31
        /// 
        /// Retrieves a list of active equipment.
        /// 
        /// <remarks>
        /// Mike Mason
        /// 2018/04/26
        /// Added missing reader for Active needed for datagrid
        /// </remarks>
        /// </summary>
        public List<Equipment> RetrieveEquipmentListByActive(bool active = true)
        {
            var equipmentList = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Active", active);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2),
                            MakeModelID = reader.GetInt32(3),
                            DatePurchased = reader.GetDateTime(4),
                            DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            CurrentValue = reader.GetDecimal(7),
                            WarrantyUntil = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            EquipmentStatusID = reader.GetString(9),
                            EquipmentDetails = reader.GetString(10),
                            Active = reader.GetBoolean(11)
                        };
                        equipmentList.Add(eq);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return equipmentList;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/08
        /// 
        /// Deactivates an equipment object by ID
        /// </summary>
        public int DeactivateEquipmentByID(int equipmentID)
        {
            int result;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_equipment_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters["@EquipmentID"].Value = equipmentID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Method to create a new Equipment using a stored procedure
        /// </summary>
        /// <param name="equipment"></param>
        /// <returns>The the rows affected</returns>
        public int CreateEquipment(Equipment equipment)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_equipment_fixed";
            var cmd = new SqlCommand(cmdText, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@MakeModelID", SqlDbType.Int);
            cmd.Parameters.Add("@DatePurchased", SqlDbType.DateTime);
            cmd.Parameters.Add("@DateLastRepaired", SqlDbType.DateTime);
            cmd.Parameters.Add("@PriceAtPurchase", SqlDbType.Decimal);
            cmd.Parameters.Add("@CurrentValue", SqlDbType.Decimal);
            cmd.Parameters.Add("@WarrantyUntil", SqlDbType.DateTime);
            cmd.Parameters.Add("@EquipmentStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@EquipmentDetails", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@EquipmentTypeID"].Value = equipment.EquipmentTypeID;
            cmd.Parameters["@Name"].Value = equipment.Name;
            cmd.Parameters["@MakeModelID"].Value = equipment.MakeModelID;
            cmd.Parameters["@DatePurchased"].Value = equipment.DatePurchased;
            cmd.Parameters["@DateLastRepaired"].Value = equipment.DateLastRepaired.Value;
            cmd.Parameters["@PriceAtPurchase"].Value = equipment.PriceAtPurchase;
            cmd.Parameters["@CurrentValue"].Value = equipment.CurrentValue;
            cmd.Parameters["@WarrantyUntil"].Value = equipment.WarrantyUntil;
            cmd.Parameters["@EquipmentStatusID"].Value = equipment.EquipmentStatusID;
            cmd.Parameters["@EquipmentDetails"].Value = equipment.EquipmentDetails;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/02
        /// 
        /// Method to create a new Equipment using a stored procedure
        /// </summary>
        /// <param name="oldEquipment"></param>
        /// <param name="newEquipment"></param>
        /// <returns>The the rows affected</returns>
        public int EditEquipment(Equipment oldEquipment, Equipment newEquipment)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_equipment_by_id_fixed";
            var cmd = new SqlCommand(cmdText, conn);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OldEquipmentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldMakeModelID", SqlDbType.Int);
            cmd.Parameters.Add("@OldDatePurchased", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldDateLastRepaired", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldPriceAtPurchase", SqlDbType.Decimal);
            cmd.Parameters.Add("@OldCurrentValue", SqlDbType.Decimal);
            cmd.Parameters.Add("@OldWarrantyUntil", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldEquipmentStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldEquipmentDetails", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@NewEquipmentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewMakeModelID", SqlDbType.Int);
            cmd.Parameters.Add("@NewDatePurchased", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewDateLastRepaired", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewPriceAtPurchase", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewCurrentValue", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewWarrantyUntil", SqlDbType.DateTime);
            cmd.Parameters.Add("@NewEquipmentStatusID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewEquipmentDetails", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);

            cmd.Parameters["@OldEquipmentTypeID"].Value = oldEquipment.EquipmentTypeID;
            cmd.Parameters["@OldName"].Value = oldEquipment.Name;
            cmd.Parameters["@OldMakeModelID"].Value = oldEquipment.MakeModelID;
            cmd.Parameters["@OldDatePurchased"].Value = oldEquipment.DatePurchased;
            cmd.Parameters["@OldDateLastRepaired"].Value = oldEquipment.DateLastRepaired.Value;
            cmd.Parameters["@OldPriceAtPurchase"].Value = oldEquipment.PriceAtPurchase;
            cmd.Parameters["@OldCurrentValue"].Value = oldEquipment.CurrentValue;
            cmd.Parameters["@OldWarrantyUntil"].Value = oldEquipment.WarrantyUntil;
            cmd.Parameters["@OldEquipmentStatusID"].Value = oldEquipment.EquipmentStatusID;
            cmd.Parameters["@OldEquipmentDetails"].Value = oldEquipment.EquipmentDetails;
            cmd.Parameters["@NewEquipmentTypeID"].Value = newEquipment.EquipmentTypeID;
            cmd.Parameters["@NewName"].Value = newEquipment.Name;
            cmd.Parameters["@NewMakeModelID"].Value = newEquipment.MakeModelID;
            cmd.Parameters["@NewDatePurchased"].Value = newEquipment.DatePurchased;
            cmd.Parameters["@NewDateLastRepaired"].Value = newEquipment.DateLastRepaired.Value;
            cmd.Parameters["@NewPriceAtPurchase"].Value = newEquipment.PriceAtPurchase;
            cmd.Parameters["@NewCurrentValue"].Value = newEquipment.CurrentValue;
            cmd.Parameters["@NewWarrantyUntil"].Value = newEquipment.WarrantyUntil;
            cmd.Parameters["@NewEquipmentStatusID"].Value = newEquipment.EquipmentStatusID;
            cmd.Parameters["@NewEquipmentDetails"].Value = newEquipment.EquipmentDetails;
            cmd.Parameters["@EquipmentID"].Value = oldEquipment.EquipmentID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/06
        /// 
        /// Method to Retrieve a Equipment List by type and availability
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveEquipmentListByTypeAndAvailability(EquipmentType equipmentType, DateTime? startDate, DateTime? endDate)
        {
            var equipment = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_equipment_list_by_type_and_schedule";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
            cmd.Parameters["@EquipmentTypeID"].Value = equipmentType.EquipmentTypeID;
            cmd.Parameters["@StartDate"].Value = startDate;
            cmd.Parameters["@EndDate"].Value = endDate;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2),
                            MakeModelID = reader.GetInt32(3),
                            DatePurchased = reader.GetDateTime(4),
                            DateLastRepaired = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5),
                            PriceAtPurchase = reader.GetDecimal(6),
                            CurrentValue = reader.GetDecimal(7),
                            WarrantyUntil = reader.IsDBNull(8) ? null : (DateTime?)reader.GetDateTime(8),
                            EquipmentStatusID = reader.GetString(9),
                            EquipmentDetails = reader.GetString(10),
                            Active = reader.GetBoolean(11)
                        };
                        equipment.Add(eq);
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
            return equipment;
        }

        public List<Equipment> RetreiveAvailableEquipmentByJobID(int jobID)
        {
            var equipment = new List<Equipment>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_get_available_equipment_for_job";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobID", jobID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eq = new Equipment()
                        {
                            EquipmentID = reader.GetInt32(0),
                            EquipmentTypeID = reader.GetString(1),
                            Name = reader.GetString(2)
                        };
                        equipment.Add(eq);
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
            return equipment;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/05/03
        /// 
        /// Reactivates an equipment object by ID
        /// </summary>
        public int ReactivateEquipmentByID(int equipmentID)
        {
            int result;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_reactivate_equipment_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters["@EquipmentID"].Value = equipmentID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
