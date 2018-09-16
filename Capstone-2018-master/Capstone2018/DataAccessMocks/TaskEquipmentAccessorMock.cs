using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessMocks
{
    /// <summary>
    /// Brady Feller
    /// Created on 2018/04/05
    /// 
    /// Mock Accessor for TaskEmployeeAccessor
    /// </summary>
    public class TaskEquipmentAccessorMock : ITaskEquipmentAccessor
    {
        private List<TaskEquipmentDetail> _detail;
        private List<Equipment> _equipmentList;
        private List<TaskEquipment> _taskEquipmentList;

        public TaskEquipmentAccessorMock()
        {
            _detail = new List<TaskEquipmentDetail>();

            _detail.Add(new TaskEquipmentDetail
            {
                TaskName = "Mow the Lawn",
                HoursOfWork = 1,
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE,
                EquipmentAssignedCount = 1

            });
            _detail.Add(new TaskEquipmentDetail
            {
                TaskName = "Prepare Sod",
                HoursOfWork = 2,
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE + 1,
                EquipmentAssignedCount = 1

            });
            _detail.Add(new TaskEquipmentDetail
            {
                TaskName = "Trim Trees",
                HoursOfWork = 2,
                TaskTypeEquipmentNeedID = Constants.IDSTARTVALUE + 2,
                EquipmentAssignedCount = 0

            });

            _equipmentList = new List<Equipment>();
            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000000,
                EquipmentTypeID = "Vehicle",
                Name = "Fork Lift",
                MakeModelID = 1000001,
                DatePurchased = new DateTime(2008, 12, 25),
                DateLastRepaired = null,
                PriceAtPurchase = 14000.00M,
                CurrentValue = 8000.00M,
                WarrantyUntil = new DateTime(2013, 12, 30),
                EquipmentStatusID = "normal",
                EquipmentDetails = "Fork lift for the warhouse",
                Active = true
            });
            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000001,
                EquipmentTypeID = "Vehicle",
                Name = "Bulldozer",
                MakeModelID = 1000002,
                DatePurchased = new DateTime(2008, 12, 25),
                DateLastRepaired = null,
                PriceAtPurchase = 38000.00M,
                CurrentValue = 17000.00M,
                WarrantyUntil = new DateTime(2016, 12, 25),
                EquipmentStatusID = "normal",
                EquipmentDetails = "Fork lift for the warhouse",
                Active = true
            });

            _taskEquipmentList = new List<TaskEquipment>();
            _taskEquipmentList.Add(new TaskEquipment()
            {
                EquipmentID = Constants.IDSTARTVALUE,
                EmployeeID = Constants.IDSTARTVALUE,
                JobID = Constants.IDSTARTVALUE
            });
            _taskEquipmentList.Add(new TaskEquipment()
            {
                EquipmentID = Constants.IDSTARTVALUE + 1,
                EmployeeID = Constants.IDSTARTVALUE + 1,
                JobID = Constants.IDSTARTVALUE + 1
            });
        }

        /// <summary>
        /// Sam Dramstad
        /// Created 2018/04/11
        /// 
        /// Adds a specified Equipment to a TaskEquipment list.
        /// </summary>
        /// 

        public bool AddEquipmentToTaskEquipment(int equipmentID, int jobID, int taskTypeEquipmentNeedID)
        {
            return true;
        }

        /// <summary>
        /// Jacob Conley
        /// Created 2018/04/13
        /// 
        /// Removes a specified Equipment from a TaskEquipment list.
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public int DeleteEquipmentFromTaskEquipment(int jobID, int equipmentID)
        {
            int result = 0;

            bool existed = _taskEquipmentList.Remove(_taskEquipmentList.Find(t => t.EquipmentID == equipmentID && t.JobID == jobID));

            if (_taskEquipmentList.Contains(_taskEquipmentList.Find(t => t.EquipmentID == equipmentID && t.JobID == jobID)) == false && existed == true)
            {
                result = 1;
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/05
        /// 
        /// Mock accessor to retrieve assigned equipment
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskID(int taskID)
        {
            List<Equipment> equipmentList = new List<Equipment>();

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.EquipmentID == taskID)
                {
                    equipmentList.Add(equipment);
                }
            }

            return equipmentList;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/05
        /// 
        /// Mock accessor to retrieve assigned equipment
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            List<Equipment> equipmentList = new List<Equipment>();

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.EquipmentID == taskID)
                {
                    equipmentList.Add(equipment);
                }
            }

            return equipmentList;
        }

        /// <summary>
        /// Zachary Hall
        /// Created on 2018/04/05
        /// 
        /// Mocks getting a list of details from a datastore
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEquipmentDetail> RetrieveTaskEquipmentDetailByJobID(int jobID)
        {
            List<TaskEquipmentDetail> detail = null;
            try
            {
                detail = _detail;
                if (detail == null)
                {
                    throw new ApplicationException("The detail list was null");
                }
            }
            catch (Exception)
            {

                throw;
            }

            return detail;
        }

        /// <summary>
        /// Noah Davison
        /// 2018/04/13
        /// 
        /// Data access mock method to delete assigned equipment
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>rows deleted</returns>
        public int DeleteAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            int rowsDeleted = 0;

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.EquipmentID == taskID)
                {
                    rowsDeleted++;
                }

            }

            return rowsDeleted;
        }

        public int UpdateEquipmentID(int taskEquipmentID, int equipmentID)
        {
            throw new NotImplementedException();
        }

        public int UpdateEquipmentIDToNull(int taskEquipmentID)
        {
            throw new NotImplementedException();
        }
    }
}
