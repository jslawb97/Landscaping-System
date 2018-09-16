using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IServiceOfferingManager
    {
        List<ServiceOffering> RetrieveServiceOfferingList();

        bool DeleteServiceOfferingByID(int serviceOfferingID);

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
