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
    public class CertificationAccessor : ICertificationAccessor
    {
        /// <summary>
        /// Weston Olund
        /// Created on 2018/01/26
        /// 
        /// Method to use stored procedure to get list of certificates from database
        /// </summary>
        /// <returns></returns>
        public List<Certification> RetrieveCertificationList()
        {
            var certificationList = new List<Certification>();
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_certification_list";
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
                        var certification = new Certification()
                        {
                            CertificationID = reader.GetInt32(0),
                            CertificationName = reader.GetString(1),
                            CertificationDescription = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };
                        certificationList.Add(certification);
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
            return certificationList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate a Certification by ID using a stored procedure
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns>Rows affected</returns>
        public int DeactivateCertificationByID(int certificationID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_certification_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CertificationID", SqlDbType.Int);
            cmd.Parameters["@CertificationID"].Value = certificationID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the Certification", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Weston Olund
        /// 
        /// Created on 2018/03/01
        /// Method to use stored procedure to add a certification
        /// </summary>
        /// <param name="certification"></param>
        /// <returns></returns>
        public int CreateCertification(Certification certification)
        {
            int newCertificationID = 0;
            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_create_certification";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", certification.CertificationName);
            cmd.Parameters.AddWithValue("@Description", certification.CertificationDescription);
            cmd.Parameters.AddWithValue("@Active", certification.Active);

            try
            {
                conn.Open();
                decimal id = (decimal)cmd.ExecuteScalar();
                newCertificationID = (int)id;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem adding your certification.", ex);
            }
            finally
            {
                conn.Close();
            }
            return newCertificationID;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to call stored procedure to edit a certification
        /// </summary>
        /// <param name="oldCertification"></param>
        /// <param name="newCertification"></param>
        /// <returns></returns>
        public int EditCertification(Certification oldCertification, Certification newCertification)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_certification";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NewName", newCertification.CertificationName);
            cmd.Parameters.AddWithValue("@OldName", oldCertification.CertificationName);
            cmd.Parameters.AddWithValue("@NewDescription", newCertification.CertificationDescription);
            cmd.Parameters.AddWithValue("@OldDescription", oldCertification.CertificationDescription);
            cmd.Parameters.AddWithValue("@CertificationID", newCertification.CertificationID);
            cmd.Parameters.AddWithValue("@OldActive", oldCertification.Active);
            cmd.Parameters.AddWithValue("@NewActive", newCertification.Active);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database access error.", ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to retrieve a certification by its id from database
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        public Certification RetrieveCertificationByID(int certificationID)
        {
            var cert = new Certification();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_certification_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CertificationID", certificationID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    cert = new Certification()
                    {
                        CertificationID = certificationID,
                        CertificationName = reader.GetString(0),
                        CertificationDescription = reader.GetString(1),
                        Active = reader.GetBoolean(2)
                    };
                }
                else
                {
                    throw new ApplicationException("Certification record not found");
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
            return cert;
        }
    }
}
