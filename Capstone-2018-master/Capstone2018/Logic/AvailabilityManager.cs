using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class AvailabilityManager : IAvailabilityManager
    {
        private IAvailabilityAccessor _iAvailabilityAccessor;

        // Constructor for real run
        public AvailabilityManager()
        {
            this._iAvailabilityAccessor = new AvailabilityAccessor();
        }

        // Constructor for unit tests
        public AvailabilityManager(IAvailabilityAccessor iAvailabilityAccessor)
        {
            this._iAvailabilityAccessor = iAvailabilityAccessor;
        }

        public int CreateAvailable(Availability availability)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Badis SAIDANI
        /// Created 2018/02/22
        /// 
        /// Deactivates an Availability
        /// </summary>
        /// <param name="availabilityID">The ID of the availability to be deactivated</param>
        /// <exception cref="SQLException">Deactivate fails</exception>
        /// <returns>Number of Availability deactivated</returns>
        public int DeactivateAvailabilityByID(int availabilityID)
        {
            int result = 0;

            try
            {
                result = _iAvailabilityAccessor.DeactivateAvailabilityByID(availabilityID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public int EditAvailability(Availability oldAvailability, Availability newAvailability)
        {
            throw new NotImplementedException();
        }

        public Availability RetrieveAvailabilityByID(int availabilityID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Calls to edit a particular employee's availability
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="availabilities"></param>
        /// <returns></returns>
        public int EditAvailability(int employeeId, IEnumerable<Availability> availabilities)
        {
            if (availabilities == null)
            {
                throw new ArgumentNullException("The list cannot be null");
            }
            try
            {
                return _iAvailabilityAccessor.EditAvailability(employeeId, availabilities);
            }
            catch (Exception)
            {

                throw new ApplicationException("Error editing availability");
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Gets a list of Availability records for an employee from the accessor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Availability> RetrieveAvailabilityByEmployeeID(int id)
        {
            List<Availability> availabilities = new List<Availability>();

            try
            {
                availabilities = _iAvailabilityAccessor.RetrieveAvailabilityByEmployeeID(id);
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
        }
    }
}
