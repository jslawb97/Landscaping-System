using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ISpecialOrderManager
    { 
        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Adds a special order to the database
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        int CreateSpecialOrder(SpecialOrder specialOrder);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Deletes a special order from the database
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        int DeleteSpecialOrderByID(int specialOrderID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Edits a special order in the database
        /// </summary>
        /// <param name="newSpecialOrder"></param>
        /// <param name="oldSpecialOrder"></param>
        /// <returns></returns>
        int EditSpecialOrder(SpecialOrder newSpecialOrder, SpecialOrder oldSpecialOrder);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// that have the same employeeID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        List<SpecialOrder> RetrieveSpecialOrderByEmployeeID(int employeeID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// that have the same jobID
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<SpecialOrder> RetrieveSpecialOrderByJobID(int jobID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a special order from the database
        /// by the given specialOrderID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        SpecialOrder RetrieveSpecialOrderByID(int specialOrderID);

        List<SpecialOrder> RetrieveSpecialOrderByStatusID(string statusID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// </summary>
        /// <returns></returns>
        List<SpecialOrder> RetrieveSpecialOrders();

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// 
        /// Edits the status of the specified SpecialOrder record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        bool EditSpecialOrderSupplyStatusByID(int id, string oldStatus, string newStatus);
    }
}
