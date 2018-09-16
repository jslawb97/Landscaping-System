using System;
using System.Collections.Generic;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class VendorManager : IVendorManager
    {
        private IVendorAccessor _vendorAccessor;

        /// <summary>
        /// Manager Constructor for handling accessor dependency
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        public VendorManager()
        {
            _vendorAccessor = new VendorAccessor();
        }

        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// </remarks>
        /// <param name="vendorAccessor"></param>
        public VendorManager(IVendorAccessor vendorAccessor)
        {
            _vendorAccessor = vendorAccessor;
        }

        /// <summary>
        /// Calls the accessor to add a Vendor to the database
        /// </summary>
        /// <param name="vendor">The vendor to be added</param>
        /// <returns>True if successful, false otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// Last Updated 2018/03/05
        /// 
        /// Marshall Sejkora
        /// Updated 2018/04/27
        /// Added Validations
        /// </remarks>
        public bool CreateVendor(Vendor vendor)
        {
            if (!StringValidations.IsValidNamePropertyEmpty(vendor.Name) || !StringValidations.IsValidNamePropertyMaxSize(vendor.Name, 100))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(vendor.Rep) || !StringValidations.IsValidNamePropertyMaxSize(vendor.Rep, 100))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(vendor.Address) || !StringValidations.IsValidNamePropertyMaxSize(vendor.Address, 250))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(vendor.Website) || !StringValidations.IsValidNamePropertyMaxSize(vendor.Website, 250))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(vendor.Phone) || !StringValidations.IsValidPhoneNumber(vendor.Phone) && !IntegerValidations.IsValidNumber(vendor.Phone))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }
            if (!IntegerValidations.IsNonNegativeNumber(vendor.Phone))
            {
                throw new ArgumentOutOfRangeException("Invalide data");
            }

            var result = false;
            try
            {
                result = _vendorAccessor.CreateVendor(vendor);
            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }

        /// <summary>
        /// Edits an existing Vendor with new Vendor data
        /// </summary>
        /// <param name="oldVendor">The vendor being edited</param>
        /// <param name="newVendor">The vendor with the new data</param>
        /// <returns>True if the edit is successful, and False if it is not</returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// Last Updated 2018/03/05
        /// 
        /// 
        /// Marshall Sejkora
        /// Updated 2018/04/27
        /// Added Validations
        /// </remarks>
        public bool EditVendor(Vendor oldVendor, Vendor newVendor)
        {
            var result = false;
            if (!StringValidations.IsValidNamePropertyEmpty(newVendor.Name) || !StringValidations.IsValidNamePropertyMaxSize(newVendor.Name, 100))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(newVendor.Rep) || !StringValidations.IsValidNamePropertyMaxSize(newVendor.Rep, 100))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(newVendor.Address) || !StringValidations.IsValidNamePropertyMaxSize(newVendor.Address, 250))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(newVendor.Website) || !StringValidations.IsValidNamePropertyMaxSize(newVendor.Website, 250))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }
            if (!StringValidations.IsValidNamePropertyEmpty(newVendor.Phone) || !StringValidations.IsValidPhoneNumber(newVendor.Phone) && !IntegerValidations.IsValidNumber(newVendor.Phone))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }
            if (!IntegerValidations.IsNonNegativeNumber(newVendor.Phone))
            {
                throw new ArgumentOutOfRangeException("Invalid data");
            }

            try
            {
                result = _vendorAccessor.EditVendor(oldVendor, newVendor);
            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }

        /// <summary>
        /// Created by John Miller
        /// Last Updated 2018/03/05
        /// Deactivates a Vendor in the database
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool DeactivateVendorByID(int vendorID)
        {
            var result = false;

            try
            {
                result = _vendorAccessor.DeactivateVendorByID(vendorID);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of Vendor objects from VendorAccessor class
        /// </summary>
        /// <returns>A list of Vendor objects</returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// Last Updated 2018/03/05
        /// </remarks>
        public List<Vendor> RetrieveVendorList()
        {
            var vendors = new List<Vendor>();

            try
            {
                vendors = _vendorAccessor.RetrieveVendorList();
            }
            catch (Exception)
            {
                throw;
            }
            return vendors;
        }

        /// <summary>
        /// Retrieves a list of active Vendor objects from VendorAccessor class 
        /// </summary>
        /// <returns>A list of active Vendor objects</returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// Last Updated 2018/03/05
        /// </remarks>
        public List<Vendor> RetrieveVendorListByActive()
        {

            var vendors = new List<Vendor>();

            try
            {
                vendors = _vendorAccessor.RetrieveVendorByActive();
            }
            catch (Exception)
            {
                throw;
            }
            return vendors;
        }

        /// <summary>
        /// Retrieves a Vendor by its ID from the VendorAccessor class 
        /// </summary>
        /// <returns>A Vendor object with the given id</returns>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/01
        /// Last Updated 2018/03/05
        /// </remarks>
        public Vendor RetrieveVendorByID(int? id)
        {
            var vendor = new Vendor();

            try
            {
                vendor = _vendorAccessor.RetrieveVendorByID(id);
            }
            catch (Exception)
            {
                throw;
            }
            return vendor;
        }

    }
}
