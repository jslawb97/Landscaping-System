using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IServiceOfferingAccessor
    {
        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// Retrieves a list of ser ice offerings
        /// </summary>
        /// <returns></returns>
        List<ServiceOffering> RetrieveServiceOfferingList();

        /// <summary>
        /// Jayden Tollefson
        /// Created 5/4/2018
        /// 
        /// Deletes a service offering
        /// </summary>
        /// <param name="serviceOfferingID"></param>
        /// <returns></returns>
        int DeleteServiceOfferingByID(int serviceOfferingID);

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/22
        /// 
        /// Added Create and Edit methods for changing service offerings
        /// </summary>
        int CreateServiceOffering(ServiceOffering serviceOffering);
        int EditServiceOffering(ServiceOffering oldServiceOffering, ServiceOffering newServiceOffering);
        
    }
}
