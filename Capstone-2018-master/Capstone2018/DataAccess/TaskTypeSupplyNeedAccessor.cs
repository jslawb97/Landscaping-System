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
    public class TaskTypeSupplyNeedAccessor : ITaskTypeSupplyNeedAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Creates a new task type supply need item
        /// </summary>
        /// <param name="taskSupply"></param>
        /// <returns></returns>
        public int CreateTaskTypeSupplyNeed(TaskTypeSupplyNeed taskSupply)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_tasktypesupply";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@SupplyItemID", SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);

            cmd.Parameters["@TaskTypeID"].Value = taskSupply.TaskTypeID;
            cmd.Parameters["@SupplyItemID"].Value = taskSupply.SupplyItemID;
            cmd.Parameters["@Quantity"].Value = taskSupply.Quantity;

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

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Deactivates an existing TaskTypeSupplyNeed item
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="supplyItemID"></param>
        /// <returns></returns>
        public int DeactivateTaskTypeSupplyNeedByID(int taskTypeSupplyNeedID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_deactivate_tasktypesupplyneed";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TaskTypeSupplyNeedID", taskTypeSupplyNeedID);

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new ApplicationException("Task Type Supply Need Item deactivation failed.");
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
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Edits an existing task type supply need item 
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public int EditTaskTypeSupplyNeed(TaskTypeSupplyNeed oldTaskSupply, TaskTypeSupplyNeed newTaskSupply)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_edit_tasktypesupply_by_task_and_supplyitemid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@TaskTypeID", SqlDbType.Int);
            cmd.Parameters.Add("@SupplyItemID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            cmd.Parameters["@TaskTypeID"].Value = oldTaskSupply.TaskTypeID;
            cmd.Parameters["@SupplyItemID"].Value = oldTaskSupply.SupplyItemID;
            cmd.Parameters["@OldQuantity"].Value = oldTaskSupply.Quantity;
            cmd.Parameters["@NewQuantity"].Value = newTaskSupply.Quantity;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Task Type Supply Need Item update failed.");
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
        /// Jacob Conley
        /// Created on 2018/03/29
        /// 
        /// Retrieves all task type supply need items
        /// </summary>
        /// <returns></returns>
        public List<TaskTypeSupplyNeed> RetrieveTaskTypeSupplyNeedList()
        {
            List<TaskTypeSupplyNeed> taskSupplies = new List<TaskTypeSupplyNeed>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_updated_tasktypesupplies";

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
                        var supplyOrder = new TaskTypeSupplyNeed()
                        {
                            TaskTypeSupplyNeedID = reader.GetInt32(0),
                            TaskTypeID = reader.GetInt32(1),
                            SupplyItemID = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            Active = reader.GetBoolean(4)
                        };
                        taskSupplies.Add(supplyOrder);
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


            return taskSupplies;
        }
    }
}
