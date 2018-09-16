using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class ServiceOfferingItemAccessorMock : IServiceOfferingItemAccessor
    {
        private List<ServiceOfferingItem> _serviceOfferingItems = new List<ServiceOfferingItem>();

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/04/06
        /// 
        /// Mock constructor to add data to lists of service items
        /// and service offerings.
        /// </summary>
        public ServiceOfferingItemAccessorMock()
        {
            _serviceOfferingItems.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                ServiceOfferingID = Constants.IDSTARTVALUE
            });
            _serviceOfferingItems.Add(new ServiceOfferingItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE + 1,
                ServiceOfferingID = Constants.IDSTARTVALUE + 1
            });
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Mock method to add a new service offering item.
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        public int CreateServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            int result = 0;
            _serviceOfferingItems.Add(serviceOfferingItem);
            if (_serviceOfferingItems.Contains(serviceOfferingItem))
            {
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Mock method to delete an existing service offering item.
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        public int DeleteServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            int result = 0;
            bool existed = _serviceOfferingItems.Remove(_serviceOfferingItems.Find(o => o.ServiceOfferingID == serviceOfferingItem.ServiceOfferingID));

            if (_serviceOfferingItems.Contains(_serviceOfferingItems.Find(o => o.ServiceOfferingID == serviceOfferingItem.ServiceOfferingID)) == false && existed == true)
            {
                result = 1;
            }
            return result;
        }

        public List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceItemID(int serviceItemID)
        {
            List<ServiceOfferingItem> offerings = new List<ServiceOfferingItem>();

            offerings = _serviceOfferingItems.FindAll(p => p.ServiceItemID == serviceItemID);

            return offerings;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Mock method to retrieve existing service offering items by service offering id.
        /// </summary>
        /// <param name="serviceOfferingID"></param>
        /// <returns></returns>
        public List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceOfferingID(int serviceOfferingID)
        {
            List<ServiceOfferingItem> offerings = new List<ServiceOfferingItem>();

            offerings = _serviceOfferingItems.FindAll(p => p.ServiceOfferingID == serviceOfferingID);

            return offerings;
        }
    }
}
