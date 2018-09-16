using DataObjects;

namespace RestApi.Models
{
    /// <summary>
    /// API related data about a Vendor
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/9/2018
    /// </remarks>
    public class ApiVendor
    {
        /// <summary>
        /// The ID of the underlying Vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public int VendorId { get; set; }

        /// <summary>
        /// The representative for this vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public string Rep { get; set; }

        /// <summary>
        /// The address of this vendor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        public string Address { get; set; }

        /// <summary>
        /// Empty constructor for serialization
        /// </summary>
        public ApiVendor()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/9/2018
        /// </remarks>
        /// <param name="vendor">The underlying Vendor object</param>
        public ApiVendor(Vendor vendor)
        {
            VendorId = vendor.VendorID;
            Rep = vendor.Rep;
            Address = vendor.Address;
        }
    }
}