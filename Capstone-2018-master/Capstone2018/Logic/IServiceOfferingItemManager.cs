using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IServiceOfferingItemManager
    {
        int CreateServiceOfferingItem(ServiceOfferingItem serviceOfferingItem);

        List<ServiceOfferingItem> RetrieveServiceOfferingItemByID(int serviceOfferingID);

        List<ServiceOfferingItem> RetrieveServiceOfferingItemByServiceItemID(int serviceItemID);

        int DeleteServiceOfferingItem(ServiceOfferingItem serviceOfferingItem);
    }
}
