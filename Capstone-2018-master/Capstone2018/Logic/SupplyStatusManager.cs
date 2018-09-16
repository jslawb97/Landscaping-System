using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class SupplyStatusManager : ISupplyStatusManager
    {
        // Constructor for actual run
        ISupplyStatusAccessor _supplyStatusAccessor;

        public SupplyStatusManager()
        {
            _supplyStatusAccessor = new SupplyStatusAccesor();

        }

        // Constructor for unit tests
        public SupplyStatusManager(ISupplyStatusAccessor supplyStatusAccessor)
        {
            _supplyStatusAccessor = supplyStatusAccessor;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/05/04
        /// Method to return a list of supply status
        /// </summary>
        /// <returns></returns>
        public List<string> RetrieveSupplyStatusList()
        {
            try
            {
                return _supplyStatusAccessor.RetrieveSupplyStatusList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
