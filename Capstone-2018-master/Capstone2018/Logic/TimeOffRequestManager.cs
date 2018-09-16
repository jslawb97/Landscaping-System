using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class TimeOffRequestManager : ITimeOffRequestManager
    {
        // Constructor for actual run
        ITimeOffRequestAccessor _timeOffRequestAccessor;
        IEmployeeAccessor _employeeAccessor;
        public TimeOffRequestManager()
        {
            _timeOffRequestAccessor = new TimeOffRequestAccessor();
            _employeeAccessor = new EmployeeAccessor();
        }

        // Constructor for unit tests
        public TimeOffRequestManager(ITimeOffRequestAccessor timeOffRequestAccessor)
        {
            _timeOffRequestAccessor = timeOffRequestAccessor;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/01
        /// 
        /// Method to create a time off request
        /// </summary>
        /// <param name="timeOffRequest"></param>
        /// <returns></returns>
        public bool CreateTimeOffRequest(TimeOffRequest timeOffRequest)
        {
            if (timeOffRequest.EmployeeID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value.");
            }
            try
            {
                return (0 != _timeOffRequestAccessor.CreateTimeOffRequest(timeOffRequest));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to return a list of Time off requests
        /// </summary>
        /// <returns></returns>
        public List<TimeOffRequest> RetrieveTimeOffRequestList()
        {
            List<TimeOffRequest> timeoffRequestList = null;
            try
            {
                timeoffRequestList = _timeOffRequestAccessor.RetrieveTimeOffRequestList();
            }
            catch (Exception)
            {
                throw;
            }
            return timeoffRequestList;
        }


        /// <summary>
        /// Jacob Conley
        /// Created 2018/03/01
        /// 
        /// Method to update time off by ID
        /// </summary>
        /// <param name="oldTimeOff"></param>
        /// <param name="newTimeOff"></param>
        /// <returns>Rows affected</returns>
        public int EditTimeOff(TimeOffRequest oldTimeOff, TimeOffRequest newTimeOff)
        {
            if (oldTimeOff.TimeOffID < Constants.IDSTARTVALUE || newTimeOff.TimeOffID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad ID Value");
            }

            int results = 0;

            try
            {
                results = _timeOffRequestAccessor.EditTimeOff(oldTimeOff, newTimeOff);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem connecting to the server.", ex);
            }

            return results;

        }

        /// <summary>
        /// Created by John Miller
        /// Last Updated 2018/03/02
        /// 
        /// Deactivates a TimeOffRequest in the database
        /// </summary>
        /// <param name="timeOffID"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool DeactivateTimeOffRequestByID(int timeOffID)
        {
            var result = false;

            try
            {
                result = _timeOffRequestAccessor.DeactivateTimeOffRequestByID(timeOffID);
                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
