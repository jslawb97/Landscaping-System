using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;
using System.Data.SqlClient;

namespace Logic
{
    public class ServiceOfferingManager : IServiceOfferingManager
    {
        private IServiceOfferingAccessor _serviceOfferingAccessor;

        // Contructor for real run
        public ServiceOfferingManager()
        {
            _serviceOfferingAccessor = new ServiceOfferingAccessor();
        }

        // Constructor for unit tests and other implementations
        public ServiceOfferingManager(IServiceOfferingAccessor serviceOfferingAccessor)
        {
            _serviceOfferingAccessor = serviceOfferingAccessor;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/02/20
        /// 
        /// Retrieves list of service offering items 
        /// </summary>
        /// <returns></returns>
        public List<ServiceOffering> RetrieveServiceOfferingList()
        {
            List<ServiceOffering> serviceOfferings = null;

            try
            {
                serviceOfferings = _serviceOfferingAccessor.RetrieveServiceOfferingList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to connect to server.", ex);
            }

            return serviceOfferings;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        ///  Sends a ServiceOffering to createServiceOffering in ServiceOfferingAccessor
        ///  then returns result
        /// </summary>
        public int CreateServiceOffering(ServiceOffering serviceOffering)
        {
            var result = 0;

            try
            {
                result = _serviceOfferingAccessor.CreateServiceOffering(serviceOffering);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/22
        /// 
        ///  Sends old ServiceOffering data and the updated data to editServiceOffering in ServiceOfferingAccessor
        ///  then returns result
        /// </summary>
        public int EditServiceOffering(ServiceOffering oldServiceOffering, ServiceOffering newServiceOffering)
        {
            var result = 0;

            if (newServiceOffering.Name == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (newServiceOffering.Description == "")
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }

            try
            {
                result = _serviceOfferingAccessor.EditServiceOffering(oldServiceOffering, newServiceOffering);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/22
        /// Updated: 2018/04/20
        /// 
        /// Sends serviceOfferingID to DeactivateServiceOfferingByID in ServiceOfferingAccessor
        /// then returns result
        /// 
        /// Marshall Sejkora
        /// Updated: 2018/04/20
        /// Formatting to fit others and removal of duplicate
        /// <param name="serviceOfferingID">The ID of the service offering to be deleted</param>
        /// <exception cref="Exception">Delete fails</exception>
        /// </summary>
        public bool DeleteServiceOfferingByID(int serviceOfferingID)
        {
            var result = false;
            try
            {
                if (1 == _serviceOfferingAccessor.DeleteServiceOfferingByID(serviceOfferingID))
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
