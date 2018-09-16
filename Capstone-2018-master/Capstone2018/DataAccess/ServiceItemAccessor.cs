using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccess {
    public class ServiceItemAccessor : IServiceItemAccessor {
        
        /// <summary>
        /// Creates a new InspectionChecklist item.
        /// </summary>
        /// <param name="newItem">The new InspectionChecklist item</param>
        /// <returns>The ID of the newly added InspectionChecklist item</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/22
        /// 
        /// Jacob Conley
        /// Updated 2018/04/06
        /// 
        /// Changed to reflect the create_serviceitem stored procedure in the database.
        /// </remarks>
        public int CreateServiceItem(ServiceItem newItem)
        {
            int newID;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_serviceitem";

            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            //cmd.Parameters.AddWithValue("@ServiceOfferingID", newItem.ServiceItemOffering.ServiceItemOfferingID);
            cmd.Parameters.AddWithValue("@Name", newItem.Description);
            cmd.Parameters.AddWithValue("@Description", newItem.Description);
            cmd.Parameters.AddWithValue("@Active", newItem.Active);

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                conn.Close();
            }
            
            return newID;
        }


    /// <summary>
    /// Amanda Tampir
    /// Created: 2018/2/19
    /// 
    /// Deactivates specified Service Item by ServiceItemID
    /// </summary>
    /// <param name="ServiceItemID">The ServiceItemID</param>
    /// <returns>The number of records affected</returns>
    public int DeactivateServiceItemByID(int ServiceItemID)
        {

            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_serviceitem_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceItemID", ServiceItemID);

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
        /// Amanda Tampir
        /// Created: 2018/2/24
        /// 
        /// edits specified Service Item by ServiceItemID
        /// </summary>
        /// <param name="ServiceItemID">The ServiceItemID</param>
        /// <returns>The number of records affected</returns>
        public int EditServiceItemByID(ServiceItem oldServiceItem, ServiceItem newServiceItem)
        {

            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_serviceitem_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceItemID", oldServiceItem.ServiceItemID);
            cmd.Parameters.AddWithValue("@OldName", oldServiceItem.Name);
            cmd.Parameters.AddWithValue("@NewName", newServiceItem.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldServiceItem.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newServiceItem.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldServiceItem.Active);
            cmd.Parameters.AddWithValue("@NewActive", newServiceItem.Active);

            //cmd.Parameters.AddWithValue("@OldServiceOfferingID", oldServiceItem.ServiceOfferingID);
            //cmd.Parameters.AddWithValue("@NewServiceOfferingID", newServiceItem.ServiceOfferingID);

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
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Methods uses the stored procedure to retrieve the service items list
        /// </summary>
        /// <returns></returns>
        public List<ServiceItem> RetrieveServiceItemList()
        {
            var serviceItemList = new List<ServiceItem>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_serviceitem_list";
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
                        var serviceItem = new ServiceItem()
                        {
                            ServiceItemID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };
                        serviceItemList.Add(serviceItem);
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
            return serviceItemList;
        }

    }
}
