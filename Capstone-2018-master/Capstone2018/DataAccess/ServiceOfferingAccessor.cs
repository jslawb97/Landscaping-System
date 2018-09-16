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
    public class ServiceOfferingAccessor : IServiceOfferingAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/20
        /// 
        /// Method that returns a list of service offering items
        /// 
        /// 
        /// Jacob Conley
        /// Updated on 2018/03/30
        /// Matched the return to what is supposed to be returned by the 
        /// stored procedure
        /// </summary>
        /// <returns></returns>
        public List<ServiceOffering> RetrieveServiceOfferingList()
        {
            var serviceOfferingsList = new List<ServiceOffering>();

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_serviceofferings";

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
                        var serviceOffering = new ServiceOffering()
                        {
                            ServiceOfferingID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                        };
                        serviceOfferingsList.Add(serviceOffering);
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
            return serviceOfferingsList;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// Adds ServiceOffering to the database
        /// </summary>
        int IServiceOfferingAccessor.CreateServiceOffering(ServiceOffering serviceOffering)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_serviceoffering";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", serviceOffering.Name);
            cmd.Parameters.AddWithValue("@Description", serviceOffering.Description);
            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    throw new ApplicationException("ServiceOffering addition failed.");
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
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// Edit ServiceOffering in the database
        /// </summary>
        int IServiceOfferingAccessor.EditServiceOffering(ServiceOffering oldServiceOffering, ServiceOffering newServiceOffering)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_serviceoffering_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceOfferingID", oldServiceOffering.ServiceOfferingID);
            cmd.Parameters.AddWithValue("@OldName", oldServiceOffering.Name);
            cmd.Parameters.AddWithValue("@NewName", newServiceOffering.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldServiceOffering.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newServiceOffering.Description);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Service Offering update failed.");
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
        /// Marshall Sejkora
        /// Noah Davison
        /// Created: 2018/02/22
        /// 
        /// Delete ServiceOffering in the database
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/26
        /// 
        /// Changed to use the current stored procedure name for deleting by id.
        /// </summary>
        int IServiceOfferingAccessor.DeleteServiceOfferingByID(int serviceOfferingID)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_delete_serviceoffering_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServiceOfferingID", serviceOfferingID);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new ApplicationException("Service Offering Delete failed.");
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
    }
}
