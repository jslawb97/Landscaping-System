using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    public interface IServiceItemAccessor
    {
        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/19
        /// 
        /// Deactivates specified Service Item by ServiceItemID
        /// </summary>
        /// <param name="ServiceItemID">The ServiceItemID</param>
        /// <returns>The number of records affected</returns>
        int DeactivateServiceItemByID(int serviceItemID);



        int EditServiceItemByID(ServiceItem oldServiceItem, ServiceItem newServiceItem);


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/3/8
        /// 
        /// Creates new ServiceItem
        /// </summary>
        /// <param name="serviceItem">The new Service item</param>
        /// <returns>The ID of the added  Service Item</returns>
        int CreateServiceItem(ServiceItem serviceItem);



        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        List<ServiceItem> RetrieveServiceItemList();

    }
}
