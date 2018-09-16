using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class PersonalEquipmentManager : IPersonalEquipmentManager
    {
        IPersonalEquipmentAccessor _peqAccessor;

        // For live implementaton
        public PersonalEquipmentManager()
        {
            _peqAccessor = new PersonalEquipmentAccessor();
        }

        // For test implementation
        public PersonalEquipmentManager(IPersonalEquipmentAccessor accessor)
        {
            _peqAccessor = accessor;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Adds an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int CreatePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int rowCount;

            if (employeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Employee ID");
            }
            if (pEquipmentID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Equipment ID");
            }

            try
            {
                rowCount = _peqAccessor.CreatePersonalEquipmentAssignment(employeeID, pEquipmentID);
                if (rowCount > 0)
                {
                    rowCount += _peqAccessor.EditPersonalEquipmentAssignment(pEquipmentID, true);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Deletes an assignment record for PersonalEquipment item
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int DeletePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int rowCount;

            if (employeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Employee ID");
            }
            if (pEquipmentID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Equipment ID");
            }

            try
            {
                rowCount = _peqAccessor.DeletePersonalEquipmentAssignment(employeeID, pEquipmentID);
                if (rowCount > 0)
                {
                    rowCount += _peqAccessor.EditPersonalEquipmentAssignment(pEquipmentID, false);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Updates the assigned status of a PersonalEquipment item 
        /// </summary>
        /// <param name="pEquipmentID"></param>
        /// <param name="assigned"></param>
        /// <returns></returns>
        public int EditPersonalEquipmentAssignment(int pEquipmentID, bool assigned)
        {
            int rowCount;

            if (pEquipmentID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Equipment ID");
            }

            try
            {
                rowCount = _peqAccessor.EditPersonalEquipmentAssignment(pEquipmentID, assigned);
            }
            catch (Exception)
            {

                throw;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment assigned to an employee
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrieveAssignedPersonalEquipmentByEmployeeID(int employeeID)
        {
            List<PersonalEquipment> peqList = null;

            if (employeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Invalid Employee ID");
            }

            try
            {
                peqList = _peqAccessor.RetrieveAssignedPersonalEquipmentByEmployeeID(employeeID);
            }
            catch (Exception)
            {

                throw;
            }

            return peqList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Retrieves all PersonalEquipment with the given assignment value
        /// </summary>
        /// <param name="assigned"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrievePersonalEquipmentByAssigned(bool assigned)
        {
            List<PersonalEquipment> peqList = null;

            try
            {
                peqList = _peqAccessor.RetrievePersonalEquipmentByAssigned(assigned);
            }
            catch (Exception)
            {

                throw;
            }

            return peqList;
        }
    }
}
