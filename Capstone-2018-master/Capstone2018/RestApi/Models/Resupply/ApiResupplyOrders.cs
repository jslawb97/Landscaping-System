using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// A list of API related ResupplyOrders
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    public class ApiResupplyOrders
    {
        /// <summary>
        /// The list of ResupplyOrders
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        [DataMember]
        public IList<ApiResupplyOrder> ResupplyOrderList { get; set; } = new List<ApiResupplyOrder>();

        /// <summary>
        /// Adds an ApiResupplyOrder to the underlying list
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="resupplyOrder">The ApiResupplyOrder to add</param>
        public void AddOrder(ApiResupplyOrder resupplyOrder)
        {
            ResupplyOrderList.Add(resupplyOrder);
        }

        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public override string ToString()
        {
            try {
                if (ResupplyOrderList == null || ResupplyOrderList.Count < 1) {
                    return "";
                }
                
                var ser = new DataContractJsonSerializer(ResupplyOrderList.GetType());
               
                string output;
 
                using (var ms = new MemoryStream())
                {
                    ser.WriteObject(ms, ResupplyOrderList);
                    output = Encoding.UTF8.GetString(ms.ToArray());
                 }

                return output;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return "There was a problem representing your API response";
            }
        }
    }
}