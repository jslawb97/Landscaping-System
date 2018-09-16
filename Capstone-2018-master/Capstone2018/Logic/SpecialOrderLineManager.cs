using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SpecialOrderLineManager : ISpecialOrderLineManager
    {

        private ISpecialOrderLineAccessor _specialOrderLineAccessor;

        // Real implementaton
        public SpecialOrderLineManager()
        {
            _specialOrderLineAccessor = new SpecialOrderLineAccessor();
        }

        // For testing
        public SpecialOrderLineManager(ISpecialOrderLineAccessor specialOrderLineAccessor)
        {
            _specialOrderLineAccessor = specialOrderLineAccessor;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Creates a new Special Order Line record using the SpecialOrderLineAccessor
        /// </summary>
        /// <param name="specialOrderLine"></param>
        /// <returns></returns>
        public int CreateSpecialOrderLine(SpecialOrderLine specialOrderLine)
        {
            int rowCount;

            try
            {
                validateSpecialOrderLine(specialOrderLine);

                rowCount = _specialOrderLineAccessor.CreateSpecialOrderLine(specialOrderLine);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Deletes a Special Order Line record by ID using the SpecialOrderLineAccessor
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        public int DeleteSpecialOrderLine(int specialOrderLineID)
        {
            int rowCount;

            try
            {
                validateID(specialOrderLineID);

                rowCount = _specialOrderLineAccessor.DeleteSpecialOrderLine(specialOrderLineID);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Edits an existing Special Order Line us SpecialOrderLineAccessor
        /// </summary>
        /// <param name="oldLine"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public int EditSpecialOrderLine(SpecialOrderLine oldLine, SpecialOrderLine newLine)
        {
            int rowCount;

            try
            {
                validateSpecialOrderLine(oldLine);
                validateSpecialOrderLine(newLine);

                rowCount = _specialOrderLineAccessor.EditSpecialOrderLine(oldLine, newLine);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Retrieves a specific SpecialOrderLine record by ID using SpecialOrderLineAccessor
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        public SpecialOrderLine RetrieveSpecialOrderLineByID(int specialOrderLineID)
        {
            SpecialOrderLine line = null;

            try
            {
                validateID(specialOrderLineID);

                line = _specialOrderLineAccessor.RetrieveSpecialOrderLineByID(specialOrderLineID);
            }
            catch (Exception)
            {

                throw;
            }

            return line;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Retrieves all Special Order Lines for a Special Order by ID using SpecialOrderLineAccessor
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public List<SpecialOrderLine> RetrieveSpecialOrderLineBySpecialOrderID(int specialOrderID)
        {
            List<SpecialOrderLine> lines = new List<SpecialOrderLine>();

            try
            {
                validateID(specialOrderID);

                lines = _specialOrderLineAccessor.RetrieveSpecialOrderLineBySpecialOrderID(specialOrderID);
            }
            catch (Exception)
            {

                throw;
            }

            return lines;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Validates a Special Order Line object before passing it to the database
        /// </summary>
        /// <param name="line"></param>
        private void validateSpecialOrderLine(SpecialOrderLine line)
        {
            if (line.SpecialOrderID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Special Order ID.");
            }
            if (line.SpecialOrderItemID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid Special Order Item ID.");
            }
            if (line.Quantity <= 0)
            {
                throw new ArgumentOutOfRangeException("You must specify a valid quantity.");
            }
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Validates a Special Order Line ID
        /// </summary>
        /// <param name="lineID"></param>
        private void validateID(int ID)
        {
            if (ID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID.");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<SpecialOrderLineDetail> RetrieveSpecialOrderLineDetailListByOrderID(int id)
        {
            List<SpecialOrderLineDetail> specialOrderLineDetailList = null;
            try
            {
                specialOrderLineDetailList = _specialOrderLineAccessor.RetrieveSpecialOrderLineDetailListByOrderID(id);
            }
            catch (Exception)
            {
                throw;
            }
            return specialOrderLineDetailList;
        }

        /// <summary>
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldRecieved"></param>
        /// <param name="newRecieved"></param>
        /// <returns></returns>
        public bool EditSpecialOrderLineQtyReceivedByID(int id, int oldRecieved, int newRecieved)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (1 == _specialOrderLineAccessor.EditSpecialOrderLineQtyReceivedByID(id, oldRecieved, newRecieved))
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
        /// Zachary Hall
        /// 2018/04/20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID Value");
            }
            var result = false;
            try
            {
                if (0 < _specialOrderLineAccessor.EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(id))
                {
                    result = true;
                }
                else
                {
                    throw new ApplicationException("No records were updated.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
