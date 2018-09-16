using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class ServiceOfferingItemManager : IServiceOfferingItemManager
    {
        private IServiceOfferingItemAccessor _serviceOfferingItemAccessor;

        // Contructor for real run
        public ServiceOfferingItemManager()
        {
            _serviceOfferingItemAccessor = new ServiceOfferingItemAccessor();
        }

        // Constructor for unit tests and other implementations
        public ServiceOfferingItemManager(IServiceOfferingItemAccessor serviceOfferingItemAccessor)
        {
            _serviceOfferingItemAccessor = serviceOfferingItemAccessor;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Creates a new ServiceOfferingItem
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        public int CreateServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            if (serviceOfferingItem.ServiceItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Item ID Value");
            }
            if (serviceOfferingItem.ServiceOfferingID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Offering ID Value");
            }
            int result = 0;

            try
            {
                result = _serviceOfferingItemAccessor.CreateServiceOfferingItem(serviceOfferingItem);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Deletes an existing ServiceOfferingItem
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        public int DeleteServiceOfferingItem(ServiceOfferingItem serviceOfferingItem)
        {
            if (serviceOfferingItem.ServiceItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Item ID Value");
            }
            if (serviceOfferingItem.ServiceOfferingID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Offering ID Value");
            }
            int result = 0;
            try
            {
                result = _serviceOfferingItemAccessor.DeleteServiceOfferingItem(serviceOfferingItem);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Returns a list of ServiceOfferingItems
        /// </summary>
        /// <param name="serviceOfferingID"></param>
        /// <returns></returns>
        public List<ServiceOfferingItem> RetrieveServiceOfferingItemByID(int serviceOfferingID)
        {
            if (serviceOfferingID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Offering ID Value");
            }
            List<ServiceOfferingItem> offerings = null;
            try
            {
                offerings = _serviceOfferingItemAccessor.RetrieveServiceOfferingItemsByServiceOfferingID(serviceOfferingID);
            }
            catch (Exception)
            {
                throw;
            }
            return offerings;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/06
        /// 
        /// Returns a list of ServiceOfferingItems from a service item id
        /// </summary>
        /// <param name="serviceItemID"></param>
        /// <returns></returns>
        public List<ServiceOfferingItem> RetrieveServiceOfferingItemByServiceItemID(int serviceItemID)
        {
            if (serviceItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Service Item ID Value");
            }
            List<ServiceOfferingItem> offerings = null;
            try
            {
                offerings = _serviceOfferingItemAccessor.RetrieveServiceOfferingItemsByServiceItemID(serviceItemID);
            }
            catch (Exception)
            {
                throw;
            }
            return offerings;
        }
    }
}
