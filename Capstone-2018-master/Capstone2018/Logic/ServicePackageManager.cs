using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created 2018/02/22
    /// 
    /// Manages logic for ServicePackage objects and their Accessors
    /// </summary>
    public class ServicePackageManager : IServicePackageManager
    {

        private IServicePackageAccessor _servicePackageAccessor;

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Initializes IServicePackageAccessor dependency
        /// </summary>
        /// <param name="servicePackageAccessor"></param>
        public ServicePackageManager(IServicePackageAccessor servicePackageAccessor)
        {
            _servicePackageAccessor = servicePackageAccessor;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Initializes the service package accessor to a concrete implementation 
        /// of the accessor that handles database transfers with SqlServer database
        /// </summary>
        public ServicePackageManager()
        {
            _servicePackageAccessor = new ServicePackageAccessor();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Managees the adding of service packages to a data store
        /// </summary>
        /// <param name="servicePackage"></param>
        /// <returns></returns>
        public bool AddServicePackage(ServicePackage servicePackage)
        {
            validateFields(servicePackage);
            try
            {
                return Constants.IDSTARTVALUE <= _servicePackageAccessor.CreateServicePackage(servicePackage);
            }
            catch (Exception)
            {

                throw new ApplicationException("Add failed");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Manages the editing of a ServicePackage record between a data store
        /// </summary>
        /// <param name="oldServicePackage"></param>
        /// <param name="newServicePackage"></param>
        /// <returns></returns>
        public bool EditServicePackage(ServicePackage oldServicePackage, ServicePackage newServicePackage)
        {

            validateFields(newServicePackage);
            try
            {
                return 1 == _servicePackageAccessor.EditServicePackage(oldServicePackage, newServicePackage);
            }
            catch (Exception)
            {

                throw new ApplicationException("Edit failed");
            }
        }

        private void validateFields(ServicePackage servicePackage)
        {
            if(servicePackage == null)
            {
                throw new ArgumentNullException("Package cannot be null");
            }

            if(servicePackage.Name == "")
            {
                throw new ArgumentOutOfRangeException("Name cannot be empty");
            }

            if(servicePackage.Name.Length > Constants.MAXNAMELENGTH)
            {
                throw new ArgumentOutOfRangeException("Name must be less than " + Constants.MAXNAMELENGTH + " characters");
            }

            if (servicePackage.Description == "")
            {
                throw new ArgumentOutOfRangeException("Description cannot be empty");
            }

            if (servicePackage.Description.Length > Constants.MAXDESCRIPTIONLENGTH)
            {
                throw new ArgumentOutOfRangeException("Description must be less than " + Constants.MAXDESCRIPTIONLENGTH + " characters");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/02/22
        /// 
        /// Deactivates a ServicePackage record by the supplied ID
        /// </summary>
        /// <param name="servicePackageID"></param>
        /// <returns></returns>
        public bool DeactivateServicePackage(int servicePackageID)
        {
            int result = 0;

            try
            {
                result = _servicePackageAccessor.DeactivateServicePackageByID(servicePackageID);
            }
            catch (Exception)
            {
                throw;
            }
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Gets a list of all the service package records
        /// </summary>
        /// <returns></returns>
        public List<ServicePackage> RetrieveServicePackageList()
        {
            var list = new List<ServicePackage>();

            try
            {
                list = _servicePackageAccessor.RetrieveServicePackageList();
            }
            catch (Exception)
            {

                throw;
            }


            return list;
        }
    }
}
