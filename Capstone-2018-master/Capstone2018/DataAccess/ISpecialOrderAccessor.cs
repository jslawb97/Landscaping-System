using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISpecialOrderAccessor
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Adds a Special Order to the database
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        int CreateSpecialOrder(SpecialOrder specialOrder);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Deletes a Special Order from the database
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        int DeleteSpecialOrderByID(int specialOrderID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Edits a Special Order in the database
        /// </summary>
        /// <param name="newSpecialOrder"></param>
        /// <param name="oldSpecialOrder"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        int EditSpecialOrder(SpecialOrder newSpecialOrder, SpecialOrder oldSpecialOrder);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from 
        /// the database made by the same employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        List<SpecialOrder> RetrieveSpecialOrderByEmployeeID(int employeeID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from 
        /// the database for the same job
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        List<SpecialOrder> RetrieveSpecialOrderByJobID(int jobID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a Special Order from the database
        /// using its unique Special Order ID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        SpecialOrder RetrieveSpecialOrderByID(int specialOrderID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from
        /// the database with the same status
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        List<SpecialOrder> RetrieveSpecialOrderByStatusID(string statusID);

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all Special Orders
        /// </summary>
        /// <returns></returns>
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
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
        /// QA Shilin Xiong 4/27/2018  test past and the add ,edit feature is working
        int EditSpecialOrderSupplyStatusByID(int id, string oldStatus, string newStatus);



    }
}
