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
    /// Created on 2018/04/05
    /// 
    /// Manager class for TaskEquipment records
    /// </summary>
    public class TaskEquipmentManager : ITaskEquipmentManager
    {
        private ITaskEquipmentAccessor _taskEquipmentAccessor;

        public TaskEquipmentManager()
        {
            _taskEquipmentAccessor = new TaskEquipmentAccessor();
        }

        public TaskEquipmentManager(ITaskEquipmentAccessor taskEquipmentAccessor)
        {
            _taskEquipmentAccessor = taskEquipmentAccessor;
        }

        /// <summary>
        /// Brady Feller
        /// Created on 2018/04/05
        /// 
        /// Gets a detail list representing TaskEquipment records for a given Job id
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<TaskEquipmentDetail> RetrieveTaskEquipmentDetailByJobID(int jobID)
        {
            List<TaskEquipmentDetail> detail = null;

            try
            {
                detail = _taskEquipmentAccessor.RetrieveTaskEquipmentDetailByJobID(jobID);
                if (detail == null)
                {
                    throw new ApplicationException("The detail list returned null!");
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
        /// Created 2018/04/05
        /// 
        /// Gets a list of equipment assigned to a particular task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskID(int taskID)
        {
            List<Equipment> equipment = null;

            try
            {
                equipment = _taskEquipmentAccessor.RetrieveAssignedEquipmentByTaskID(taskID);
            }
            catch (Exception)
            {

                throw;
            }

            return equipment;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/19
        /// 
        /// Gets a list of equipment assigned to a particular task and job
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            List<Equipment> equipment = null;

            try
            {
                equipment = _taskEquipmentAccessor.RetrieveAssignedEquipmentByTaskIDAndJobID(taskID, jobID);
            }
            catch (Exception)
            {

                throw;
            }

            return equipment;
        }

        public bool AddEquipmentToTaskEquipment(int equipmentID, int jobID, int taskTypeEquipmentNeedID)
        {
            bool result = false;
            try
            {
                if (_taskEquipmentAccessor.AddEquipmentToTaskEquipment(equipmentID, jobID, taskTypeEquipmentNeedID))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/13
        /// Deletes all equipment assigned to a particular task based on taskID
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns>Rows Deleted</returns>
        public int DeleteAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID)
        {
            int rowsDeleted = 0;
            try
            {
                rowsDeleted = _taskEquipmentAccessor.DeleteAssignedEquipmentByTaskIDAndJobID(taskID, jobID);


                if (rowsDeleted == 0)
                {
                    throw new ApplicationException("There was no equipment assigned to that task to delete");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rowsDeleted;
        }

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/14
        /// 
        /// Removes a specific Equipment from a TaskEquipment list
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        public int DeleteEquipmentFromTaskEquipment(int jobID, int equipmentID)
        {
            int result = 0;

            if (jobID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Job ID Value");
            }
            if (equipmentID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad Equipment ID Value");
            }
            try
            {
                result = _taskEquipmentAccessor.DeleteEquipmentFromTaskEquipment(jobID, equipmentID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool UpdateEquipmentID(int taskEquipmentID, int equipmentID)
        {
            bool result = true;
            try
            {
                int updateResult = _taskEquipmentAccessor.UpdateEquipmentID(taskEquipmentID, equipmentID);
                if(updateResult != 1)
                {
                    result = false;
                }
            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }

        public bool UpdateEquipmentIDToNull(int taskEquipmentID)
        {
            bool result = true;
            try
            {
                int updateResult = _taskEquipmentAccessor.UpdateEquipmentIDToNull(taskEquipmentID);
                if (updateResult != 1)
                {
                    result = false;
                }
            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }
    }
}
