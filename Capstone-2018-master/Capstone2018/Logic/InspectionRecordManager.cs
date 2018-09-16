using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class InspectionRecordManager : IInspectionRecordManager
    {
        private IInspectionRecordAccessor _inspectionRecordAccessor;
        
        public InspectionRecordManager()
        {
            _inspectionRecordAccessor = new InspectionRecordAccessor();
        }
        
        public InspectionRecordManager(IInspectionRecordAccessor inspectionRecordAccessor)
        {
            _inspectionRecordAccessor = inspectionRecordAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/07
        /// 
        /// Method to create a new InspectionRecord
        /// </summary>
        /// <param name="inspectionRecord"></param>
        /// <returns>Success/failure of the creation</returns>
        public bool CreateInspectionRecord(InspectionRecord inspectionRecord)
        {
            bool result = false;

            if(!inspectionRecord.EquipmentID.IsValidID()
                || !inspectionRecord.EmployeeID.IsValidID()
                || !inspectionRecord.Description.IsValidDescriptionProperty())
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }

            try
            {
                result = (0 != _inspectionRecordAccessor.CreateInspectionRecord(inspectionRecord));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to edit an InspectionRecord
        /// </summary>
        /// <param name="oldInspectionRecord"></param>
        /// <param name="newInspectionRecord"></param>
        /// <returns></returns>
        public bool EditInspectionRecord(InspectionRecord oldInspectionRecord, InspectionRecord newInspectionRecord)
        {
            bool result = false;

            if(!newInspectionRecord.InspectionRecordID.IsValidID()
                || !newInspectionRecord.EquipmentID.IsValidID()
                || !newInspectionRecord.EmployeeID.IsValidID()
                || !newInspectionRecord.Description.IsValidDescriptionProperty()
                || !oldInspectionRecord.InspectionRecordID.IsValidID()
                || !oldInspectionRecord.EquipmentID.IsValidID()
                || !oldInspectionRecord.EmployeeID.IsValidID()
                || !oldInspectionRecord.Description.IsValidDescriptionProperty()
                || newInspectionRecord.InspectionRecordID != oldInspectionRecord.InspectionRecordID)
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }

            try
            {
                result = (0 != _inspectionRecordAccessor
                    .EditInspectionRecord(oldInspectionRecord, newInspectionRecord));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to retrieve an InspectionRecord by ID
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns>InspectionRecord</returns>
        public InspectionRecord RetrieveInspectionRecordByID(int inspectionRecordID)
        {
            InspectionRecord inspectionRecord = null;

            if(!inspectionRecordID.IsValidID())
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }

            try
            {
                inspectionRecord
                    = _inspectionRecordAccessor.RetrieveInspectionRecordByID(inspectionRecordID);
            }
            catch (Exception)
            {
                throw;
            }

            return inspectionRecord;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/22
        /// 
        /// Method to retrieve a list of InspectionRecord by EquipmentID
        /// </summary>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public List<InspectionRecord> RetrieveInspectionRecordListByEquipmentID(int equipmentID)
        {
            List<InspectionRecord> inspectionRecordList = null;

            if(!equipmentID.IsValidID())
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }

            try
            {
                inspectionRecordList
                    = _inspectionRecordAccessor.RetrieveInspectionRecordListByEquipmentID(equipmentID);
            }
            catch (Exception)
            {
                throw;
            }

            return inspectionRecordList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to deactivate an InspectionRecord by ID
        /// </summary>
        /// <param name="inspectionRecordID"></param>
        /// <returns></returns>
        public bool DeleteInspectionRecord(int inspectionRecordID)
        {
            bool result = false;

            if (!inspectionRecordID.IsValidID())
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }
            try
            {
                result = (0 !=
                    _inspectionRecordAccessor.DeleteInspectionRecordByID(inspectionRecordID));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/23
        /// 
        /// Method to retrieve all InspectionRecords
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecord> RetrieveInspectionRecordList()
        {
            List<InspectionRecord> inspectionRecordList = null;

            try {
                inspectionRecordList
                    = _inspectionRecordAccessor.RetrieveInspectionRecordList();
            }
            catch (Exception)
            {
                throw;
            }

            return inspectionRecordList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/02
        /// 
        /// Method to retrieve InspectionRecordDetails
        /// </summary>
        /// <returns></returns>
        public List<InspectionRecordDetail> RetrieveInspectionRecordDetailList()
        {
            List<InspectionRecordDetail> inspectionRecordDetailList = null;

            try
            {
                inspectionRecordDetailList
                    = _inspectionRecordAccessor.RetrieveInspectionRecordDetailList();
            }
            catch (Exception)
            {
                throw;
            }

            return inspectionRecordDetailList;
        }
    }
}
