using DataObjects;

namespace RestApi.Models.Resupply
{
    /// <summary>
    /// Deatiled API information about the underlying ResupplyOrderLine
    /// </summary>
    /// <remarks>
    /// Zach Murphy
    /// Updated on 5/4/2018
    /// </remarks>
    public class ApiResupplyOrderLine
    {
        /// <summary>
        /// The ResupplyOrderLine ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public int ResupplyOrderLineId { get; set; }

        /// <summary>
        /// The SupplyItem ID
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public int SupplyItemId { get; set; }

        /// <summary>
        /// The quantity of items in this order
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public int Quantity { get; set; }

        /// <summary>
        /// The cost of the items in this order
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public decimal Price { get; set; }

        /// <summary>
        /// Empty constructor for JSON serialization
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        public ApiResupplyOrderLine()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Zach Murphy
        /// Updated on 5/4/2018
        /// </remarks>
        /// <param name="orderLineDetail">The underlying ResupplyOrderLineDetail</param>
        public ApiResupplyOrderLine(ResupplyOrderLineDetail orderLineDetail)
        {
            ResupplyOrderLineId = orderLineDetail.ResupplyOrderLineID;
            SupplyItemId = orderLineDetail.SupplyItemID;
            Quantity = orderLineDetail.Quantity;
            Price = orderLineDetail.Price;
        }
    }
}