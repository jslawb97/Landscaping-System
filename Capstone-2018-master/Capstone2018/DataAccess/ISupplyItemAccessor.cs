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
    /// Created: 2018/02/01
    /// 
    /// Public interface for SupplyItem transactions with a data store
    /// </summary>
    public interface ISupplyItemAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Gets a list of SupplyItems from a data store
        /// </summary>
        /// <returns>List of SupplyItem</returns>
        List<SupplyItem> RetrieveSupplyItemList();

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Adds a SupplyItem record to a data store
        /// </summary>
        /// <param name="supplyItem">The new SupplyItem</param>
        /// <returns>The ID of the newly created SupplyItem</returns>
        int CreateSupplyItem(SupplyItem supplyItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Submits updated data for a given SupplyItem to a data store
        /// </summary>
        /// <param name="oldSupplyItem">The SupplyItem being updated</param>
        /// <param name="newSupplyItem">The SupplyItem with the updated data.</param>
        /// <returns>the number of records affected</returns>
        int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Deactivates the SupplyItem with the given ID
        /// </summary>
        /// <param name="supplyItemID">The ID of the SupplyItem to be deactivated</param>
        /// <returns>The number of records affected</returns>
        int DeactivateSupplyItemByID(int supplyItemID);

        /// <summary>
        /// Weston Olund
        /// 2018/03/08
        /// Method to get data from database using stored procedure
        /// </summary>
        /// <returns></returns>
        List<SupplyItemDetail> RetrieveSupplyItemDetailList();
		List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailList();

		List<SupplyItemDetail> RetrieveItemsNeedingReorderNotOnOrderSupplyItemDetailList();
	}
}
