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
    /// Created: 2018/01/31
    /// 
    /// Interface for interacting with SpecialItem objects.
    /// </summary>
    public interface ISpecialOrderItemManager
    {
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Gets a list of Special Items. 
        /// </summary>
        /// <returns>Returns a collection of SpecialItems</returns>
        List<SpecialItem> RetrieveSpecialOrderItems();

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Edits a Special Order Item from an old entry to a new entry
        /// </summary>
        /// <param name="oldSpecialItem">The item being edited</param>
        /// <param name="newSpecialItem">The item with the new data</param>
        /// <returns>The number of records affected</returns>
        bool EditSpecialOrderItem(SpecialItem oldSpecialItem, SpecialItem newSpecialItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Adds a special order item 
        /// </summary>
        /// <param name="newItem">The item to add</param>
        /// <returns>the id of the newly added item</returns>
        bool AddSpecialOrderItem(SpecialItem newItem);

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Deactivates a SpecialItem record by calling appropriate access method
        /// </summary>
        /// <param name="id">The id of the record to be deactivated</param>
        /// <returns>How many records were effected by the deactivate</returns>
        bool DeactivateSpecialOrderItem(int id);

        List<SpecialOrderItemDetail> RetrieveSpecialOrderItemDetail();
    }
}
