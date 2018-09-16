using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    ///     Badis SAIDANI
    ///     Created 2018/02/22
    ///     
    ///     Interface for the AvailabilityManager
    /// </summary>
    public interface IAvailabilityManager
    {
        int CreateAvailable(Availability availability);
        Availability RetrieveAvailabilityByID(int availabilityID);
        int EditAvailability(Availability oldAvailability, Availability newAvailability);
        int DeactivateAvailabilityByID(int availabilityID);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Gets a list of availability of an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Availability> RetrieveAvailabilityByEmployeeID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Submits edit availability information
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="availabilities"></param>
        /// <returns></returns>
        int EditAvailability(int employeeId, IEnumerable<Availability> availabilities);
    }
}
