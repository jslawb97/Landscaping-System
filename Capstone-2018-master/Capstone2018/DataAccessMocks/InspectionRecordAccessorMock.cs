using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class InspectionRecordAccessorMock : IInspectionRecordAccessor
    {
        private List<InspectionRecord> _inspectionRecords = new List<InspectionRecord>();

        public InspectionRecordAccessorMock()
        {
            _inspectionRecords.Add(new InspectionRecord
            {
                InspectionRecordID = 1000000,
                EquipmentID = 1000000,
                EmployeeID = 1000000,
                Description = "TestDescription1",
                Date = DateTime.Now
            });
            _inspectionRecords.Add(new InspectionRecord
            {
                InspectionRecordID = 1000001,
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "TestDescription2",
                Date = DateTime.Parse("1/28/2010 9:01:26 PM")
            });
            _inspectionRecords.Add(new InspectionRecord
            {
                InspectionRecordID = 1000002,
                EquipmentID = 1000002,
                EmployeeID = 1000002,
                Description = "TestDescription3",
                Date = DateTime.Now
            });
            _inspectionRecords.Add(new InspectionRecord
            {
                InspectionRecordID = 1000003,
                EquipmentID = 1000000,
                EmployeeID = 1000003,
                Description = "TestDescription4",
                Date = DateTime.Now
            });
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/09
        /// 
        /// Mock method to create a new InspectionRecord
        /// </summary>
        /// <param name="inspectionRecord"></param>
        /// <returns></returns>
        public int CreateInspectionRecord(InspectionRecord inspectionRecord)
        {
            _inspectionRecords.Add(inspectionRecord);
            return 1;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Mock method to retrieve an InspectionRecords by ID
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns></returns>
        public InspectionRecord RetrieveInspectionRecordByID(int inspectionRecordID)
        {
            InspectionRecord inspectionRecord = null;

            foreach(InspectionRecord record in _inspectionRecords)
            {
                if(record.InspectionRecordID == inspectionRecordID)
                {
                    inspectionRecord = record;
                }
            }

            if(inspectionRecord == null)
            {
                throw new ApplicationException("No data found");
            }

            return inspectionRecord;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Mock method to retrieve a list of InspectionRecords by EquipmentID
        /// </summary>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public List<InspectionRecord> RetrieveInspectionRecordListByEquipmentID(int equipmentID)
        {
            List<InspectionRecord> inspectionRecordList = new List<InspectionRecord>();

            foreach(InspectionRecord record in _inspectionRecords)
            {
                if(record.EquipmentID == equipmentID)
                {
                    inspectionRecordList.Add(record);
                }
            }

            if(inspectionRecordList.Count() == 0)
            {
                throw new ApplicationException("No data found");
            }

            return inspectionRecordList;
        }
        
        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Mock method to edit an InspectionRecord
        /// </summary>
        /// <param name="oldInspectionRecord"></param>
        /// <param name="newInspectionRecord"></param>
        /// <returns></returns>
        public int EditInspectionRecord(InspectionRecord oldInspectionRecord
            , InspectionRecord newInspectionRecord)
        {
            int result = 0;

            foreach(InspectionRecord record in _inspectionRecords) {
                if (newInspectionRecord.InspectionRecordID == record.InspectionRecordID)
                {
                    if (oldInspectionRecord.EquipmentID != record.EquipmentID
                        || oldInspectionRecord.EmployeeID != record.EmployeeID
                        || !oldInspectionRecord.Description.Equals(record.Description)
                        || !oldInspectionRecord.Date.Equals(record.Date))
                    {
                        throw new ApplicationException("There was a problem editing the InspectionRecord");
                    }
                    record.EquipmentID = newInspectionRecord.EquipmentID;
                    record.EmployeeID = newInspectionRecord.EmployeeID;
                    record.Description = newInspectionRecord.Description;
                    record.Date = newInspectionRecord.Date;

                    result = 1;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Mock method to delete an InspectionRecord by ID
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns></returns>
        public int DeleteInspectionRecordByID(int inspectionRecordID)
        {
            int result = 0;
            InspectionRecord foundRecord = null;

            foreach (InspectionRecord record in _inspectionRecords)
            {
                if (inspectionRecordID == record.InspectionRecordID)
                {
                    foundRecord = record;
                    result++;
                }
            }
            if (result < 1)
            {
                throw new ApplicationException("There was a problem deleting your InspectionRecord");
            }
            _inspectionRecords.Remove(foundRecord);

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Mock method to retrieve all InspectionRecords
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecord> RetrieveInspectionRecordList()
        {
            return _inspectionRecords;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/03
        /// 
        /// Mock method to retrieve a list of InspectionRecordDetails
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecordDetail> RetrieveInspectionRecordDetailList()
        {
            var inspectionRecordDetailList = new List<InspectionRecordDetail>();
            foreach(var ir in _inspectionRecords)
            {
                inspectionRecordDetailList.Add(new InspectionRecordDetail
                {
                    InspectionRecordID = ir.InspectionRecordID,
                    EquipmentID = ir.EquipmentID,
                    EmployeeID = ir.EmployeeID,
                    Description = ir.Description,
                    Date = ir.Date,
                    EquipmentName = "Chainsaw",
                    EmployeeFirstName = "John",
                    EmployeeLastName = "Smith"
                });
            }
            return inspectionRecordDetailList;
        }
    }
}
