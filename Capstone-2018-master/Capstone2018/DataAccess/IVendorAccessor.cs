using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Public interface for handling access to Vendor data
    /// </summary>
    /// <remarks>
    /// John Miller
    /// Updated 2018/02/01
    /// </remarks>
    public interface IVendorAccessor
    {
        List<Vendor> RetrieveVendorByActive();

        /// <summary>
        /// Retrieves a Vendor object from the SqlServer crlandscaping database by its VendorID
        /// </summary>
        /// <returns>A Vendor from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        Vendor RetrieveVendorByID(int? id);

        /// <summary>
        /// Retrieves a list of Vendor objects from the SqlServer crlandscaping database
        /// </summary>
        /// <returns>A list of Vendors from the database</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        List<Vendor> RetrieveVendorList();

        /// <summary>
        /// Sends data to edit an existing vendor in the database by VendorID
        /// </summary>
        /// <param name="OldVendor">The Vendor being edited</param>
        /// <param name="NewVendor">The Vendor with the new data</param>
        /// <returns>True if the update succeeded, and False if the update failed.</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        bool EditVendor(Vendor OldVendor, Vendor NewVendor);

        /// <summary>
        /// Sends data to create a new Vendor in the database
        /// </summary>
        /// <param name="vendor">The Vendor being added to the database</param>
        /// <returns>True if Vendor creation is successful, False if unsuccessful. </returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/02/01
        /// </remarks>
        bool CreateVendor(Vendor vendor);

        /// <summary>
        /// Deactivates the Vendor with the given VendorID
        /// </summary>
        /// <remarks>
        /// John Miller
        /// Created 2018/02/15
        /// </remarks>
        /// <param name="vendorID"></param>
        /// <returns>True if deactivation is successful, False if unsuccessful.</returns>
        bool DeactivateVendorByID(int vendorID);
    }
}
