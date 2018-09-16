using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Interface for the logic functions regarding data from a data store
    /// </summary>
    public interface IServicePackageManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Manages the adding of a ServicePackage record with the data store
        /// </summary>
        /// <param name="servicePackage"></param>
        /// <returns></returns>
        bool AddServicePackage(ServicePackage servicePackage);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Manages the editing of a ServicePackage record with a data store
        /// </summary>
        /// <param name="oldServicePackage"></param>
        /// <param name="newServicePackage"></param>
        /// <returns></returns>
        bool EditServicePackage(ServicePackage oldServicePackage, ServicePackage newServicePackage);

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/20
        /// 
        /// Deactivates a ServicePackage by an ID
        /// </summary>
        /// <param name="servicePackageID"></param>
        /// <returns></returns>
        bool DeactivateServicePackage(int servicePackageID);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all the service packages
        /// </summary>
        /// <returns></returns>
        List<ServicePackage> RetrieveServicePackageList();
    }
}
