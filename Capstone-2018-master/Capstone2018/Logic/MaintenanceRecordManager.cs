using DataAccess;
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
    /// Logic Layer for Maintenance Records
    /// </summary>
    public class MaintenanceRecordManager : IMaintenanceRecordManager
    {
        private IMaintenanceRecordAccessor _maintenanceRecordAccessor;
        private IEquipmentAccessor _equipmentAccessor;
        private IEmployeeAccessor _employeeAccessor;

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/01
        /// 
        /// Default constructor
        /// </summary>
        public MaintenanceRecordManager()
        {
            _maintenanceRecordAccessor = new MaintenanceRecordAccessor();
            _equipmentAccessor = new EquipmentAccessor();
            _employeeAccessor = new EmployeeAccessor();
        }

        public MaintenanceRecordManager(IMaintenanceRecordAccessor maintenanceRecordAccessor, IEquipmentAccessor equipmentAccessor, IEmployeeAccessor employeeAccessor)
        {
            _maintenanceRecordAccessor = maintenanceRecordAccessor;
            _equipmentAccessor = equipmentAccessor;
            _employeeAccessor = employeeAccessor;
        }

        public int CreateMaintenanceRecord(MaintenanceRecord maintenanceRecord)
        {
            var result = 0;

            try
            {
                result = _maintenanceRecordAccessor.CreateMaintenanceRecord(maintenanceRecord);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/04/20
        /// 
        /// Method deletes a maintenance record by the ID
        /// </summary>
        /// <param name="maintenanceRecordID"></param>
        /// <returns></returns>
        public bool DeleteMaintenanceRecord(int maintenanceRecordID)
        {
            bool result = false;

            if (!maintenanceRecordID.IsValidID())
            {
                throw new ArgumentOutOfRangeException("Bad input(s)!");
            }
            try
            {
                result = (0 !=
                    _maintenanceRecordAccessor.DeleteMaintenanceRecordByID(maintenanceRecordID));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool EditMaintenanceRecord(MaintenanceRecord oldMaintenanceRecord, MaintenanceRecord newMaintenanceRecord)
        {
            var result = false;

            try
            {
                result = (0 != _maintenanceRecordAccessor.EditMaintenanceRecord(oldMaintenanceRecord, newMaintenanceRecord));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public MaintenanceRecordDetail RetrieveMaintenanceRecordDetail(MaintenanceRecord maintenanceRecord)
        {
            MaintenanceRecordDetail maintenanceRecordDetail = null;
            MaintenanceRecord record = null;
            Equipment equip = null;
            Employee employ = null;

            try
            {
                record = _maintenanceRecordAccessor.RetrieveMaintenanceRecordByID(maintenanceRecord.MaintenanceRecordID);
                equip = _equipmentAccessor.RetrieveEquipmentByID(maintenanceRecord.EquipmentID);
                employ = _employeeAccessor.RetrieveEmployeeByID(maintenanceRecord.EmployeeID);

                maintenanceRecordDetail = new MaintenanceRecordDetail()
                {
                    MaintenanceRecord = record,
                    Equipment = equip,
                    Employee = employ
                };
            }
            catch (Exception)
            {

                throw;
            }

            return maintenanceRecordDetail;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/27
        /// 
        /// Retrieves a list of maintenance record details
        /// </summary>
        /// <returns></returns>
        public List<MaintenanceRecordDetail> RetrieveMaintenanceRecordDetailList()
        {
            List<MaintenanceRecordDetail> maintenanceRecordDetailList = new List<MaintenanceRecordDetail>();

            try
            {
                maintenanceRecordDetailList = _maintenanceRecordAccessor.RetrieveMaintenanceRecordDetailList();
            }
            catch(Exception)
            {
                throw;
            }

            return maintenanceRecordDetailList;
        }
    }
}
