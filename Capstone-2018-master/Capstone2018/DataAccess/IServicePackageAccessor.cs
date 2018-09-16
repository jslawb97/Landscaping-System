using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Public interface for ServicePackage transactions with a data store
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/03/09
    /// </remarks>
    /// </summary>
    public interface IServicePackageAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Adds a ServicePackage record to a data store
        /// </summary>
        /// <param name="servicePackage"></param>
        /// <returns></returns>
        int CreateServicePackage(ServicePackage servicePackage);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Submits updated data for a ServicePackage to a data store
        /// </summary>
        /// <param name="oldServicePackage"></param>
        /// <param name="newServicePackage"></param>
        /// <returns></returns>
        int EditServicePackage(ServicePackage oldServicePackage, ServicePackage newServicePackage);

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/20
        /// 
        /// Deactivates a ServicePackage by and ID
        /// </summary>
        /// <param name="servicePackageID"></param>
        /// <returns></returns>
        int DeactivateServicePackageByID(int servicePackageID);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Retrieves all service packages from a data store.
        /// </summary>
        /// <returns></returns>
        List<ServicePackage> RetrieveServicePackageList();
    }
}
