using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Reuben Cassell
    /// Created 4-27-2018
    /// 
    /// Manager class for PersonalEquipment
    /// </summary>
    public interface IPersonalEquipmentManager
    {
        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment with the given assignment value
        /// </summary>
        /// <param name="assigned"></param>
        /// <returns></returns>
        List<PersonalEquipment> RetrievePersonalEquipmentByAssigned(bool assigned);

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment assigned to an employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        List<PersonalEquipment> RetrieveAssignedPersonalEquipmentByEmployeeID(int employeeID);

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Updates the assigned status of a PersonalEquipment item 
        /// </summary>
        /// <param name="pEquipmentID"></param>
        /// <param name="assigned"></param>
        /// <returns></returns>
        int EditPersonalEquipmentAssignment(int pEquipmentID, bool assigned);

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Deletes an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        int DeletePersonalEquipmentAssignment(int employeeID, int pEquipmentID);

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Adds an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        int CreatePersonalEquipmentAssignment(int employeeID, int pEquipmentID);
    }
}
