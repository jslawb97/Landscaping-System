using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class MaintenanceRecordAccessorMock : IMaintenanceRecordAccessor
    {
        List<MaintenanceRecord> maintenanceRecordList = new List<MaintenanceRecord>();

        public MaintenanceRecordAccessorMock()
        {
            maintenanceRecordList.Add(new MaintenanceRecord
            {
                MaintenanceRecordID = 1000000,
                EquipmentID = 1000000,
                EmployeeID = 1000000,
                Description = "Changed oil",
                Date = new DateTime(2018 - 01 - 01),

            });
            maintenanceRecordList.Add(new MaintenanceRecord
            {
                MaintenanceRecordID = 1000001,
                EquipmentID = 1000001,
                EmployeeID = 1000001,
                Description = "New tires",
                Date = new DateTime(2018 - 01 - 01),

            });
        }
        public int CreateMaintenanceRecord(MaintenanceRecord record)
        {
            throw new NotImplementedException();
        }

        public int DeleteMaintenanceRecordByID(int maintenanceRecordID)
        {
            throw new NotImplementedException();
        }

        public int EditMaintenanceRecord(MaintenanceRecord oldRecord, MaintenanceRecord newRecord)
        {
            throw new NotImplementedException();
        }

        public MaintenanceRecord RetrieveMaintenanceRecordByID(int maintenanceRecordID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Noah Davison
        /// Created 04/27/2018
        /// 
        /// Mock method to retrieve list of maintenance record details
        /// </summary>
        /// <returns></returns>
        public List<MaintenanceRecordDetail> RetrieveMaintenanceRecordDetailList()
        {
            List<MaintenanceRecordDetail> maintenanceRecordDetailList = new List<MaintenanceRecordDetail>();

            foreach (MaintenanceRecord maintenanceRecord in maintenanceRecordList)
            {
                maintenanceRecordDetailList.Add(new MaintenanceRecordDetail
                {
                    MaintenanceRecord = maintenanceRecord,
                });
            }

            return maintenanceRecordDetailList;
        }
    }
}
