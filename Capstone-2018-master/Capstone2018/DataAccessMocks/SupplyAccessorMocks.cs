using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class SupplyAccessorMocks : ISupplyItemAccessor
    {
        List<SupplyItem> _supplyItems = new List<SupplyItem>();
        List<SupplyItemDetail> _supplyItemDetails = new List<SupplyItemDetail>();
        public SupplyAccessorMocks()
        {
            SupplyItem supplyItem = new SupplyItem();
            supplyItem.SupplyItemID = Constants.IDSTARTVALUE;
            supplyItem.Name = "Valid_Supply_Name";
            supplyItem.Description = "Valid_Description";
            supplyItem.Location = "Valid Location";
            supplyItem.QuantityInStock = 50;
            supplyItem.ReorderLevel = 75;
            supplyItem.ReorderQuantity = 50;
            supplyItem.Active = true;
            _supplyItems.Add(supplyItem);

            SupplyItemDetail supplyItemDetail = new SupplyItemDetail();
            supplyItemDetail.PriceEach = 100.0M;
            supplyItemDetail.VendorName = "Valid_Vendor_Name";
            supplyItemDetail.VendorID = Constants.IDSTARTVALUE;
            supplyItemDetail.SourceID = Constants.IDSTARTVALUE;
            supplyItemDetail.SupplyItem = supplyItem;


            supplyItem = new SupplyItem();
            supplyItem.SupplyItemID = Constants.IDSTARTVALUE + 1;
            supplyItem.Name = "Valid_Supply_Name2";
            supplyItem.Description = "Valid_Description2";
            supplyItem.Location = "Valid_Location2";
            supplyItem.QuantityInStock = 100;
            supplyItem.ReorderLevel = 125;
            supplyItem.ReorderQuantity = 200;
            supplyItem.Active = true;
            _supplyItems.Add(supplyItem);

            supplyItemDetail = new SupplyItemDetail();
            supplyItemDetail.PriceEach = 100.0M;
            supplyItemDetail.VendorName = "Valid_Vendor_Name";
            supplyItemDetail.VendorID = Constants.IDSTARTVALUE + 1;
            supplyItemDetail.SourceID = Constants.IDSTARTVALUE + 1;
            supplyItemDetail.SupplyItem = supplyItem;
        }


        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Mock create SupplyItem record
        /// </summary>
        /// <param name="supplyItem"></param>
        /// <returns></returns>
        public int CreateSupplyItem(SupplyItem supplyItem)
        {
            return 1000000;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Mock deactivate SupplyItem record
        /// </summary>
        /// <param name="supplyItemID"></param>
        /// <returns></returns>
        public int DeactivateSupplyItemByID(int supplyItemID)
        {
            return 1;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Mock edit SupplyItem record
        /// </summary>
        /// <param name="oldSupplyItem"></param>
        /// <param name="newSupplyItem"></param>
        /// <returns></returns>
        public int EditSupplyItem(SupplyItem oldSupplyItem, SupplyItem newSupplyItem)
        {
            return 1;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Mock get list of SupplyItem records
        /// </summary>
        /// <returns></returns>
        public List<SupplyItem> RetrieveSupplyItemList()
        {
            return new List<SupplyItem>();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/03/08
        /// Method to return mock data
        /// </summary>
        /// <returns></returns>
        public List<SupplyItemDetail> RetrieveSupplyItemDetailList()
        {
            return _supplyItemDetails;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/27
        /// Method to return mock data
        /// </summary>
        /// <returns></returns>
		public List<SupplyItemDetail> RetrieveItemsNeedingReorderSupplyItemDetailList()
		{
            return _supplyItemDetails;
		}

		public List<SupplyItemDetail> RetrieveItemsNeedingReorderNotOnOrderSupplyItemDetailList()
		{
			throw new NotImplementedException();
		}
	}
}
