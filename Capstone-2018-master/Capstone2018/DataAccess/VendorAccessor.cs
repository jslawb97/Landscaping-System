using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccess
{
    /// <summary>
    /// Facilitates Vendor data movement between the application and the SqlServer database 
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/02/01
    /// </remarks>
    public class VendorAccessor : IVendorAccessor
    {
        /// <summary>
        /// John Miller
        /// Updated 2018/02/23
        /// 
        /// Retrieves a list of Active Vendors
        /// </summary>
        /// <returns>a list of active vendors</returns>
        public List<Vendor> RetrieveVendorByActive()
        {
            List<Vendor> vendors = new List<Vendor>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_vendor_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vendor = new Vendor()
                        {

                            VendorID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Rep = reader.GetString(2),
                            Address = reader.GetString(3),
                            Website = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Active = reader.GetBoolean(6)

                        };
                        vendors.Add(vendor);
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
            return vendors;
        }

        /// <summary>
        /// Retrieves a list of Vendor objects from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>A list of Vendors from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        public List<Vendor> RetrieveVendorList()
        {
            List<Vendor> vendors = new List<Vendor>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_vendor_list";
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
                        var vendor = new Vendor()
                        {

                            VendorID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Rep = reader.GetString(2),
                            Address = reader.GetString(3),
                            Website = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Active = reader.GetBoolean(6)

                        };
                        vendors.Add(vendor);
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
            return vendors;
        }

        /// <summary>
        /// John Miller
        /// Created: 2018/02/15
        /// 
        /// Deactivates the vendor record with the given ID in the SqlServer database
        /// </summary>
        /// <param name="vendorID">The ID of the Vendor to be deactivated</param>
        /// <returns>True if successful, false if unsuccessful</returns>
        public bool DeactivateVendorByID(int vendorID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_vendor_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VendorID", vendorID);

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

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sends data to edit an existing vendor in the database by VendorID
        /// </summary>
        /// <param name="OldVendor">The Vendor being edited</param>
        /// <param name="NewVendor">The Vendor with the new data</param>
        /// <returns>true if the update succeeded, and false if the update failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        public bool EditVendor(Vendor OldVendor, Vendor NewVendor)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_vendor";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VendorID", OldVendor.VendorID);
            cmd.Parameters.AddWithValue("@OldName", OldVendor.Name);
            cmd.Parameters.AddWithValue("@NewName", NewVendor.Name);
            cmd.Parameters.AddWithValue("@OldRep", OldVendor.Rep);
            cmd.Parameters.AddWithValue("@NewRep", NewVendor.Rep);
            cmd.Parameters.AddWithValue("@OldAddress", OldVendor.Address);
            cmd.Parameters.AddWithValue("@NewAddress", NewVendor.Address);
            cmd.Parameters.AddWithValue("@OldWebsite", OldVendor.Website);
            cmd.Parameters.AddWithValue("@NewWebsite", NewVendor.Website);
            cmd.Parameters.AddWithValue("@OldPhone", OldVendor.Phone);
            cmd.Parameters.AddWithValue("@NewPhone", NewVendor.Phone);
            cmd.Parameters.AddWithValue("@OldActive", OldVendor.Active);
            cmd.Parameters.AddWithValue("@NewActive", NewVendor.Active);

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

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sends data to create a new Vendor in the database
        /// </summary>
        /// <param name="vendor">The Vendor being added to the database</param>
        /// <returns>True if successful, False if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// Updated2018/02/01        
        /// </remarks>
        public bool CreateVendor(Vendor vendor)
        {
            var newID = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_vendor";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", vendor.Name);
            cmd.Parameters.AddWithValue("@Rep", vendor.Rep);
            cmd.Parameters.AddWithValue("@Address", vendor.Address);
            cmd.Parameters.AddWithValue("@Website", vendor.Website);
            cmd.Parameters.AddWithValue("@Phone", vendor.Phone);

            try
            {
                conn.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (newID != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves a Vendor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Vendor from the Sql database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/04/27 by Zach Murphy
        /// </remarks>
        public Vendor RetrieveVendorByID(int? id)
        {
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_vendor_by_id";
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            cmd.Parameters.Add("@VendorID", SqlDbType.Int, 100);
            cmd.Parameters["@VendorID"].Value = id;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Vendor()
                        {

                            VendorID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Rep = reader.GetString(2),
                            Address = reader.GetString(3),
                            Website = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                    }
                }
                return null;
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
