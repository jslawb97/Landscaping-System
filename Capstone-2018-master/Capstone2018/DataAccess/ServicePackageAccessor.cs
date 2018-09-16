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
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Accessor class for ServicePackage items and an SqlServer database
    /// </summary>
    public class ServicePackageAccessor : IServicePackageAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Adds the ServicePackage to the database
        /// </summary>
        /// <param name="servicePackage"></param>
        /// <returns></returns>
        public int CreateServicePackage(ServicePackage servicePackage)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_servicepackage";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", servicePackage.Name);
            cmd.Parameters.AddWithValue("@Description", servicePackage.Description);
            cmd.Parameters.AddWithValue("@Active", servicePackage.Active);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Updates a given ServicePackage with values from another ServicePackage to a database
        /// </summary>
        /// <param name="oldServicePackage"></param>
        /// <param name="newServicePackage"></param>
        /// <returns></returns>
        public int EditServicePackage(ServicePackage oldServicePackage, ServicePackage newServicePackage)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_servicepackage_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ServicePackageID", oldServicePackage.ServicePackageID);
            cmd.Parameters.AddWithValue("@OldName", oldServicePackage.Name);
            cmd.Parameters.AddWithValue("@NewName", newServicePackage.Name);
            cmd.Parameters.AddWithValue("@OldDescription", oldServicePackage.Description);
            cmd.Parameters.AddWithValue("@NewDescription", newServicePackage.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldServicePackage.Active);
            cmd.Parameters.AddWithValue("@NewActive", newServicePackage.Active);


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
        /// Brady Feller
        /// Created 2018/02/22
        /// 
        /// Deactivates a service package by ID.
        /// 
        /// Jacob Slaubaugh
        /// Updated 2018/04/12
        /// 
        /// Fixed the database parameter.
        /// </summary>
        /// <param name="servicePackageID"></param>
        /// <returns></returns>
        public int DeactivateServicePackageByID(int servicePackageID)
        {
            int rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_service_package_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ServicePackageID", SqlDbType.Int);
            cmd.Parameters["@ServicePackageID"].Value = servicePackageID;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the Service Package record", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all the service packages from a data store
        /// </summary>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageList()
        {//sp_retrieve_servicepackage_list
            var list = new List<ServicePackage>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_servicepackage_list";
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
                        var servicePackage = new ServicePackage()
                        {
                            ServicePackageID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Active = reader.GetBoolean(3),
                        };
                        list.Add(servicePackage);
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

            return list;
        }
    }
}
