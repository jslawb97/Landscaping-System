using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IServiceOfferingItemAccessor
    {
        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// Create a service offering item
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        int CreateServiceOfferingItem(ServiceOfferingItem serviceOfferingItem);

        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// retrieves a list of service offering items by it's id
        /// </summary>
        /// <param name="serviceOfferingID"></param>
        /// <returns></returns>
        List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceOfferingID(int serviceOfferingID);

        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// Retrieves a list of service offering items by the service item id
        /// </summary>
        /// <param name="serviceItemID"></param>
        /// <returns></returns>
        List<ServiceOfferingItem> RetrieveServiceOfferingItemsByServiceItemID(int serviceItemID);

        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// Delets a sercive offerint item
        /// </summary>
        /// <param name="serviceOfferingItem"></param>
        /// <returns></returns>
        int DeleteServiceOfferingItem(ServiceOfferingItem serviceOfferingItem);
    }
}
