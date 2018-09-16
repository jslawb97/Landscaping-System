using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;
namespace Logic
{
    public class ServiceItemManager : IServiceItemManager
    {

        private IServiceItemAccessor _serviceItemAccessor;


        /// Amanda Tampir
        /// Created: 2018/02/16
        /// 
        /// default Manager Constructor for accessing accessor
        /// </summary>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public ServiceItemManager()
        {
            _serviceItemAccessor = new ServiceItemAccessor();
        }

        /// Amanda Tampir
        /// Created: 2018/02/16
        /// 
        /// Manager Constructor for accessing accessor
        /// </summary>
        /// <param name="serviceItemAccessor"> </param>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public ServiceItemManager(IServiceItemAccessor serviceItemAccessor)
        {
            _serviceItemAccessor = serviceItemAccessor;
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/19
        /// 
        /// Deactivates specified Service Item by ServiceItemID
        /// </summary>
        /// <param name="ServiceItemID">The ServiceItemID</param>
        /// <returns>The number of records affected</returns>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public int DeactivateServiceItemByID(int serviceItemID)
        {
            int result = 0;


            if (serviceItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID Number");
            }
            try
            {
                result = _serviceItemAccessor.DeactivateServiceItemByID(serviceItemID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Thing not deleted", ex);
            }

            return result;

        }


        public int EditServiceItemByID(ServiceItem oldServiceItem, ServiceItem newServiceItem)
        {
            int result = 0;

            try
            {
                result = _serviceItemAccessor.EditServiceItemByID(oldServiceItem, newServiceItem);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }



        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Method to retrieve the list of service items
        /// </summary>
        /// <returns></returns>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<ServiceItem> RetrieveServiceItemList()
        {
            List<ServiceItem> serviceItemList = null;

            try
            {
                serviceItemList = _serviceItemAccessor.RetrieveServiceItemList();
            }
            catch (Exception)
            {
                throw;
            }
            return serviceItemList;
        }

        /// Amanda Tampir
        /// Created: 2018/03/08
        /// 
        /// add to service Item 
        /// <param name="serviceItem">The ServiceITem list</param>
        /// </summary>
        /// // QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public int AddServiceItem(ServiceItem serviceItem)
        {
            int id = 0;

            try
            {
                id = _serviceItemAccessor.CreateServiceItem(serviceItem);
            }
            catch (Exception)
            {

                throw;
            }

            return id;
        }





        //new deactivate method per jims requirements
        //public bool DeactivateServiceItem(int serviceItemID)
        //{
        //    if (serviceItemID < Constants.IDStartValue)
        //    {
        //        throw new ArgumentOutOfRangeException("Invalid ID Number");
        //    }

        //    try
        //    {
        //        return (1 == _serviceItemAccessor.DeactivateServiceItem(serviceItemID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("Thing not deleted", ex)
        //  }

        //}


















    }
}
