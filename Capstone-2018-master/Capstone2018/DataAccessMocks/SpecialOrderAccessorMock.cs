using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class SpecialOrderAccessorMock : ISpecialOrderAccessor
    {

        private List<SpecialOrder> _specialOrderList = new List<SpecialOrder>();

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Constructor that adds sample data to _specialOrderList
        /// for testing purposes
        /// </summary>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public SpecialOrderAccessorMock()
        {
            _specialOrderList.Add(new SpecialOrder()
            {
                SpecialOrderID = 1000001,
                EmployeeID = 1000001,
                JobID = 1000001,
                SupplyStatusID = "Shipped",
                Date = new DateTime(2018, 02, 27)
            });
            _specialOrderList.Add(new SpecialOrder()
            {
                SpecialOrderID = 1000002,
                EmployeeID = 1000002,
                JobID = 1000001,
                SupplyStatusID = "Not Shipped",
                Date = new DateTime(2018, 02, 27)
            });
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Adds a Special Order to _specialOrderList for testing
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public int CreateSpecialOrder(SpecialOrder specialOrder)
        {
            int rowCount;
            int closedJob = 1000003;
            int orderCount = _specialOrderList.Count;

            // need to figure out a way to tell if a job is closesd,
            // and if it should be on database layer or logic layer.
            if (specialOrder.JobID == closedJob)
            {
                throw new ApplicationException();
            }


            _specialOrderList.Add(specialOrder);


            if (_specialOrderList.Count > orderCount)
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Deletes a Special Order from _specialOrderList for testing
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public int DeleteSpecialOrderByID(int specialOrderID)
        {
            int rowCount;

            var orderToDelete = _specialOrderList.Find(order => order.SpecialOrderID == specialOrderID);

            if (_specialOrderList.Remove(orderToDelete))
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Replaces a Special Order in _specialOrderList to simulate
        /// editing for testing
        /// </summary>
        /// <param name="newSpecialOrder"></param>
        /// <param name="oldSpecialOrder"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public int EditSpecialOrder(SpecialOrder newSpecialOrder, SpecialOrder oldSpecialOrder)
        {
            int rowCount;

            SpecialOrder oldOrder = RetrieveSpecialOrderByID(oldSpecialOrder.SpecialOrderID);

            oldOrder = newSpecialOrder;
            
            _specialOrderList[_specialOrderList
                .FindIndex(order => order.JobID == oldOrder.JobID)] = oldOrder;

            if (_specialOrderList.Contains(oldOrder))
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;

        }

        public int EditSpecialOrderSupplyStatusByID(int id, string oldStatus, string newStatus)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from _specialOrderList
        /// made by the same employee for testing
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByEmployeeID(int employeeID)
        {
            if (!_specialOrderList.Any(order => order.EmployeeID == employeeID))
            {
                throw new ApplicationException();
            }

            return _specialOrderList.FindAll(order => order.EmployeeID == employeeID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a Special Order from _specialOrderList
        /// using its unique Special Order ID for testing
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public SpecialOrder RetrieveSpecialOrderByID(int specialOrderID)
        {
            if (!_specialOrderList.Any(order => order.SpecialOrderID == specialOrderID))
            {
                throw new ApplicationException();
            }

            return _specialOrderList.Find(order => order.SpecialOrderID == specialOrderID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from 
        /// _specialOrderList for the same job for testing
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByJobID(int jobID)
        {
            if (!_specialOrderList.Any(order => order.JobID == jobID))
            {
                throw new ApplicationException();
            }

            return _specialOrderList.FindAll(order => order.JobID == jobID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a list of Special Orders from
        /// _specialOrderList with the same status for testing
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrderByStatusID(string statusID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all Special Orders in _specialOrderList
        /// for testing
        /// </summary>
        /// <returns></returns>
        ///  QA Shilin Xiong 4/27/2018  test past and the add ,edit,list feature is working
        public List<SpecialOrder> RetrieveSpecialOrders()
        {
            if (_specialOrderList.Count == 0)
            {
                throw new ApplicationException();
            }

            return _specialOrderList;
        }

    }
}
