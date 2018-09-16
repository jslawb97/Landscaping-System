using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    public class ServiceOfferingAccessorMockFail : IServiceOfferingAccessor
    {
        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/21
        /// 
        /// Method to retrieve service offerings.
        /// </summary>
        /// <returns></returns>
        public List<ServiceOffering> RetrieveServiceOfferingList()
        {
            throw new ApplicationException("Unable to connect to server.");
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/03/22
        /// 
        /// </summary>
        public int CreateServiceOffering(ServiceOffering serviceOffering)
        {
            throw new ApplicationException("Unable to connect to server.");
        }
        public int EditServiceOffering(ServiceOffering oldServiceOffering, ServiceOffering newServiceOffering)
        {
            throw new ApplicationException("Unable to connect to server.");
        }
        public int DeleteServiceOfferingByID(int serviceOfferingID)
        {
            throw new ApplicationException("Unable to connect to server.");
        }
    }
}
