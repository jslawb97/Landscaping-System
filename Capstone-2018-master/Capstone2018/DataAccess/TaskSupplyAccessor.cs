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
    public class TaskSupplyAccessor : ITaskSupplyAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/04/06
        /// 
        /// Method to edit the quantity of a TaskSupply using a stored procedure
        /// </summary>
        /// <param name="oldTaskSupply"></param>
        /// <param name="newTaskSupply"></param>
        /// <returns></returns>
        public int EditTaskSupplyQuantity(TaskSupplyDetail oldTaskSupply, TaskSupplyDetail newTaskSupply)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_tasksupply_quantity";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TaskSupplyID", SqlDbType.Int);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);

            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);

            cmd.Parameters["@TaskSupplyID"].Value = oldTaskSupply.TaskSupplyTaskSupplyID;
            cmd.Parameters["@OldQuantity"].Value = oldTaskSupply.TaskSupplyQuantity;

            cmd.Parameters["@NewQuantity"].Value = newTaskSupply.TaskSupplyQuantity;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();

                if (rowcount == 0)
                {
                    throw new ApplicationException("TaskSupply edit failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the TaskSupply", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }
		
        /// <summary>
        /// Mike Mason
        /// Created 2018/04/05
        /// 
        /// Method to retrieve a list of Task Supply using a stored procedure
        /// </summary>
        /// <returns>A detail list of Task Supply</returns>
        public List<TaskSupplyDetail> RetrieveTaskSupplyDetailList(int jobID)
        {
            var taskSupplyDetail = new List<TaskSupplyDetail>();

            // Start with a SQL Connection
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_tasksupply_allocation_detail_list_by_job_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobID", jobID);


            // try to execute the command
            try
            {
                // first, open the connection
                conn.Open();

                // create a data reader by executing the command
                var reader = cmd.ExecuteReader();

                // check to see if anything was returned
                if (reader.HasRows)
                {
                    // loop through the rows
                    while (reader.Read())
                    {
                        // read the values from each row and use them
                        // to create a c# object we can use
                        var aTaskSupply = new TaskSupplyDetail()
                        {


                            TaskName = reader.GetString(0),
                            SupplyItemName = reader.GetString(1),
                            TaskTypeSupplyNeedQuantity = reader.GetInt32(2),
                            SupplyItemQuantityInStock = reader.GetInt32(3),
                            TaskSupplyQuantity = reader.GetInt32(4),
                            TaskSupplyTaskSupplyID = reader.GetInt32(5)



                        };
                        // don't leave the loop iteration without saving
                        taskSupplyDetail.Add(aTaskSupply);
                    }
                }
                reader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                // housekeeping cleanup
                conn.Close();
            }

            return taskSupplyDetail;
        }
    }
}