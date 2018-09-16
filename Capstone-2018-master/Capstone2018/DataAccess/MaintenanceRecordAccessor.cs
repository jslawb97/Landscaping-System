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
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/01
    /// </summary>
    /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
    public class MaintenanceRecordAccessor : IMaintenanceRecordAccessor
    {
        /// <summary>
        /// Brady Feller
        /// 2018/03/01
        /// 
        /// Calls a store procedure to create a maintenance record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public int CreateMaintenanceRecord(MaintenanceRecord record)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_maintenance_record";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EquipmentID", record.EquipmentID);
            cmd.Parameters.AddWithValue("@EmployeeID", record.EmployeeID);
            cmd.Parameters.AddWithValue("@Description", record.Description);
            cmd.Parameters.AddWithValue("@Date", record.Date);

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
        /// Jacob Slaubaugh
        /// Created 2018/04/20
        /// 
        /// Calls the stored procedure to delete a maintenance record
        /// </summary>
        /// <param name="maintenanceRecordID"></param>
        /// <returns></returns>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public int DeleteMaintenanceRecordByID(int maintenanceRecordID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_maintenancerecord_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaintenanceRecordID", SqlDbType.Int);
            cmd.Parameters["@MaintenanceRecordID"].Value = maintenanceRecordID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the maintenance record", ex);
            }
            finally
            {
                conn.Close();
            }
            return rowcount;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/01
        /// 
        /// calls a stored procedure to edit a maintence record
        /// </summary>
        /// <param name="oldRecord"></param>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public int EditMaintenanceRecord(MaintenanceRecord oldRecord, MaintenanceRecord newRecord)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_maintenance_record";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaintenanceRecordID", newRecord.MaintenanceRecordID);
            cmd.Parameters.AddWithValue("@NewEquipmentID", newRecord.EquipmentID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newRecord.EmployeeID);
            cmd.Parameters.AddWithValue("@NewDescription", newRecord.Description);
            cmd.Parameters.AddWithValue("@NewDate", newRecord.Date);

            cmd.Parameters.AddWithValue("@OldEquipmentID", oldRecord.EquipmentID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldRecord.EmployeeID);
            cmd.Parameters.AddWithValue("@OldDescription", oldRecord.Description);
            cmd.Parameters.AddWithValue("@OldDate", oldRecord.Date);

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
        /// Created 2018/03/01
        /// 
        /// calls a strored procedure to display a maintenance record
        /// </summary>
        /// <param name="maintenanceRecordID"></param>
        /// <returns></returns>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public MaintenanceRecord RetrieveMaintenanceRecordByID(int maintenanceRecordID)
        {
            MaintenanceRecord maintenanceRecord = null;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_maintenance_record_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaintenanceRecordID", maintenanceRecordID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    maintenanceRecord = new MaintenanceRecord()
                    {
                        MaintenanceRecordID = reader.GetInt32(0),
                        EquipmentID = reader.GetInt32(1),
                        EmployeeID = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Date = reader.GetDateTime(4)
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
            return maintenanceRecord;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/27
        /// 
        /// Retrieves list of maintenance record details
        /// </summary>
        /// <returns></returns>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public List<MaintenanceRecordDetail> RetrieveMaintenanceRecordDetailList()
        {

            var maintenanceRecordDetailList = new List<MaintenanceRecordDetail>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_maintenance_record_detail_list";

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
                        var maintenanceRecord = new MaintenanceRecord()
                        {
                            MaintenanceRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4).Date,
                        };

                        var equipment = new Equipment()
                        {
                            Name = reader.GetString(5),
                        };

                        var employee = new Employee()
                        {
                            FirstName = reader.GetString(6),
                            LastName = reader.GetString(7),
                        };

                        var maintenanceRecordDetail = new MaintenanceRecordDetail()
                        {
                            MaintenanceRecord = maintenanceRecord,
                            Equipment = equipment,
                            Employee = employee,

                        };
                        maintenanceRecordDetailList.Add(maintenanceRecordDetail);
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
            return maintenanceRecordDetailList;

        }
    }
}
