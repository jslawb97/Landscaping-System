using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using Microsoft.SqlServer.Server;

namespace DataAccess
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/03/10
    /// 
    /// Data access for JobLocation related objects and a Sql Server database
    /// </summary>
    public class JobLocationAccessor : IJobLocationAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a JobLocation record in the SqlServer database. Returns the id of the newly created record
        /// </summary>
        /// <param name="jobLocation"></param>
        /// <returns></returns>
        public int CreateJobLocation(JobLocation jobLocation)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_joblocation";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", jobLocation.CustomerID);
            cmd.Parameters.AddWithValue("@Street", jobLocation.Street);
            cmd.Parameters.AddWithValue("@City", jobLocation.City);
            cmd.Parameters.AddWithValue("@State", jobLocation.State);
            cmd.Parameters.AddWithValue("@ZipCode", jobLocation.ZipCode);
            cmd.Parameters.AddWithValue("@Comments", jobLocation.Comments);

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
        /// Created 2018/03/10
        /// 
        /// Merges the given list data with data in the sql server database
        /// If the record does not exist in the database, it is inserted, else it is updated with the data from the list.
        /// </summary>
        /// <param name="jobLocationAttributes"></param>
        /// <returns></returns>
        public int CreateUpdateJobLocationAttributes(IEnumerable<JobLocationAttribute> jobLocationAttributes)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_edit_joblocationattribute";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            var list = new List<SqlDataRecord>();

            foreach (var item in jobLocationAttributes)
            {
                var record = new SqlDataRecord(new SqlMetaData[] { new SqlMetaData("JobLocationID", SqlDbType.Int),
                                                                   new SqlMetaData("JobLocationAttributeTypeID", SqlDbType.NVarChar, 100),
                                                                   new SqlMetaData("Value", SqlDbType.Int)
                                                                   });
                record.SetInt32(0, item.JobLocationID);
                record.SetString(1, item.JobLocationAttributeTypeID);
                record.SetInt32(2, item.Value);
                list.Add(record);
            }

            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@tvpJobLocationAttributes", list);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "dbo.JobLocationAttributeTableType";
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
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Updates the JobLocation record associated with oldJobLocation with data from newJobLocation
        /// </summary>
        /// <param name="oldJobLocation"></param>
        /// <param name="newJobLocation"></param>
        /// <returns></returns>
        public int UpdateJobLocation(JobLocation oldJobLocation, JobLocation newJobLocation)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_joblocation_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobLocationID", oldJobLocation.JobLocationID);
            cmd.Parameters.AddWithValue("@NewStreet", newJobLocation.Street);
            cmd.Parameters.AddWithValue("@OldStreet", oldJobLocation.Street);
            cmd.Parameters.AddWithValue("@NewCity", newJobLocation.City);
            cmd.Parameters.AddWithValue("@OldCity", oldJobLocation.City);
            cmd.Parameters.AddWithValue("@NewState", newJobLocation.State);
            cmd.Parameters.AddWithValue("@OldState", oldJobLocation.State);
            cmd.Parameters.AddWithValue("@NewZipCode", newJobLocation.ZipCode);
            cmd.Parameters.AddWithValue("@OldZipCode", oldJobLocation.ZipCode);
            cmd.Parameters.AddWithValue("@NewComments", newJobLocation.Comments);
            cmd.Parameters.AddWithValue("@OldComments", oldJobLocation.Comments);
            cmd.Parameters.AddWithValue("@NewActive", newJobLocation.Active);
            cmd.Parameters.AddWithValue("@OldActive", oldJobLocation.Active);



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
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the JobLocationAttributes of a JobLocation with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByJobLocationID(int id)
        {
            var list = new List<JobLocationAttribute>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocationattribute_list_by_joblocationid";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobLocationID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new JobLocationAttribute()
                        {
                            JobLocationID = reader.GetInt32(0),
                            JobLocationAttributeTypeID = reader.GetString(1),
                            Value = reader.GetInt32(2)
                        };

                        list.Add(item);
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

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the Attributes associated with a service package given by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeListByServicePackageID(int id)
        {
            var list = new List<JobLocationAttribute>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocationattributetypeid_list_by_servicepackage_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ServicePackageID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new JobLocationAttribute()
                        {
                            JobLocationAttributeTypeID = reader.GetString(0),
                            Value = 0
                        };

                        list.Add(item);
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

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a specific JObLocation record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocation RetrieveJobLocationByID(int id)
        {
            JobLocation jobLocation = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_joblocation_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobLocationID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    jobLocation = new JobLocation()
                    {
                        JobLocationID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        Street = reader.GetString(2),
                        City = reader.GetString(3),
                        State = reader.GetString(4),
                        ZipCode = reader.GetString(5),
                        Comments = reader.GetString(6),
                        Active = reader.GetBoolean(7)
                    };
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


            return jobLocation;
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Creates a JobLocationDetail pulling data based on the given job location record id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JobLocationDetail RetrieveJobLocationDetailByID(int id)
        {
            JobLocationDetail detail = null;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_joblocation_detail_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobLocationID", id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    var jobLocation = new JobLocation()
                    {
                        JobLocationID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        Street = reader.GetString(2),
                        City = reader.GetString(3),
                        State = reader.GetString(4),
                        ZipCode = reader.GetString(5),
                        Comments = reader.GetString(6),
                        Active = reader.GetBoolean(7)
                    };

                    reader.NextResult();
                    List<JobLocationAttribute> attributeList = new List<JobLocationAttribute>(); ;
                    while (reader.Read())
                    {
                        var attribute = new JobLocationAttribute()
                        {
                            JobLocationID = reader.GetInt32(0),
                            JobLocationAttributeTypeID = reader.GetString(1),
                            Value = reader.GetInt32(2)
                        };

                        attributeList.Add(attribute);
                    }

                    detail = new JobLocationDetail()
                    {
                        JobLocation = jobLocation,
                        JobLocationAttributes = attributeList

                    };


                }
                
                
            }
            catch (Exception)
            {

                throw;
            }

            return detail;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the JobLocations associated with a customer given by the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JobLocation> RetrieveJobLocationListByCustomerID(int id)
        {
            var list = new List<JobLocation>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocation_list_by_customer_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", id);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new JobLocation()
                        {
                            JobLocationID = reader.GetInt32(0),
                            CustomerID = reader.GetInt32(1),
                            Street = reader.GetString(2),
                            City = reader.GetString(3),
                            State = reader.GetString(4),
                            ZipCode = reader.GetString(5),
                            Comments = reader.GetString(6),
                            Active = reader.GetBoolean(7)
                        };

                        list.Add(item);
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

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of JobLocationDetail records
        /// </summary>
        /// <returns></returns>
        public List<JobLocationDetail> RetrieveJobLocationDetailList()
        {
            var list = new List<JobLocationDetail>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocation_detail_list";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            /*[JobLocation].[JobLocationID], [JobLocation].[Street], [JobLocation].[City], [JobLocation].[State], 
				[JobLocation].[ZipCode], [JobLocation].[Comments], [JobLocation].[Active],
				[JobLocationAttribute].[JobLocationAttributeTypeID], [JobLocationAttribute].[Value] */
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    JobLocation location = null;
                    Customer customer = null;
                    List<JobLocationAttribute> attributes = new List<JobLocationAttribute>();
                    JobLocationDetail detail;
                    while (reader.Read())
                    {
                        
                        if (location == null)
                        {
                            location = new JobLocation {
                                JobLocationID = reader.GetInt32(0),
                                Street = reader.GetString(1),
                                City = reader.GetString(2),
                                State = reader.GetString(3),
                                ZipCode = reader.GetString(4),
                                Comments = reader.GetString(5),
                                Active = reader.GetBoolean(6)
                            };
                            customer = new Customer
                            {
                                CustomerID = reader.GetInt32(9),
                                CustomerTypeID = reader.GetString(10),
                                Email = reader.GetString(11),
                                FirstName = reader.GetString(12),
                                LastName = reader.GetString(13),
                                PhoneNumber = reader.GetString(14),
                                Active = reader.GetBoolean(15)
                            };
                            if (!reader.IsDBNull(7))
                            {
                                var attribute = new JobLocationAttribute
                                {
                                    JobLocationID = reader.GetInt32(0),
                                    JobLocationAttributeTypeID = reader.GetString(7),
                                    Value = reader.GetInt32(8)

                                };
                                attributes.Add(attribute);
                            }

                        }
                        else if(reader.GetInt32(0) == location.JobLocationID)
                        {
                            var attribute = new JobLocationAttribute {
                                JobLocationID = reader.GetInt32(0),
                                JobLocationAttributeTypeID = reader.GetString(7),
                                Value = reader.GetInt32(8)

                            };
                            attributes.Add(attribute);


                        }else if(reader.GetInt32(0) != location.JobLocationID)
                        {
                            detail = new JobLocationDetail {
                                JobLocation = location,
                                JobLocationAttributes = attributes,
                                Customer = customer

                            };
                            list.Add(detail);
                            location = new JobLocation
                            {
                                JobLocationID = reader.GetInt32(0),
                                Street = reader.GetString(1),
                                City = reader.GetString(2),
                                State = reader.GetString(3),
                                ZipCode = reader.GetString(4),
                                Comments = reader.GetString(5),
                                Active = reader.GetBoolean(6)
                            };
                            customer = new Customer
                            {
                                CustomerID = reader.GetInt32(9),
                                CustomerTypeID = reader.GetString(10),
                                Email = reader.GetString(11),
                                FirstName = reader.GetString(12),
                                LastName = reader.GetString(13),
                                PhoneNumber = reader.GetString(14),
                                Active = reader.GetBoolean(15)
                            };
                            attributes = new List<JobLocationAttribute>();
                            if (!reader.IsDBNull(7))
                            {
                                var attribute = new JobLocationAttribute
                                {
                                    JobLocationID = reader.GetInt32(0),
                                    JobLocationAttributeTypeID = reader.GetString(7),
                                    Value = reader.GetInt32(8)

                                };
                                attributes.Add(attribute);
                            }

                        }

                        
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

            return list;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Deactivates the JobLocation record with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeactivateJobLocationByID(int id)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_joblocation_by_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@JobLocationID", id);

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
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a default list of all JobLocationAttributes
        /// </summary>
        /// <returns></returns>
        public List<JobLocationAttribute> RetrieveJobLocationAttributeList()
        {
            List<JobLocationAttribute> attributes = new List<JobLocationAttribute>();

            //sp_retrieve_joblocationattributetype_list
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_joblocationattributetype_list";
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
                        var item = new JobLocationAttribute()
                        {
                            JobLocationAttributeTypeID = reader.GetString(0),
                            Value = 0
                        };

                        attributes.Add(item);
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

            return attributes;
        }
    }
}
