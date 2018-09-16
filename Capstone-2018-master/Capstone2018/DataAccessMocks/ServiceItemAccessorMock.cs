using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class ServiceItemAccessorMock : IServiceItemAccessor
    {

        private List<ServiceItem> _serviceItem = new List<ServiceItem>();


        public ServiceItemAccessorMock()
        {
            _serviceItem.Add(new ServiceItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE,
                Name = "ServiceItemTestItem1",
                Description = "Lengthy text description here",
                Active = true
            });
            _serviceItem.Add(new ServiceItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE + 1,
                Name = "ServiceItemTestItem2",
                Description = "Lengthy text description here",
                Active = true
            });
            _serviceItem.Add(new ServiceItem()
            {
                ServiceItemID = Constants.IDSTARTVALUE + 2,
                Name = "ServiceItemTestItem3",
                Description = "Lengthy text description here",
                Active = true
            });
        }

        public int CreateServiceItem(ServiceItem serviceItem)
        {
            try {
                this._serviceItem.Add(serviceItem);
                return 1;
            } catch (Exception e) {
                return 0;
            }
        }

        /// <summary>
        /// Amanda Tampir
        /// Created on 2018/02/22
        /// 
        /// method to return mock data
        /// </summary>
        /// <returns>_serviceItem</returns>
        public List<ServiceItem> RetrieveServiceItemList()
        {
            return _serviceItem;
        }

        public ServiceItem TestRetreiveServiceItemByID(int serviceItemID)
        {
            return this._serviceItem.Find(serviceitem => serviceitem.ServiceItemID.Equals(serviceItemID));

            //same as above 
            //foreach (var serviceitem in this._serviceItem)
            //{

            //    if (serviceitem.ServiceItemID.Equals(serviceItemID))
            //    {
            //        return serviceitem;
            //    }
            // }
            //return null;

        }
        public int DeactivateServiceItemByID(int serviceItemID)
        {
            try
            {
                TestRetreiveServiceItemByID(serviceItemID).Active = false;

                return 1;
            } catch (Exception)
            {
                return 0;
            }          
           
        }

        public int EditServiceItemByID(ServiceItem oldServiceItem, ServiceItem newServiceItem)
        {
            var found = 0;

            this._serviceItem.ForEach(serviceitem =>
            {
                if (serviceitem == oldServiceItem)
                {
                    serviceitem.Name = newServiceItem.Name;
                    serviceitem.Description = newServiceItem.Description;
                    //serviceitem.ServiceOfferingID = newServiceItem.ServiceOfferingID;
                    found = 1;
                }
            });
            return found;
        }
    }
}
