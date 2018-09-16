using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SupplyStatusAccesor : DataAccess.ISupplyStatusAccessor
    {
        /// <summary>
        /// Weston Olund
        /// 2018/05/04
        /// Method to return all supply status from database
        /// </summary>
        /// <returns></returns>
        public List<String> RetrieveSupplyStatusList()
        {
            List<String> supplyStatusIDList = new List<string>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_supplystatus_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string supplyStatus = reader.GetString(0);
                        supplyStatusIDList.Add(supplyStatus);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("There was a problem retrieving your data");
            }
            finally
            {
                conn.Close();
            }
            return supplyStatusIDList;
        }
    }
}
