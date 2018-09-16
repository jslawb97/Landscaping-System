using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Mock ServicePackageAccessor
    /// </summary>
    public class ServicePackageAccessorMock : IServicePackageAccessor
    {
        private List<ServicePackage> packages = new List<ServicePackage>();

        public ServicePackageAccessorMock()
        {
            packages.Add(new ServicePackage()
            {
                ServicePackageID = 1000000,
                Name = "name",
                Description = "description",
                Active = true
            });
            packages.Add(new ServicePackage()
            {
                ServicePackageID = 1000001,
                Name = "name",
                Description = "description",
                Active = true
            });
            packages.Add(new ServicePackage()
            {
                ServicePackageID = 1000002,
                Name = "name",
                Description = "description",
                Active = true
            });
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Mock create ServicePackage record
        /// </summary>
        /// <param name="servicePackage"></param>
        /// <returns></returns>
        public int CreateServicePackage(ServicePackage servicePackage)
        {
            
            packages.Add(servicePackage);
            if(packages.Count > 0)
            {
                return 1000000;
            }
            else
            {
                return 0;
            }
            
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Mock edit ServicePackage record
        /// </summary>
        /// <param name="oldServicePackage"></param>
        /// <param name="newServicePackage"></param>
        /// <returns></returns>
        public int EditServicePackage(ServicePackage oldServicePackage, ServicePackage newServicePackage)
        {
            packages.Add(oldServicePackage);
            foreach(var item in packages)
            {
                if(item.ServicePackageID == oldServicePackage.ServicePackageID)
                {
                    item.Name = newServicePackage.Name;
                    item.Description = newServicePackage.Description;
                    item.Active = newServicePackage.Active;
                    return 1;
                }
                
            }
            return 0;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/22
        /// 
        /// Deactivates ServicePackage by an ID
        /// </summary>
        /// <param name="servicePackageID"></param>
        /// <returns></returns>
        public int DeactivateServicePackageByID(int servicePackageID)
        {
            int result = 0;

            foreach (ServicePackage servicePackage in packages)
            {
                if (servicePackage.ServicePackageID == servicePackageID)
                {
                    servicePackage.Active = false;
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets the list of all Service Packages
        /// </summary>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageList()
        {
            if (packages == null)
            {
                throw new ApplicationException("The list is null");

            }
            else
            {
                return packages;
            }
        }
    }
}
