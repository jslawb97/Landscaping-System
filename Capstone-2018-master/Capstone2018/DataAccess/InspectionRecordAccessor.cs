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
    public class InspectionRecordAccessor : IInspectionRecordAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/03/07
        /// 
        /// Method to create an InspectionRecord
        /// </summary>
        /// <param name="inspectionRecord"></param>
        /// <returns>ID of new InspectionRecord</returns>
        public int CreateInspectionRecord(InspectionRecord inspectionRecord)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_inspectionrecord";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Date", SqlDbType.Date);

            cmd.Parameters["@EquipmentID"].Value = inspectionRecord.EquipmentID;
            cmd.Parameters["@EmployeeID"].Value = inspectionRecord.EmployeeID;
            cmd.Parameters["@Description"].Value = inspectionRecord.Description;
            cmd.Parameters["@Date"].Value = inspectionRecord.Date;

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newID = (int)id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem creating the inspection record", ex);
            }
            finally
            {
                conn.Close();
            }

            return newID;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to edit an InspectionRecord using a stored procedure
        /// </summary>
        /// <param name="oldInspectionRecord"></param>
        /// <param name="newInspectionRecord"></param>
        /// <returns></returns>
        public int EditInspectionRecord(InspectionRecord oldInspectionRecord, InspectionRecord newInspectionRecord)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_inspectionrecord_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InspectionRecordID", SqlDbType.Int);
            cmd.Parameters.Add("@NewEquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@NewEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@NewDate", SqlDbType.Date);

            cmd.Parameters.Add("@OldEquipmentID", SqlDbType.Int);
            cmd.Parameters.Add("@OldEmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@OldDate", SqlDbType.Date);

            cmd.Parameters["@InspectionRecordID"].Value = oldInspectionRecord.InspectionRecordID;
            cmd.Parameters["@NewEquipmentID"].Value = newInspectionRecord.EquipmentID;
            cmd.Parameters["@NewEmployeeID"].Value = newInspectionRecord.EmployeeID;
            cmd.Parameters["@NewDescription"].Value = newInspectionRecord.Description;
            cmd.Parameters["@NewDate"].Value = newInspectionRecord.Date;

            cmd.Parameters["@OldEquipmentID"].Value = oldInspectionRecord.EquipmentID;
            cmd.Parameters["@OldEmployeeID"].Value = oldInspectionRecord.EmployeeID;
            cmd.Parameters["@OldDescription"].Value = oldInspectionRecord.Description;
            cmd.Parameters["@OldDate"].Value = oldInspectionRecord.Date;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
                
                if(rowcount == 0)
                {
                    throw new ApplicationException("InspectionRecord edit failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the InspectionRecord", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to retrieve an InspectionRecord by ID using a stored procedure
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns>InspectionRecord</returns>
        public InspectionRecord RetrieveInspectionRecordByID(int inspectionRecordID)
        {
            InspectionRecord inspectionRecord = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionrecord_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InspectionRecordID", SqlDbType.Int);
            cmd.Parameters["@InspectionRecordID"].Value = inspectionRecordID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    inspectionRecord = new InspectionRecord
                    {
                        InspectionRecordID = reader.GetInt32(0),
                        EquipmentID = reader.GetInt32(1),
                        EmployeeID = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        Date = reader.GetDateTime(4)
                    };
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

            return inspectionRecord;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to retrieve a list of InspectionRecords by EquipmentID using a
        /// stored procedure
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns>List of InspectionRecord</returns>
        public List<InspectionRecord> RetrieveInspectionRecordListByEquipmentID(int equipmentID)
        {
            var inspectionRecordList = new List<InspectionRecord>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionrecord_list_by_equipmentid";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EquipmentID", SqlDbType.Int);
            cmd.Parameters["@EquipmentID"].Value = equipmentID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var inspectionRecord = new InspectionRecord
                        {
                            InspectionRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                        inspectionRecordList.Add(inspectionRecord);
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

            return inspectionRecordList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to deactivate an InspectionRecord by ID using
        /// a stored procedure
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns></returns>
        public int DeleteInspectionRecordByID(int inspectionRecordID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_inspectionrecord_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InspectionRecordID", SqlDbType.Int);
            cmd.Parameters["@InspectionRecordID"].Value = inspectionRecordID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
                    
                if(rowcount == 0)
                {
                    throw new ApplicationException("No data deleted");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the InspectionRecord", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to retrieve all InspectionRecords using a
        /// stored procedure
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecord> RetrieveInspectionRecordList()
        {
            List<InspectionRecord> inspectionRecordList = new List<InspectionRecord>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionrecord_list";
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
                        var inspectionRecord = new InspectionRecord
                        {
                            InspectionRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                        inspectionRecordList.Add(inspectionRecord);
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

            return inspectionRecordList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/02
        /// 
        /// Method to retrieve a list of InspectionRecordDetail using a
        /// stored procedure
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecordDetail> RetrieveInspectionRecordDetailList()
        {
            List<InspectionRecordDetail> inspectionRecordDetailList = new List<InspectionRecordDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_inspectionrecord_detail_list";
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
                        var inspectionRecordDetail = new InspectionRecordDetail
                        {
                            InspectionRecordID = reader.GetInt32(0),
                            EquipmentID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            Description = reader.GetString(3),
                            Date = reader.GetDateTime(4),
                            EquipmentName = reader.GetString(5),
                            EmployeeLastName = reader.GetString(6),
                            EmployeeFirstName = reader.GetString(7)
                        };
                        inspectionRecordDetailList.Add(inspectionRecordDetail);
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

            return inspectionRecordDetailList;
        }
    }
}
