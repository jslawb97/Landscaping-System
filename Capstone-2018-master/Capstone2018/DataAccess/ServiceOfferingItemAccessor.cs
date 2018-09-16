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
    public class ServiceOfferingItemAccessor : IServiceOfferingItemAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Creates the connection between the service item and service offering in the database
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden Tollefson added throw new ApplicationException in the catch block 5/4/2018</remarks>
        public int CreateServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_serviceofferingitem";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceItemID", serviceOfferingItem.ServiceItemID);
            cmd.Parameters.AddWithValue("@ServiceOfferingID", serviceOfferingItem.ServiceOfferingID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Service Offering Item addition failed.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to connect to the database" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Removes the connection between the service item and service offering in the database
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden Tollefson added throw new ApplicationException in the catch block 5/4/2018</remarks>
        public int DeleteServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_serviceofferingitem_by_ids";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceItemID", serviceOfferingItem.ServiceItemID);
            cmd.Parameters.AddWithValue("@ServiceOfferingID", serviceOfferingItem.ServiceOfferingID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("There was a problem with the database" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deleting the service offering item", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Method to retrieve service offering items from the database with the given service item id
        /// </summary>
        /// <param name="serviceItemID"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden Tollefson added throw new ApplicationException in the catch block 5/4/2018</remarks>
        public List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceItemID(int serviceItemID)
        {
            List<ServiceOfferingItem> serviceOfferingItems = new List<ServiceOfferingItem>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_serviceofferingitems_by_serviceitemid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceItemID", serviceItemID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var serviceOfferingItem = new ServiceOfferingItem()
                        {
                            ServiceItemID = reader.GetInt32(0),
                            ServiceOfferingID = reader.GetInt32(1)
                        };
                        serviceOfferingItems.Add(serviceOfferingItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to connect to the database" + ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return serviceOfferingItems;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Method to retrieve service item ids from the database with the given service offering id
        /// </summary>
        /// <param name="serviceOfferingID"></param>
        /// <returns></returns>
        /// <remarks>QA Jayden Tollefson added throw new ApplicationException in the catch block 5/4/2018</remarks>
        public List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceOfferingID(int serviceOfferingID)
        {
            List<ServiceOfferingItem> serviceOfferingItems = new List<ServiceOfferingItem>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_serviceofferingitems_by_serviceofferingid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServiceOfferingID", serviceOfferingID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var serviceOfferingItem = new ServiceOfferingItem()
                        {
                            ServiceItemID = reader.GetInt32(0),
                            ServiceOfferingID = reader.GetInt32(1)
                        };
                        serviceOfferingItems.Add(serviceOfferingItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to connect to the database" + ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return serviceOfferingItems;
        }
    }
}
