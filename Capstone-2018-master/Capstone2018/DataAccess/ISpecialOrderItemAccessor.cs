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
    /// Created: 2018/01/31
    /// 
    /// Public interface for handling access to Special Order Item data
    /// </summary>
    public interface ISpecialOrderItemAccessor
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Retrieves a list of special order items
        /// </summary>
        /// <returns>A list of special order items from the database</returns>
        List<SpecialItem> RetrieveSpecialOrderItemList();

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Edits a specified special order item with data from a new special order item
        /// </summary>
        /// <param name="oldSpecialItem">The item being edited</param>
        /// <param name="newSpecialItem">The item containing the new data</param>
        /// <returns>The number of records affected</returns>
        int EditSpecialOrderItem(SpecialItem oldSpecialItem, SpecialItem newSpecialItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Creates a new special order item.
        /// </summary>
        /// <param name="newItem">The new special order item</param>
        /// <returns>The ID of the newly added special order item</returns>
        int CreateSpecialOrderItem(SpecialItem newItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/8
        /// 
        /// Deactivate a SpecialItem record in a data store by its ID field
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeactivateSpecialOrderByID(int id);

        /// <summary>
        /// Reuben Cassell
        /// Created 4/20/2018
        /// 
        /// Retrieves a list of SpecialOrderITemDetails
        /// </summary>
        /// <returns></returns>
        List<SpecialOrderItemDetail> RetrieveSpecialOrderItemDetails();
    }
}
