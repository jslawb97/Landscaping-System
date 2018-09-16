using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// James McPherson
    /// Created 2018/03/07
    /// 
    /// Interface for the InspectionRecordManager
    /// </summary>
    public interface IInspectionRecordManager
    {
        bool CreateInspectionRecord(InspectionRecord inspectionRecord);
        InspectionRecord RetrieveInspectionRecordByID(int inspectionRecordID);
        List<InspectionRecord> RetrieveInspectionRecordListByEquipmentID(int equipmentID);
        List<InspectionRecord> RetrieveInspectionRecordList();
        List<InspectionRecordDetail> RetrieveInspectionRecordDetailList();
        bool EditInspectionRecord(InspectionRecord oldInspectionRecord
            , InspectionRecord newInspectionRecord);
        bool DeleteInspectionRecord(int inspectionRecordID);
    }
}
