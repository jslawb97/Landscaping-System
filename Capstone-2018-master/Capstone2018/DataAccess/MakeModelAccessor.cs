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
    public class MakeModelAccessor : IMakeModelAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to create a new MakeModel using a stored procedure
        /// </summary>
        /// <param name="makeModel"></param>
        /// <returns>The auto-generated ID for the new MakeModel</returns>
        public int CreateMakeModel(MakeModel makeModel)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_makemodel";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Make", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Model", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@MaintenanceChecklistID", SqlDbType.Int);

            cmd.Parameters["@Make"].Value = makeModel.Make;
            cmd.Parameters["@Model"].Value = makeModel.Model;
            if (makeModel.MaintenanceChecklistID != null)
            {
                cmd.Parameters["@MaintenanceChecklistID"].Value = makeModel.MaintenanceChecklistID;
            }
            else
            {
                cmd.Parameters["@MaintenanceChecklistID"].Value = DBNull.Value;
            }

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newID = (int)id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem creating the MakeModel", ex);
            }
            finally
            {
                conn.Close();
            }

            return newID;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to edit an existing MakeModel using a stored procedure
        /// </summary>
        /// <param name="oldMakeModel"></param>
        /// <param name="newMakeModel"></param>
        /// <returns>Rows affected</returns>
        public int EditMakeModel(MakeModel oldMakeModel, MakeModel newMakeModel)
        {
            var rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_makemodel_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MakeModelID", SqlDbType.Int);

            cmd.Parameters.Add("@NewMake", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewModel", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewMaintenanceChecklistID", SqlDbType.Int);

            cmd.Parameters.Add("@OldMake", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldModel", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldMaintenanceChecklistID", SqlDbType.Int);

            cmd.Parameters["@MakeModelID"].Value = newMakeModel.MakeModelID;

            cmd.Parameters["@NewMake"].Value = newMakeModel.Make;
            cmd.Parameters["@NewModel"].Value = newMakeModel.Model;
            cmd.Parameters["@NewMaintenanceChecklistID"].Value = newMakeModel.MaintenanceChecklistID;
            
            cmd.Parameters["@OldMake"].Value = oldMakeModel.Make;
            cmd.Parameters["@OldModel"].Value = oldMakeModel.Model;
            cmd.Parameters["@OldMaintenanceChecklistID"].Value = oldMakeModel.MaintenanceChecklistID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();

                if(rowcount == 0)
                {
                    throw new ApplicationException("MakeModel edit failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the MakeModel", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to retrieve a list of MakeModels using a stored procedure
        /// </summary>
        /// <returns>A list of MakeModels</returns>
        public List<MakeModel> RetrieveMakeModelList()
        {
            List<MakeModel> makeModelList = new List<MakeModel>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_makemodel_list";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var makeModel = new MakeModel()
                        {
                            MakeModelID = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            MaintenanceChecklistID = reader.IsDBNull(3) ? null : (int?) reader.GetInt32(3),
                        };
                        makeModelList.Add(makeModel);
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

            return makeModelList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to retrieve a makemodel by ID using a stored procedure
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns>A MakeModel</returns>
        public MakeModel RetrieveMakeModelByID(int makeModelID)
        {
            MakeModel makeModel = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_makemodel_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MakeModelID", SqlDbType.Int);
            cmd.Parameters["@MakeModelID"].Value = makeModelID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    makeModel = new MakeModel()
                    {
                        MakeModelID = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        MaintenanceChecklistID = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3),
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

            return makeModel;
        }
        
        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate a makemodel by ID using a stored procedure
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns>Rows affected</returns>
        public int DeactivateMakeModelByID(int makeModelID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_makemodel_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MakeModelID", SqlDbType.Int);
            cmd.Parameters["@MakeModelID"].Value = makeModelID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the MakeModel", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }


        public List<MakeModel> RetrieveMakeModelListByActive(bool active = true)
        {
            List<MakeModel> makeModelList = new List<MakeModel>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_makemodel_list_by_active";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var makeModel = new MakeModel()
                        {
                            MakeModelID = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            MaintenanceChecklistID = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3),
                        };
                        makeModelList.Add(makeModel);
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

            return makeModelList;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/14
        /// 
        /// Retrieves a list of MakeModels by make
        /// </summary>
        public List<MakeModel> RetrieveMakeModelListByMake(string make)
        {
            List<MakeModel> makeModelList = new List<MakeModel>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_makemodel_list_by_make";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Make", SqlDbType.NVarChar);
            cmd.Parameters["@Make"].Value = make;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var makeModel = new MakeModel()
                        {
                            MakeModelID = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            MaintenanceChecklistID = reader.GetInt32(3)
                        };
                        makeModelList.Add(makeModel);
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

            return makeModelList;
        }

        /// <summary>
        /// James Mcpherson
        /// Created 2018/04/26
        /// 
        /// Method to retrieve a list of MakeModelDetails using a stored procedure
        /// </summary>
        /// <returns></returns>
        public List<MakeModelDetail> RetrieveMakeModelDetailList()
        {
            List<MakeModelDetail> makeModelList = new List<MakeModelDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_makemodel_detail_list";
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
                        var makeModel = new MakeModelDetail()
                        {
                            MakeModelID = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            Active = reader.GetBoolean(3),
                            MaintenanceChecklistID = reader.GetInt32(4),
                            MaintenanceChecklistName = reader.GetString(5)
                        };
                        makeModelList.Add(makeModel);
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

            return makeModelList;
        }
    }
}
