using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/01
    /// </summary>
    /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
    public interface IMaintenanceRecordAccessor
    {
        int CreateMaintenanceRecord(MaintenanceRecord record);

        int EditMaintenanceRecord(MaintenanceRecord oldRecord, MaintenanceRecord newRecord);

        MaintenanceRecord RetrieveMaintenanceRecordByID(int maintenanceRecordID);

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/19
        /// 
        /// Deletes a maintenance record.
        /// </summary>
        /// <param name="maintenanceRecordID"></param>
        /// <returns></returns>
        int DeleteMaintenanceRecordByID(int maintenanceRecordID);

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/27
        /// 
        /// Retrieves list of maintenance record details
        /// </summary>
        /// <returns></returns>
        List<MaintenanceRecordDetail> RetrieveMaintenanceRecordDetailList();
    }
}
