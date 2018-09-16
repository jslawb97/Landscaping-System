using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class PersonalEquipmentAccessorMock : IPersonalEquipmentAccessor
    {
        private List<PersonalEquipment> _peList = new List<PersonalEquipment>();
        private List<EmployeePersonalEquipment> _assignmentList = new List<EmployeePersonalEquipment>();

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Constructor for testing
        /// </summary>
        public PersonalEquipmentAccessorMock()
        {
            _peList.Add(new PersonalEquipment
            {
                PersonalEquipmentID = Constants.IDSTARTVALUE,
                PersonalEquipmentType = "Tool",
                Name = "Item1",
                Description = "Description1",
                PersonalEquipmentStatus = "Status1",
                Assigned = true
            });

            _peList.Add(new PersonalEquipment
            {
                PersonalEquipmentID = Constants.IDSTARTVALUE + 1,
                PersonalEquipmentType = "Tool",
                Name = "Item2",
                Description = "Description2",
                PersonalEquipmentStatus = "Status2",
                Assigned = true
            });

            _peList.Add(new PersonalEquipment
            {
                PersonalEquipmentID = Constants.IDSTARTVALUE + 2,
                PersonalEquipmentType = "Tool",
                Name = "Item2",
                Description = "Description2",
                PersonalEquipmentStatus = "Status2",
                Assigned = false
            });

            _assignmentList.Add(new EmployeePersonalEquipment
            {
                EmployeeID = Constants.IDSTARTVALUE,
                PersonalEquipmentID = Constants.IDSTARTVALUE
            });

            _assignmentList.Add(new EmployeePersonalEquipment
            {
                EmployeeID = Constants.IDSTARTVALUE + 1,
                PersonalEquipmentID = Constants.IDSTARTVALUE + 1
            });

            _assignmentList.Add(new EmployeePersonalEquipment
            {
                EmployeeID = Constants.IDSTARTVALUE + 2,
                PersonalEquipmentID = Constants.IDSTARTVALUE + 2
            });
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Mock of CreatePersonalEquipmentAssignment method
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int CreatePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int listCount = _assignmentList.Count();
            int rowCount;

            _assignmentList.Add(new EmployeePersonalEquipment
            {
                EmployeeID = employeeID,
                PersonalEquipmentID = pEquipmentID
            });

            if (_assignmentList.Count > listCount)
            {
                rowCount = 1;
            }
            else
            {
                rowCount = 0;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Mock of DeletePersonalEquipmentAssignment method
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int DeletePersonalEquipmentAssignment(int employeeID, int pEquipmentID)
        {
            int listCount = _assignmentList.Count();
            int rowCount = 0;

            EmployeePersonalEquipment assignment = _assignmentList.Find(a => a.PersonalEquipmentID == pEquipmentID && a.EmployeeID == employeeID);

            if (_assignmentList.Remove(assignment))
            {
                rowCount = 1;
            }

            return rowCount;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Mock of RetrieveAssignedPersonalEquipmentByEmployeeID method
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrieveAssignedPersonalEquipmentByEmployeeID(int employeeID)
        {
            List<EmployeePersonalEquipment> assignmentList = _assignmentList.FindAll(a => a.EmployeeID == employeeID);
            List<PersonalEquipment> employeesEquipment = new List<PersonalEquipment>();
            List<int> assignedEquipmentIDs = new List<int>();
            PersonalEquipment equipment;

            foreach (var assignment in assignmentList)
            {
                assignedEquipmentIDs.Add(assignment.PersonalEquipmentID);
            }



            foreach (var id in assignedEquipmentIDs)
            {

                equipment = _peList.Find(p => p.PersonalEquipmentID == id);
                employeesEquipment.Add(equipment);
            }

            return employeesEquipment;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Mock of RetrievePersonalEquipmentByAssigned method
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public List<PersonalEquipment> RetrievePersonalEquipmentByAssigned(bool assigned)
        {
            List<PersonalEquipment> unassignedList = _peList.FindAll(p => p.Assigned == assigned);

            return unassignedList;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4-27-2018
        /// 
        /// Mock of EditPersonalEquipmentAssignment method
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="pEquipmentID"></param>
        /// <returns></returns>
        public int EditPersonalEquipmentAssignment(int pEquipmentID, bool assigned)
        {
            int rowCount = 0;

            PersonalEquipment eqToEdit = _peList.Find(p => p.PersonalEquipmentID == pEquipmentID);
            int index = _peList.IndexOf(eqToEdit);
            _peList[index].Assigned = assigned;

            if (_peList[index].Assigned == assigned)
            {
                rowCount = 1;
            }


            return rowCount;
        }
    }
}
