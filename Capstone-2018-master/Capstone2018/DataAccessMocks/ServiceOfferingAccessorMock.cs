using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class ServiceOfferingAccessorMock : IServiceOfferingAccessor
    {

        private List<ServiceOffering> _serviceOfferings = new List<ServiceOffering>();

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/21
        /// 
        /// Mock constructor to add data to list of service offerings
        /// and service packages.
        /// </summary>
        public ServiceOfferingAccessorMock()
        {
            _serviceOfferings.Add(new ServiceOffering()
            {
                ServiceOfferingID = 1000000,
                ServicePackageID = 1000000,
                Name = "Tree Trimming",
                Description = "Cut loose branches down, make entire tree look pretty again."
            });
            _serviceOfferings.Add(new ServiceOffering()
            {
                ServiceOfferingID = 1000001,
                ServicePackageID = 1000001,
                Name = "Stump Grinding",
                Description = "Mulch the remaining stump to make the customer's yard beautiful again."
            });
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/21
        /// 
        /// Method to retrieve service offerings.
        /// </summary>
        /// <returns></returns>
        public List<ServiceOffering> RetrieveServiceOfferingList()
        {
            return _serviceOfferings;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/23
        /// 
        /// Method to create a service offering
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/26
        /// 
        /// Changed to make the method return whether or not it was added
        /// instead of an automatic addition
        /// </summary>
        public int CreateServiceOffering(ServiceOffering serviceOffering)
        {
            int result = 0;
            _serviceOfferings.Add(serviceOffering);
            if (_serviceOfferings.Contains(serviceOffering))
            {
                result = 1;
            }
            return result;
        }
        public int EditServiceOffering(ServiceOffering oldServiceOffering, ServiceOffering newServiceOffering)
        {
            return 1;
        }
        public int DeleteServiceOfferingByID(int serviceOfferingID)
        {
            return 1;
        }
    }
}
