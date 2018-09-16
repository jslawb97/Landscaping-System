using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/03/07
    /// 
    /// Interface for the InspectionRecordAccessor
    /// </summary>
    public interface IInspectionRecordAccessor
    {
        int CreateInspectionRecord(InspectionRecord inspectionRecord);
        InspectionRecord RetrieveInspectionRecordByID(int inspectionRecordID);
        List<InspectionRecord> RetrieveInspectionRecordListByEquipmentID(int equipmentID);
        List<InspectionRecord> RetrieveInspectionRecordList();
        List<InspectionRecordDetail> RetrieveInspectionRecordDetailList();
        int EditInspectionRecord(InspectionRecord oldInspectionRecord
            , InspectionRecord newInspectionRecord);
        int DeleteInspectionRecordByID(int inspectionRecordID);
    }
}
