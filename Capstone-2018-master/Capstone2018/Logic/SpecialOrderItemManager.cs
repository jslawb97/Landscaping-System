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
    /// Created: 2018/01/31
    /// 
    /// Implementation of ISpecialOrderItemManager to deal with Accessing data via Sql Server database.
    /// </summary>
    public class SpecialOrderItemManager : ISpecialOrderItemManager
    {

        private ISpecialOrderItemAccessor _specialOrderItemAccessor;

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Manager Constructor for handling SQLServer database accessor dependency
        /// </summary>
        public SpecialOrderItemManager()
        {
            _specialOrderItemAccessor = new SpecialOrderItemAccessor();
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Manager constructor for handling a given accessor dependency
        /// </summary>
        /// <param name="specialOrderItemAccessor">The accessor being handled</param>
        public SpecialOrderItemManager(ISpecialOrderItemAccessor specialOrderItemAccessor)
        {
            _specialOrderItemAccessor = specialOrderItemAccessor;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Calls the accessor to add a Special Order Item to the database
        /// </summary>
        /// <param name="newItem">The item to be added</param>
        /// <returns>The newly created items id</returns>
        public bool AddSpecialOrderItem(SpecialItem newItem)
        {

            validateFields(newItem);
            try
            {
                return  Constants.IDSTARTVALUE <= _specialOrderItemAccessor.CreateSpecialOrderItem(newItem);
            }
            catch (Exception)
            {

                throw new ApplicationException("Add failed");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/08
        /// 
        /// Deactivates a SpecialItem record by calling appropriate SQLServer database access method
        /// </summary>
        /// <param name="id">The id of the record to be deactivated</param>
        /// <returns></returns>
        public bool DeactivateSpecialOrderItem(int id)
        {

            if(id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID: ID should be no less than " + Constants.IDSTARTVALUE);
            }

            try
            {
                return 1 == _specialOrderItemAccessor.DeactivateSpecialOrderByID(id);
            }
            catch (Exception)
            {

                throw new ApplicationException("Deactivate failed");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Edits the old special order item with data from the new special order item.
        /// </summary>
        /// <param name="oldSpecialItem">The item being edited</param>
        /// <param name="newSpecialItem">The item with the new data</param>
        /// <returns>The number of rows affected</returns>
        public bool EditSpecialOrderItem(SpecialItem oldSpecialItem, SpecialItem newSpecialItem)
        {
            validateFields(newSpecialItem);

            try
            {
                return  1 == _specialOrderItemAccessor.EditSpecialOrderItem(oldSpecialItem, newSpecialItem);
            }
            catch (Exception)
            {

                throw new ApplicationException("Edit failed");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Retrieves a List of SpecialItem objects from an accessor class that gets them from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>Returns a List of SpecialItem objects</returns>
        public List<SpecialItem> RetrieveSpecialOrderItems()
        {
            var items = new List<SpecialItem>();

            try
            {
                items = _specialOrderItemAccessor.RetrieveSpecialOrderItemList();
            }
            catch (Exception)
            {

                throw;
            }

            return items;
        }

        public List<SpecialOrderItemDetail> RetrieveSpecialOrderItemDetail()
        {
            var detailList = new List<SpecialOrderItemDetail>();

            try
            {
                detailList = _specialOrderItemAccessor.RetrieveSpecialOrderItemDetails();
            }
            catch (Exception)
            {

                throw;
            }

            return detailList;
        }

        private void validateFields(SpecialItem specialItem)
        {
            if(specialItem == null)
            {
                throw new ArgumentNullException("Special Item cannot be null");
            }

            if(specialItem.Name.Length > Constants.MAX_SPECIAL_ITEM_NAME_LENGTH)
            {
                throw new ArgumentOutOfRangeException("Invalid Name: Name should be no greater than " + Constants.MAX_SPECIAL_ITEM_NAME_LENGTH + " characters");
            }

            if(specialItem.Name == "")
            {
                throw new ArgumentOutOfRangeException("Invalid Name: Name should not be empty");
            }
        }
    }
}
