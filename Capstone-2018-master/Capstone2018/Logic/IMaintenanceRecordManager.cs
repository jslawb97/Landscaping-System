using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Brady Feller
    /// Created 2018/03/01
    /// 
    /// Interface for a Maintenace Record Manager
    /// </summary>
    public interface IMaintenanceRecordManager
    {
        int CreateMaintenanceRecord(MaintenanceRecord maintenanceRecord);

        bool EditMaintenanceRecord(MaintenanceRecord oldMaintenanceRecord, MaintenanceRecord newMaintenanceRecord);

        MaintenanceRecordDetail RetrieveMaintenanceRecordDetail(MaintenanceRecord maintenanceRecord);

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/20
        /// 
        /// Deletes a mainenance record
        /// </summary>
        /// <param name="maintenanceRecordID"></param>
        /// <returns></returns>
        bool DeleteMaintenanceRecord(int maintenanceRecordID);


        /// <summary>
        /// Noah Davison
        /// Created 2018/04/27
        /// 
        /// Retrieves a list of maintenance record details
        /// </summary>
        /// <returns></returns>
        List<MaintenanceRecordDetail> RetrieveMaintenanceRecordDetailList();
    }
}
