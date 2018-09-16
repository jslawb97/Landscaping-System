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
    /// Created: 2018/02/01
    /// 
    /// Manages logic for SupplyItems and their Accessors
    /// </summary>
    public class SupplyItemManager : ISupplyItemManager
    {

        private ISupplyItemAccessor _supplyItemAccessor;

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Initializes ISupplyItemAccessor dependency
        /// </summary>
        public SupplyItemManager()
        {
            _supplyItemAccessor = new SupplyItemAccessor();
            

       
    }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/02
        /// </summary>
        /// <param name="supplyItemAccessor"></param>
        public SupplyItemManager(ISupplyItemAccessor supplyItemAccessor)
        {
            _supplyItemAccessor = supplyItemAccessor;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the adding of a SupplyItem to the data store
        /// 
        /// Jacob Conley
        /// Updated: 2018/05/01
        /// 
        /// Changed method name to create instead of add
        /// </summary>
        /// <param name="supplyItem">The SupplyItem to be added</param>
        /// <returns>The ID of the newly created record</returns>
        public bool CreateSupplyItem(SupplyItem supplyItem)
        {

            validateFields(supplyItem);
            try
            {
                return Constants.IDSTARTVALUE < _supplyItemAccessor.CreateSupplyItem(supplyItem);
            }
            catch (Exception)
            {

                throw new ApplicationException("Add Failed");
            }

            
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the deactivation of a SupplyItem record.
        /// </summary>
        /// <param name="supplyItemID">The ID of the record to be deactivated</param>
        /// <returns>The number of rows affected</returns>
        public bool DeactivateSupplyItemByID(int supplyItemID)
        {
            if(supplyItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID: ID must be no less than " + Constants.IDSTARTVALUE);
            }

            try
            {
                return 1 == _supplyItemAccessor.DeactivateSupplyItemByID(supplyItemID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the editing of a SupplyItem record.
        /// </summary>
        /// <param name="oldSupplyItem">The SupplyItem to be edited</param>
        /// <param name="newSupplyItem">The SupplyItem with the updated data</param>
        /// <returns>The number of records affected</returns>
        public bool EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            validateFields(newSupplyItem);
            try
            {
                return  1 == _supplyItemAccessor.EditSupplyItem(oldSupplyItem, newSupplyItem);
            }
            catch (Exception)
            {

                throw new ApplicationException("Edit Failed");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Manages the getting of a list of SupplyItems from the data store
        /// </summary>
        /// <returns>A list of every SupplyItem from the data store</returns>
        public List<SupplyItem> RetrieveSupplyItemList()
        {
            var items = new List<SupplyItem>();

            try
            {
                items = _supplyItemAccessor.RetrieveSupplyItemList();
            }
            catch (Exception)
            {

                throw;
            }

            return items;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// 
        /// Manages getting a supply item detail
        /// </summary>
        /// <returns></returns>
        public List<SupplyItemDetail> RetrieveSupplyItemDetailList()
        {
            try
            {
                return _supplyItemAccessor.RetrieveSupplyItemDetailList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void validateFields(SupplyItem supplyItem)
        {
            if(supplyItem == null)
            {
                throw new ArgumentNullException("The Supply Item cannot be null");
            }

            if (supplyItem.Name.Length > Constants.MAXNAMELENGTH)
            {
                throw new ArgumentOutOfRangeException("Invalid Name: Name cannot be more than " + Constants.MAXNAMELENGTH + " characters.");
            }
            if (supplyItem.Name == "")
            {
                throw new ArgumentOutOfRangeException("Invalid Name: Name cannot be empty");
            }

            if (supplyItem.Description.Length > Constants.MAXDESCRIPTIONLENGTH)
            {
                throw new ArgumentOutOfRangeException("Invalid Description: Description cannot be more than " + Constants.MAXDESCRIPTIONLENGTH + " characters.");
            }
            if (supplyItem.Description == "")
            {
                throw new ArgumentOutOfRangeException("Invalid Description: Description cannot be empty");
            }

            if (supplyItem.Location.Length > Constants.MAXNAMELENGTH)
            {
                throw new ArgumentOutOfRangeException("Invalid Location: Location cannot be more than " + Constants.MAXNAMELENGTH + " characters.");
            }
            if (supplyItem.Location == "")
            {
                throw new ArgumentOutOfRangeException("Invalid Location: Location cannot be empty");
            }
        }

		/// <summary>
		/// Weston Olund
		/// 2018/04/27
		/// Method to retrieve supply item details on order where quanity below reorder level
		/// </summary>
		/// <returns></returns>
		public List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailList()
		{
			try
			{
			return	_supplyItemAccessor.RetrieveItemsNeedingReorderSupplyItemDetailList();
			}
			catch (Exception)
			{
				throw new ApplicationException();
			}
		}

        /// <summary>
        /// Weston Olund
        /// Method to retrieve supply item details where not ordered and quanity below reorder level
        /// </summary>
        /// <returns></returns>
		public List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailListNotOnReorder()
		{
			try
			{
				return _supplyItemAccessor.RetrieveItemsNeedingReorderNotOnOrderSupplyItemDetailList();
			}
			catch (Exception)
			{
				throw new ApplicationException();
			}
		}
	}
}
