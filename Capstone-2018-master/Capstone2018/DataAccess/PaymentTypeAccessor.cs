using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class PaymentTypeAccessor : IPaymentTypeAccessor
    {
        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Method to retrieve a list of PaymentTypes using a stored procedure
        /// </summary>
        /// <param name="active"></param>
        /// <returns>A list of PaymentTypes</returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is not working
        public List<PaymentType> RetrievePaymentTypeListByActive(bool active = true)
        {
            List<PaymentType> paymentTypeList = new List<PaymentType>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_paymenttype_by_active";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var paymentType = new PaymentType
                        {
                            PaymentTypeID = reader.GetString(0),
                            Description = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        };
                        paymentTypeList.Add(paymentType);
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

            return paymentTypeList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/19
        /// 
        /// Method to deactivate a PaymentType using a stored procedure
        /// </summary>
        /// <param name="paymentTypeID"></param>
        /// <returns>Rows affected</returns>
        public int DeactivatePaymentTypeByID(string paymentTypeID)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_deactivate_paymenttype_by_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaymentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@PaymentTypeID"].Value = paymentTypeID;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem deactivating the MakeModel", ex);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/20
        /// 
        /// Method to edit a PaymentType by PaymentTypeID
        /// </summary>
        /// <param name="oldPaymentType"></param>
        /// <param name="newPaymentType"></param>
        /// <returns>Rows affected</returns>
        public int EditPaymentTypeByID(PaymentType oldPaymentType, PaymentType newPaymentType)
        {
            var rowcount = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_edit_paymenttype_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PaymentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewDescription", SqlDbType.NVarChar, 1000);
            cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@PaymentTypeID"].Value = oldPaymentType.PaymentTypeID;
            cmd.Parameters["@NewDescription"].Value = newPaymentType.Description;
            cmd.Parameters["@OldDescription"].Value = oldPaymentType.Description;

            try
            {
                conn.Open();
                rowcount = cmd.ExecuteNonQuery();

                if (rowcount == 0)
                {
                    throw new ApplicationException("PaymentType edit failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem editing the payment type", ex);
            }
            finally
            {
                conn.Close();
            }

            return rowcount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/20/2018
        /// 
        /// Method to add a new Payment Type to the database
        /// </summary>
        /// <param name="PaymentTypeID"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public int CreatePaymentType(string paymentTypeID, string description)
        {
            int rowCount = 0;

            var conn = DBConnection.GetDBConnection();

            var cmdText = @"sp_create_payment_type";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PaymentTypeID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000);

            cmd.Parameters["@PaymentTypeID"].Value = paymentTypeID;
            cmd.Parameters["@Description"].Value = description;

            try
            {
                conn.Open();

                rowCount = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem adding your data:", ex);
            }

            return rowCount;
        }    
    
    }
}
