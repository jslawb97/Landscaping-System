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
    /// Created 2018/04/05
    /// 
    /// Accessor interface for task equipment
    /// </summary>
    public interface ITaskEquipmentAccessor
    {
        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Gets a list of TaskEquipment detail records
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<TaskEquipmentDetail> RetrieveTaskEquipmentDetailByJobID(int jobID);

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/05
        /// 
        /// Gets a list of equipment assigned to a particular task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        List<Equipment> RetrieveAssignedEquipmentByTaskID(int taskID);

        /// <summary>
        /// Sam Dramstad
        /// Created 2018/04/11
        /// 
        /// Adds a specified Equipment to a TaskEquipment list.
        /// </summary>
        /// 
        bool AddEquipmentToTaskEquipment(int equipmentID, int jobID, int taskTypeEquipmentNeedID);

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/19
        /// 
        /// Gets a list of equipment assigned to a particular task and job
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="jobID"></param>
        /// <returns></returns>
        List<Equipment> RetrieveAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID);

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/13
        /// 
        /// Removes a specified Equipment from a TaskEquipment list.
        /// </summary>
        /// <param name="jobID"></param>
        /// <param name="equipmentID"></param>
        /// <returns></returns>
        int DeleteEquipmentFromTaskEquipment(int jobID, int equipmentID);

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/13
        /// Deletes all equipment assigned to a particular task based on taskID
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        int DeleteAssignedEquipmentByTaskIDAndJobID(int taskID, int jobID);
        int UpdateEquipmentID(int taskEquipmentID, int equipmentID);
        int UpdateEquipmentIDToNull(int taskEquipmentID);
    }
}
