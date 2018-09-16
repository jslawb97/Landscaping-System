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
    /// Created: 2018/02/01
    /// 
    /// Public interface for the management of SupplyItem logic
    /// </summary>
    public interface ISupplyItemManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the getting of a list of SupplyItems
        /// </summary>
        /// <returns>List of SupplyItems</returns>
        List<SupplyItem> RetrieveSupplyItemList();

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the adding of a SupplyItem with a data store.
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add 
        /// </summary>
        /// <param name="supplyItem">The SupplyItem to be added</param>
        /// <returns>The ID of the newly created record</returns>
        bool CreateSupplyItem(SupplyItem supplyItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the editing of a SupplyItem with a data store
        /// </summary>
        /// <param name="oldSupplyItem">The SupplyItem being edited</param>
        /// <param name="newSupplyItem">The SupplyItem with the updated information</param>
        /// <returns>The number of records affected</returns>
        bool EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the deactivation of a SupplyItem record in a data store
        /// </summary>
        /// <param name="supplyItemID">The ID of the record to be deactivated</param>
        /// <returns>the number of record affected</returns>
        bool DeactivateSupplyItemByID(int supplyItemID);

        List<SupplyItemDetail> RetrieveSupplyItemDetailList();
		List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailList();
		List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailListNotOnReorder();
	}
}
