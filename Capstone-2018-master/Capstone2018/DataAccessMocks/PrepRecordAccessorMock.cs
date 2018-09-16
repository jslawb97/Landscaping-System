using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class PrepRecordAccessorMock : IPrepRecordAccessor
    {
        private List<PrepRecord> _prepRecordList = new List<PrepRecord>();

        // <summary>
        /// Badis Saidani
        /// Created 2018/03/07
        /// 
        /// Mock constructor to add data to the PrepRecord list
        /// </summary>
        public PrepRecordAccessorMock()
        {
            _prepRecordList.Add(new PrepRecord()
            {
                PrepRecordID = 1000000,
                EquipmentID = 1000000,
                EmployeeID = 1000000,
                Description = "Prep Record 1",
                Date = new DateTime(2018, 3, 17, 10, 0, 0)
            });
            _prepRecordList.Add(new PrepRecord()
            {
                PrepRecordID = 1000001,
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "Prep Record 2",
                Date = new DateTime(2018, 3, 17, 10, 0, 0)
            });
            _prepRecordList.Add(new PrepRecord()
            {
                PrepRecordID = 1000002,
                EquipmentID = 1000002,
                EmployeeID = 1000002,
                Description = "Prep Record 3",
                Date = new DateTime(2018, 3, 17, 10, 0, 0)
            });
            _prepRecordList.Add(new PrepRecord()
            {
                PrepRecordID = 1000003,
                EquipmentID = 1000003,
                EmployeeID = 1000003,
                Description = "Prep Record 4",
                Date = new DateTime(2018, 3, 17, 10, 0, 0)
            });
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/03/07
        /// 
        /// Mock method to add prepRecord
        /// </summary>
        /// <returns></returns>
        public int CreatePrepRecord(PrepRecord prepRecord)
        {
            try
            {
                prepRecord.PrepRecordID = Constants.IDSTARTVALUE + _prepRecordList.Count;
                this._prepRecordList.Add(prepRecord);

                return _prepRecordList[_prepRecordList.Count - 1].PrepRecordID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeletePrepRecordByID(int prepRecordID)
        {
            var prep = _prepRecordList.Find(p => p.PrepRecordID == prepRecordID);
            int res = _prepRecordList.Count;

            try
            {
                _prepRecordList.Remove(prep);

                res = res - 1;
                return res;
            }
            catch (Exception)
            {
                return res;
            }
        }

        public int EditPrepRecordItem(PrepRecord oldPrepRecord, PrepRecord newPrepRecord)
        {
            var found = 0;


            this._prepRecordList.ForEach(p =>
            {
                if (p == oldPrepRecord)
                {
                    p.EquipmentID = newPrepRecord.EquipmentID;
                    p.EquipmentID = newPrepRecord.EmployeeID;
                    p.Description = newPrepRecord.Description;
                    p.Date = newPrepRecord.Date;
                    found = 1;
                }
            });
            return found;
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/03/07
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="prepRecordID"></param>
        /// <returns></returns>
        public PrepRecord RetrievePrepRecordByID(int prepRecordID)
        {
            PrepRecord prepRecord = null;
            foreach (var emp in _prepRecordList)
            {
                if (emp.PrepRecordID == prepRecordID)
                {
                    prepRecord = emp;
                }
            }
            if (prepRecord == null)
            {
                throw new ApplicationException("PrepRecord record not found.");
            }
            return prepRecord;
        }

        public List<PrepRecordDetail> RetrievePrepRecordDetailList()
        {
            throw new NotImplementedException();
        }

        public List<PrepRecord> RetrievePrepRecordList()
        {
            return _prepRecordList;
        }


    }


}
