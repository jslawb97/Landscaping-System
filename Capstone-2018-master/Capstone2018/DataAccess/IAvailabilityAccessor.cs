using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Badis SAIDANI
    /// Created 2018/02/22
    /// 
    /// Interface for the AvailabilityAccessor
    /// </summary>
    public interface IAvailabilityAccessor
    {
        int CreateAvailable(Availability availability);
        Availability RetrieveAvailabilityByID(int availabilityID);
        int EditAvailability(Availability oldAvailability, Availability newAvailability);
        int DeactivateAvailabilityByID(int availabilityID);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Gets a list of availability from a data store, by a certain employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Availability> RetrieveAvailabilityByEmployeeID(int id);

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Sends a list of updated availability objects to be updated in the database.
        /// Availability records not present in the collection are assumed to be deleted.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="availabilities"></param>
        /// <returns></returns>
        int EditAvailability(int employeeId, IEnumerable<Availability> availabilities);
    }
}
