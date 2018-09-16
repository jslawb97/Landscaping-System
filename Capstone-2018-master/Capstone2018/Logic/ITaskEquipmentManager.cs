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
    /// Manager interface for task equipment
    /// </summary>
    public interface ITaskEquipmentManager
    {
        /// <summary>
        /// Brady Feller
        /// Created 2018/04/05
        /// 
        /// Gets a list of TaskEquipmentDetail objects by the associated JobID
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
        /// Sam Dramstad
        /// Created 2018/04/11
        /// 
        /// Adds a specified Equipment to a TaskEquipment list.
        /// </summary>
        /// 
        bool AddEquipmentToTaskEquipment(int equipmentID, int jobID, int taskTypeEquipmentNeedID);

        /// <summary>
        /// Jacob Conley
        /// Created: 2018/04/14
        /// 
        /// Removes a specific Equipment from a TaskEquipment list.
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
        bool UpdateEquipmentID(int taskEquipmentID, int equipmentID);
        bool UpdateEquipmentIDToNull(int taskEquipmentID);
    }
}
