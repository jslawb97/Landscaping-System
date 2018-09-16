using System;
using DataAccess;
using DataObjects;
using System.Collections.Generic;


namespace Logic
{
    public class PrepRecordManager : IPrepRecordManager
    {
        private readonly IPrepRecordAccessor _PrepRecordAccessor;

        public PrepRecordManager()
        {
            _PrepRecordAccessor = new PrepRecordAccessor();
        }

        // For unit tests
        public PrepRecordManager(IPrepRecordAccessor PrepRecordAccessor)
        {
            _PrepRecordAccessor = PrepRecordAccessor;
        }

        /// <summary>
        /// Adds an PrepRecord item 
        /// </summary>
        /// <param name="newItem">The item to add</param>
        /// <returns>the id of the newly added item</returns>
        /// <remarks>
        /// Author: Badis SAIDANI
        /// Date Modified: 2018/2/24
        /// </remarks>
        public int CreatePrepRecord(PrepRecord newItem)
        {
            return _PrepRecordAccessor.CreatePrepRecord(newItem);
        }

        /// <summary>
        /// Edits an PrepRecord Item from an old entry to a new entry
        /// </summary>
        /// <param name="oldItem">The item being edited</param>
        /// <param name="newItem">The item with the new data</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Badis SAIDANI
        /// Date Modified: 2018/2/24
        /// </remarks>
        public int EditPrepRecordItem(PrepRecord oldItem, PrepRecord newItem)
        {
            return _PrepRecordAccessor.EditPrepRecordItem(oldItem, newItem);
        }

        /// <summary>
        /// Retrieves an PrepRecord by it's ID
        /// </summary>
        /// <param name="id">The ID of the PrepRecord to retrieve.</param>
        /// <returns>An PrepRecord item from the database</returns>
        /// <remarks>
        /// Badis SAIDANI
        /// Updated 2018/02/24
        /// </remarks>
        public PrepRecord RetrievePrepRecordByID(int id)
        {
            return _PrepRecordAccessor.RetrievePrepRecordByID(id);
        }

        /// <summary>
        /// Gets a list of PrepRecords. 
        /// </summary>
        /// <returns>Returns a collection of PrepRecord</returns>
        /// <remarks>
        /// Author: Badis SAIDANI
        /// Date Modified: 2018/2/24
        /// </remarks>
        public List<PrepRecord> RetrievePrepRecordList()
        {
            return _PrepRecordAccessor.RetrievePrepRecordList();
        }

        /// <summary>
        /// Gets a list of PrepRecords. 
        /// </summary>
        /// <returns>Returns a collection of PrepRecord</returns>
        /// <remarks>
        /// Author: Badis SAIDANI
        /// Date Modified: 2018/5/3
        /// </remarks>
        public List<PrepRecordDetail> RetrievePrepRecordDetailList()
        {
            return _PrepRecordAccessor.RetrievePrepRecordDetailList();
        }


        /// <summary>
        /// Deletes an PrepRecord Item in the database
        /// </summary>
        /// <param name="id">The id of the item being deleted</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Badis SAIDANI
        /// Date Modified: 2018/2/24
        /// </remarks>
        public int DeletePrepRecordByID(int id)
        {
            return _PrepRecordAccessor.DeletePrepRecordByID(id);
        }
    }

}
