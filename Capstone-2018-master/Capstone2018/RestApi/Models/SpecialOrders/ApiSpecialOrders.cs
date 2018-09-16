using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RestApi.Models.SpecialOrders
{
    /// <summary>
    /// A list of API related SpecialOrders
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public class ApiSpecialOrders
    {
        /// <summary>
        /// The list of SpecialOrders
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        [DataMember]
        public IList<ApiSpecialOrder> SpecialOrderList { get; set; } = new List<ApiSpecialOrder>();

        /// <summary>
        /// Adds an ApiSpecialOrder to the underlying list
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="specialOrder">The ApiSpecialOrder to add</param>
        public void AddOrder(ApiSpecialOrder specialOrder)
        {
            SpecialOrderList.Add(specialOrder);
        }

        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public override string ToString()
        {
            try
            {
                if (SpecialOrderList == null || SpecialOrderList.Count < 1)
                {
                    return "";
                }

                var ser = new DataContractJsonSerializer(SpecialOrderList.GetType());

                string output;

                using (var ms = new MemoryStream())
                {
                    ser.WriteObject(ms, SpecialOrderList);
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