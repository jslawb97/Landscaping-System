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
    public class EmployeeCertificationAccessor : IEmployeeCertificationAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate an EmployeeCertification by ID
        /// using a stored procedure
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        public int DeactivateEmployeeCertificationByID(int employeeID, int certificationID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_employeecertification";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@CertificationID", SqlDbType.Int);
            
            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@CertificationID"].Value = certificationID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the EmployeeCertification", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    

        /// <summary>
        /// Mike Mason
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of Employee Certifications
        /// </summary>
		/// <remark> QA Jayden T 3/30/18 Updated Cammand text</remark>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public List<EmployeeCertificationDetail> RetrieveEmployeeCertificationList()
        {
            var employeeCertificationDetail = new List<EmployeeCertificationDetail>();

            // Start with a SQL Connection
            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_retrieve_employeecertificationdetails_list";


            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


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
                        var aEmployeeCertification = new EmployeeCertificationDetail()
                        {
                            Employee = new Employee
                            {
                                EmployeeID = reader.GetInt32(0),
                                Email = reader.GetString(1)
                            },

                            Certification = new Certification
                            {
                                CertificationID = reader.GetInt32(2),
                                CertificationName = reader.GetString(3)
                            },

                            EndDate = reader.GetDateTime(4),
                            Active = reader.GetBoolean(5)


                        };
                        // don't leave the loop iteration without saving
                        employeeCertificationDetail.Add(aEmployeeCertification);
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

            return employeeCertificationDetail;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Calls a stored to procedure to add a EmployeeCertification record
        /// </summary>
        /// <param name="employeeCertification"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public int CreateEmployeeCertification(EmployeeCertification employeeCertification)
        {
            int newId = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_employee_certification";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CertificationID", employeeCertification.CertificationID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeCertification.EmployeeID);
            cmd.Parameters.AddWithValue("@EndDate", employeeCertification.EndDate);
            cmd.Parameters.AddWithValue("@Active", employeeCertification.Active);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            return newId;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Calls a stored to procedure to edit a EmployeeCertification record
        /// </summary>
        /// <param name="oldEmployeeCertification"></param>
        /// <param name="newEmployeeCertification"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public int EditEmployeeCertification(EmployeeCertification oldEmployeeCertification, EmployeeCertification newEmployeeCertification)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_employee_certification_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewCertificationID", newEmployeeCertification.CertificationID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newEmployeeCertification.EmployeeID);
            cmd.Parameters.AddWithValue("@NewEndDate", newEmployeeCertification.EndDate);
            cmd.Parameters.AddWithValue("@NewActive", newEmployeeCertification.Active);

            cmd.Parameters.AddWithValue("@OldCertificationID", oldEmployeeCertification.CertificationID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldEmployeeCertification.EmployeeID);
            cmd.Parameters.AddWithValue("@OldEndDate", oldEmployeeCertification.EndDate);
            cmd.Parameters.AddWithValue("@OldActive", oldEmployeeCertification.Active);

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
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Calls a stored to procedure to retrieve a EmployeeCertification 
        /// record by its ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public EmployeeCertification RetrieveEmployeeCertificationByID(int employeeID, int certificationID)
        {
            EmployeeCertification employeeCert = null;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_employeecertification_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CertificationID", certificationID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    employeeCert = new EmployeeCertification()
                    {
                        CertificationID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        EndDate = reader.GetDateTime(2),
                        Active = reader.GetBoolean(3)
                    };
                }
                else
                {
                    throw new ApplicationException("No data found.");
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
            return employeeCert;
        }
    }
}
