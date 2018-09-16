using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{
    public interface IServiceItemManager
    {



        /// Amanda Tampir
        /// Created: 2018/03/08
        /// 
        /// add to service Item 
        /// <param name="serviceItem">The ServiceITem list</param>
        /// </summary>
        int AddServiceItem(ServiceItem serviceItem);

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/19
        /// 
        /// Deactivates specified Service Item by ServiceItemID
        /// </summary>
        /// <param name="ServiceItemID">The ServiceItemID</param>
        /// <returns>The number of records affected</returns>
        int DeactivateServiceItemByID(int ServiceItemID);


        int EditServiceItemByID(ServiceItem oldServiceItem, ServiceItem newServiceItem);

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Interface for ServiceItemManager Retreieve
        /// </summary>
        List<ServiceItem> RetrieveServiceItemList();
    }
}
