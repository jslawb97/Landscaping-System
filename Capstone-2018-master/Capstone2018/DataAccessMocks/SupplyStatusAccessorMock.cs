using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class SupplyStatusAccessorMock : ISupplyStatusAccessor
    {
        private List<string> supplyStatusList = new List<string>();
        public SupplyStatusAccessorMock()
        {
            
            supplyStatusList.Add("ValidStatus1");
            supplyStatusList.Add("ValidStatus2");
        }

        public List<string> RetrieveSupplyStatusList()
        {
            return supplyStatusList;
        }
    }
}
