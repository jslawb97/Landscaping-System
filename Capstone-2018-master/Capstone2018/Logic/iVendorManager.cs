using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Interface for interacting with Vendor objects.
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/02/01
    /// </remarks>
    public interface IVendorManager
    {
        /// <summary>
        /// John Miller
        /// Updated 2018/02/01
        /// Gets a list of Vendors.
        /// </summary>
        /// <returns>a collection of Vendors</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        List<Vendor> RetrieveVendorList();

        /// <summary>
        /// John Miller
        /// Updated 2018/02/01
        /// Gets a list of active Vendors.
        /// </summary>
        /// <returns>a collection of Vendors</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/18
        /// </remarks>
        List<Vendor> RetrieveVendorListByActive();

        /// <summary>
        /// John Miller
        /// Updated 2018/03/02
        /// Retrieves a vendor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Vendor</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/23
        /// </remarks>
        Vendor RetrieveVendorByID(int? id);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/02
        /// 
        /// Adds a Vendor
        /// </summary>
        /// <param name="vendor">the Vendor to be added</param>
        /// <returns>True if Vendor is successfully added, False otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        bool CreateVendor(Vendor vendor);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/02
        /// 
        /// Edits an existing Vendor with new data
        /// </summary>
        /// <param name="oldVendor">The Vendor being edited</param>
        /// <param name="newVendor">The Vendor with the new data</param>
        /// <returns>True if Vendor is successfully edited, False otherwise</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        bool EditVendor(Vendor oldVendor, Vendor newVendor);

        /// <summary>
        /// John Miller
        /// Updated 2018/03/02
        /// Deactivates a Vendor by its ID
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns>True if successful, false if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/23
        /// </remarks>
        bool DeactivateVendorByID(int vendorID);
    }
}
