using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISpecialOrderLineAccessor
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Creates a new Special Order Line
        /// </summary>
        /// <param name="specialOrderLine"></param>
        /// <returns></returns>
        int CreateSpecialOrderLine(SpecialOrderLine specialOrderLine);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Retrieves all Special Order Lines for a specific Special Order
        /// by ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        List<SpecialOrderLine> RetrieveSpecialOrderLineBySpecialOrderID(int specialOrderID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Retrieves a Special Order Line by ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        SpecialOrderLine RetrieveSpecialOrderLineByID(int specialOrderLineID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Updates a Special Order Line
        /// </summary>
        /// <param name="oldLine"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        int EditSpecialOrderLine(SpecialOrderLine oldLine, SpecialOrderLine newLine);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/23/2018
        /// 
        /// Deletes a Special Order Line by ID
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        int DeleteSpecialOrderLine(int specialOrderLineID);

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// 
        /// Gets a list of SpecialORderLineDetail objects whose SpecialOrderID is the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<SpecialOrderLineDetail> RetrieveSpecialOrderLineDetailListByOrderID(int id);

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// 
        /// updates the QtyReceived record of the given SpecialOrderLine record ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldRecieved"></param>
        /// <param name="newRecieved"></param>
        /// <returns></returns>
        int EditSpecialOrderLineQtyReceivedByID(int id, int oldRecieved, int newRecieved);

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// 
        /// Sets the QtyReceived field to the value of the Quantity field for SpecialOrderLines that have the SpecialOrderID of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(int id);
    }
}
