using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class SpecialOrderManager : ISpecialOrderManager
    {
        ISpecialOrderAccessor _specialOrderAccessor;

        //For Real run
        public SpecialOrderManager()
        {
            _specialOrderAccessor = new SpecialOrderAccessor();
        }

        //For testing
        public SpecialOrderManager(ISpecialOrderAccessor specialOrderAccessor)
        {
            _specialOrderAccessor = specialOrderAccessor;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Adds a special order to the database
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        public int CreateSpecialOrder(SpecialOrder specialOrder)
        {
            int rowCount;

            try
            {
                validateSpecialOrder(specialOrder);

                rowCount = _specialOrderAccessor.CreateSpecialOrder(specialOrder);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Deletes a special order from the database
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public int DeleteSpecialOrderByID(int specialOrderID)
        {
            int rowCount;

            if (specialOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Special Order ID");
            }

            try
            {
                rowCount = _specialOrderAccessor.DeleteSpecialOrderByID(specialOrderID);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Edits a special order in the database
        /// </summary>
        /// <param name="newSpecialOrder"></param>
        /// <param name="oldSpecialOrder"></param>
        /// <returns></returns>
        public int EditSpecialOrder(SpecialOrder newSpecialOrder, SpecialOrder oldSpecialOrder)
        {
            int rowCount;

            try
            {

                validateSpecialOrder(newSpecialOrder);
                validateSpecialOrder(oldSpecialOrder);

                rowCount = _specialOrderAccessor.EditSpecialOrder(newSpecialOrder, oldSpecialOrder);
            }
            catch (Exception)
            {

                throw;
            }


            return rowCount;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        public bool EditSpecialOrderSupplyStatusByID(int id, string oldStatus, string newStatus)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (1 == _specialOrderAccessor.EditSpecialOrderSupplyStatusByID(id, oldStatus, newStatus))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// that have the same employeeID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<SpecialOrder> RetrieveSpecialOrderByEmployeeID(int employeeID)
        {
            List<SpecialOrder> specialOrderList = null;

            if (employeeID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Employee ID");
            }
            try
            {
                specialOrderList = _specialOrderAccessor
                    .RetrieveSpecialOrderByEmployeeID(employeeID);
            }
            catch (Exception)
            {

                throw;
            }

            return specialOrderList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves a special order from the database
        /// by the given specialOrderID
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public SpecialOrder RetrieveSpecialOrderByID(int specialOrderID)
        {
            SpecialOrder specialOrder = null;

            if (specialOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Special Order ID");
            }
            try
            {
                specialOrder = _specialOrderAccessor.RetrieveSpecialOrderByID(specialOrderID);
            }
            catch (Exception)
            {

                throw;
            }

            return specialOrder;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// that have the same jobID
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<SpecialOrder> RetrieveSpecialOrderByJobID(int jobID)
        {
            List<SpecialOrder> specialOrderList = null;

            if (jobID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Job ID");
            }
            try
            {
                specialOrderList = _specialOrderAccessor.RetrieveSpecialOrderByJobID(jobID);
            }
            catch (Exception)
            {

                throw;
            }

            return specialOrderList;
        }

        public List<SpecialOrder> RetrieveSpecialOrderByStatusID(string statusID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/26/2018
        /// 
        /// Retrieves all special orders from the database
        /// </summary>
        /// <returns></returns>
        public List<SpecialOrder> RetrieveSpecialOrders()
        {
            List<SpecialOrder> specialOrderList;

            try
            {
                specialOrderList = _specialOrderAccessor.RetrieveSpecialOrders();
            }
            catch (Exception)
            {

                throw;
            }

            return specialOrderList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 2/28/2018
        /// 
        /// Validates a special order to business rules
        /// </summary>
        /// <param name="specialOrder"></param>
        private void validateSpecialOrder(SpecialOrder specialOrder)
        {
            if (specialOrder.Date == null || specialOrder.SupplyStatusID == null)
            {
                throw new ArgumentOutOfRangeException("All fields must be filled.");
            }
            if (specialOrder.JobID < Constants.IDSTARTVALUE && specialOrder.JobID != null && specialOrder.JobID != 0)
            {
                throw new ArgumentOutOfRangeException("Invalid Job");
            }


        }
    }
}
