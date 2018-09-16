using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class AvailabilityAccessorMock : IAvailabilityAccessor
    {
         private List<Availability> _availabilityList = new List<Availability>();

        // <summary>
        /// Badis Saidani
        /// Created 2018/02/22
        /// 
        /// Mock constructor to add data to the Availability list
        /// </summary>
        public AvailabilityAccessorMock()
        {
            _availabilityList = new List<Availability>() {
                new Availability(){AvailabilityID = 1000000,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 07:00:00"), EndTime=Convert.ToDateTime("2018-02-18 13:00:00")},
                new Availability(){AvailabilityID = 1000001,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 07:00:00"), EndTime=Convert.ToDateTime("2018-02-18 15:00:00")},
                new Availability(){AvailabilityID = 1000002,EmployeeID=1000000,StartTime=Convert.ToDateTime("2018-02-18 07:00:00"), EndTime=Convert.ToDateTime("2018-02-18 11:00:00")}

            };
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/02/22
        /// 
        /// Mock method to add Availability
        /// </summary>
        /// <returns></returns>
        public int CreateAvailable(Availability availability)
        {
            try
            {
                this._availabilityList.Add(availability);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeactivateAvailabilityByID(int availabilityID)
        {
            throw new NotImplementedException();
        }

        public int EditAvailability(Availability oldAvailability, Availability newAvailability)
        {
            throw new NotImplementedException();
        }

        public int EditAvailability(int employeeId, IEnumerable<Availability> availabilities)
        {
            int rowCount = 0;
            
            
            _availabilityList.RemoveAll(x => x.EmployeeID == employeeId);
            _availabilityList.AddRange(availabilities);
            rowCount += availabilities.Count();
            return rowCount;
        }

        public List<Availability> RetrieveAvailabilityByEmployeeID(int id)
        {
            List<Availability> avail = new List<Availability>();
            foreach (var item in _availabilityList)
            {
                if(item.EmployeeID == id)
                {
                    avail.Add(item);
                }
            }

            return avail;
        }

        /// <summary>
        /// Badis Saidani
        /// Created on 2018/02/22
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="AvailabilityID"></param>
        /// <returns></returns>
        public Availability RetrieveAvailabilityByID(int availabilityID)
        {
            Availability availability = null;
            foreach (var emp in _availabilityList)
            {
                if (emp.AvailabilityID == availabilityID)
                {
                    availability = emp;
                }
            }
            if (availability == null)
            {
                throw new ApplicationException("Availability record not found.");
            }
            return availability;
        }

        public List<Availability> RetrieveAvailabilityList()
        {
            return _availabilityList;
        }

    }
}
