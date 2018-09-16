using System;
using System.Collections.Generic;
using DataObjects;
using DataAccess;


namespace DataAccessMocks
{
    public class VendorAccessorMock : IVendorAccessor
    {
        private List<Vendor> _vendorList = new List<Vendor>();

        /// <summary>
        /// John Miller
        /// Created 2018/02/22
        /// 
        /// Mock constructor to add data to the vendor list
        /// </summary>
        public VendorAccessorMock()
        {
            _vendorList.Add(new Vendor()
            {
                VendorID = 1000000,
                Name = "Supply Store",
                Rep = "John",
                Address = "123 Fake Street",
                Website = "www.supplies.com",
                Phone = "3193555455",
                Active = true
            });
            _vendorList.Add(new Vendor()
            {
                VendorID = 1000001,
                Name = "VendorZilla",
                Rep = "Jayden",
                Address = "543 E Avenue",
                Website = "www.VendorZilla.com",
                Phone = "5631234567",
                Active = true
            });
            _vendorList.Add(new Vendor()
            {
                VendorID = 1000002,
                Name = "Supplies R Us",
                Rep = "Zach",
                Address = "514 Supply Blvd",
                Website = "www.suppliesrus.com",
                Phone = "5157654987",
                Active = true
            });
        }

        /// <summary>
        /// Created by John Miller
        /// Last updated 2018/02/23
        /// 
        /// Gets all Vendors
        /// </summary>
        public List<Vendor> RetrieveVendorList()
        {
            return this._vendorList;
        }


        /// <summary>
        /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// 
        /// Edits a mock Vendor object.
        /// </summary>
        /// <param name="oldItem"></param>
        /// <param name="newItem"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool EditVendor(Vendor oldItem, Vendor newItem)
        {
            var found = 0;

            this._vendorList.ForEach(vendorList =>
            {
                if (vendorList == oldItem)
                {
                    vendorList.Name = newItem.Name;
                    vendorList.Rep = newItem.Rep;
                    vendorList.Address = newItem.Address;
                    vendorList.Website = newItem.Website;
                    vendorList.Phone = newItem.Phone;
                    vendorList.Active = newItem.Active;
                    found = 1;
                }
            });
            if (found == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// 
        /// Adds a new Vendor to the list.
        /// <returns>true if successful, false if unsuccessful</returns>
        /// </summary>
        public bool CreateVendor(Vendor newVendor)
        {
            try
            {
                this._vendorList.Add(newVendor);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// </summary>
        /// <returns>true if successful, false if unsuccessful</returns>
        public List<Vendor> RetrieveVendorByActive()
        {
            List<Vendor> activeVendors = new List<Vendor>();
            foreach (var item in _vendorList)
            {
                if (item.Active == true)
                {
                    activeVendors.Add(item);
                }
            }
            return activeVendors;
        }



        /// <summary>
        /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// 
        /// Adds a new Vendor item to the list
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool CreateVendorList(Vendor newItem)
        {
            try
            {
                this._vendorList.Add(newItem);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Vendor RetrieveVendorByID(int? id)
        {
            return this._vendorList.Find(vendorlist => vendorlist.VendorID.Equals(id));
        }

        /// <summary>
        /// Created by John Miller
        /// 2018/02/23
        /// Last Updated 2018/03/02
        /// 
        /// Deactivates a Vendor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool DeactivateVendorByID(int id)
        {
            try
            {
                RetrieveVendorByID(id).Active = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
