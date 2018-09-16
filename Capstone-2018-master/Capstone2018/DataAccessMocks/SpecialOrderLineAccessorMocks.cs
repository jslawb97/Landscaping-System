using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    public class SpecialOrderLineAccessorMock : ISpecialOrderLineAccessor
    {

        private List<SpecialOrderLine> _specialOrderLines = new List<SpecialOrderLine>();

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Constructor that adds sample data to the _specialOrderLines 
        /// list when instantiated
        /// </summary>
        public SpecialOrderLineAccessorMock()
        {
            _specialOrderLines.Add(new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000000,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000000,
                Quantity = 1
            });

            _specialOrderLines.Add(new SpecialOrderLine()
            {
                SpecialOrderLineID = 1000001,
                SpecialOrderID = 1000000,
                SpecialOrderItemID = 1000001,
                Quantity = 3
            });
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Adds a Special Order Line to _specialOrderLines for testing
        /// </summary>
        /// <param name="specialOrderLine"></param>
        /// <returns></returns>
        public int CreateSpecialOrderLine(SpecialOrderLine specialOrderLine)
        {
            int rowCount;
            int lineCount = _specialOrderLines.Count;

            _specialOrderLines.Add(specialOrderLine);

            if (_specialOrderLines.Count > lineCount)
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
        /// Created 3/28/2018
        /// 
        /// Removes a Special Order Line from _specialOrderLines for testing
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        public int DeleteSpecialOrderLine(int specialOrderLineID)
        {
            int rowCount;

            var lineToDelete = _specialOrderLines.Find(line => line.SpecialOrderLineID == specialOrderLineID);

            if (_specialOrderLines.Remove(lineToDelete))
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
        /// Created 3/28/2018
        /// 
        /// Edits an existing Special Order Line in _specialOrderLines for testing
        /// </summary>
        /// <param name="oldLine"></param>
        /// <param name="newLine"></param>
        /// <returns></returns>
        public int EditSpecialOrderLine(SpecialOrderLine oldLine, SpecialOrderLine newLine)
        {
            // Have someone check this to make sure it's not insane and rediculous, because it might be
            // and I used the same structure for the SpecialOrderMock edit method.

            int rowCount;

            // gets the old line from the list
            SpecialOrderLine oldOrderLine = RetrieveSpecialOrderLineByID(oldLine.SpecialOrderLineID);

            // sets the old line equal to the new line provided by the parameter
            oldOrderLine = newLine;

            // finds the index of the old line and replaces it with the new one
            _specialOrderLines[_specialOrderLines.FindIndex(line => line.SpecialOrderID == oldLine.SpecialOrderID)] = oldOrderLine;

            if (_specialOrderLines.Contains(oldOrderLine))
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;
        }

        public int EditSpecialOrderLineQtyReceivedByID(int id, int oldRecieved, int newRecieved)
        {
            throw new NotImplementedException();
        }

        public int EditSpecialOrderLineQtyReceivedToQtyOrderedByOrderID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Retrieves a SpecialOrderLine from_specialOrderLines using SpecialOrderLineID 
        /// for testing.
        /// </summary>
        /// <param name="specialOrderLineID"></param>
        /// <returns></returns>
        public SpecialOrderLine RetrieveSpecialOrderLineByID(int specialOrderLineID)
        {
            if (!_specialOrderLines.Any(line => line.SpecialOrderLineID == specialOrderLineID))
            {
                throw new ApplicationException();
            }

            return _specialOrderLines.Find(line => line.SpecialOrderLineID == specialOrderLineID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/28/2018
        /// 
        /// Retrieves all Special Order Lines from _specialOrderLines for a specific
        /// Special Order using the SpecialOrderID for testing.
        /// </summary>
        /// <param name="specialOrderID"></param>
        /// <returns></returns>
        public List<SpecialOrderLine> RetrieveSpecialOrderLineBySpecialOrderID(int specialOrderID)
        {
            if (!_specialOrderLines.Any(line => line.SpecialOrderID == specialOrderID))
            {
                throw new ApplicationException();
            }

            return _specialOrderLines.FindAll(line => line.SpecialOrderID == specialOrderID);
        }

        public List<SpecialOrderLineDetail> RetrieveSpecialOrderLineDetailListByOrderID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
