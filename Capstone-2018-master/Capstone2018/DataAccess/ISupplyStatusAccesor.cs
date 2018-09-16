using System;
using System.Collections.Generic;
namespace DataAccess
{
    public interface ISupplyStatusAccessor
    {
        List<string> RetrieveSupplyStatusList();
    }
}
