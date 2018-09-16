using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class TimeOffRequestAccessorMock : ITimeOffRequestAccessor
    {
        private List<TimeOffRequest> _timeOffRequestList = new List<TimeOffRequest>();

        /// <summary>
        ///  Weston Olund
        ///  Created on 2018/2/21
        ///  
        /// Mock constructor to add data to the time off request list
        /// </summary>
        /// QA Shilin Xiong 4/27/2018  missing the delete time off request test.
        public TimeOffRequestAccessorMock()
        {
            _timeOffRequestList.Add(new TimeOffRequest()
            {
                TimeOffID = Constants.IDSTARTVALUE,
                EmployeeID = Constants.IDSTARTVALUE,
                StartTime = new DateTime(2017, 12, 05),
                EndTime = new DateTime(2017, 12, 12),
                Approved = false,
            });

            _timeOffRequestList.Add(new TimeOffRequest()
            {
                TimeOffID = Constants.IDSTARTVALUE + 1,
                EmployeeID = Constants.IDSTARTVALUE + 1,
                StartTime = new DateTime(2017, 12, 20),
                EndTime = new DateTime(2017, 12, 30),
                Approved = true,
            });
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/01
        /// 
        /// method to return mock data
        /// </summary>
        /// <param name="timeOffRequest"></param>
        /// <returns></returns>
        public int CreateTimeOffRequest(TimeOffRequest timeOffRequest)
        {
            if (timeOffRequest.Approved)
                return 1;
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/02/22
        /// 
        /// Method to return mock data
        /// </summary>
        /// <returns></returns>
        public List<TimeOffRequest> RetrieveTimeOffRequestList()
        {
            return _timeOffRequestList;
        }



        public int EditTimeOff(TimeOffRequest oldTimeOff, TimeOffRequest newTimeOff)
        {
            int result = 0;

            TimeOffRequest timeOff = _timeOffRequestList.Find(t => t.TimeOffID == oldTimeOff.TimeOffID && t.TimeOffID == newTimeOff.TimeOffID);
            if (timeOff != null)
            {
                timeOff = newTimeOff;

                if (timeOff.Equals(newTimeOff))
                {
                    result = 1;
                }

            }
            else
            {
                throw new ApplicationException("No time off requests with that ID found.");
            }

            return result;
        }

        /// <summary>
        /// Created by John Miller
        /// Last Updated 2018/03/02
        /// 
        /// Edits mock TimeOffRequest objects.
        /// </summary>
        /// <param name="oldTimeOffRequest"></param>
        /// <param name="newTimeOffRequest"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool EditTimeOffRequest(TimeOffRequest oldTimeOffRequest, TimeOffRequest newTimeOffRequest)
        {
            bool found = false;
            this._timeOffRequestList.ForEach(timeOffList =>
            {
                if (timeOffList == oldTimeOffRequest)
                {
                    timeOffList.EmployeeID = newTimeOffRequest.EmployeeID;
                    timeOffList.StartTime = newTimeOffRequest.StartTime;
                    timeOffList.EndTime = newTimeOffRequest.EndTime;
                    timeOffList.Approved = newTimeOffRequest.Approved;
                    timeOffList.Active = newTimeOffRequest.Active;
                    found = true;
                }
            });
            return found;
        }

        /// <summary>
        /// Created by John Miller
        /// Last updated 2018/03/02
        /// 
        /// Retrieves a time off request by EmployeeID
        /// </summary>
        /// <param name="id"></param>
        public TimeOffRequest RetrieveTimeOffRequestByEmployeeID(int id)
        {
            return this._timeOffRequestList.Find(timeOffRequestList =>
                timeOffRequestList.EmployeeID.Equals(id));
        }

        /// <summary>
        /// Created by John Miller
        /// Last updated 2018/03/02
        /// 
        /// Retrieves a time off request by TimeOffID
        /// </summary>
        /// <param name="id"></param>
        public TimeOffRequest RetrieveTimeOffRequestByID(int id)
        {
            return this._timeOffRequestList.Find(timeOffRequestList =>
                timeOffRequestList.TimeOffID.Equals(id));
        }

        /// <summary>
        /// Created by John Miller
        /// Last updated 2018/03/02
        /// 
        /// Deactivates a TimeOffRequest By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if successful, false if unsuccessful</returns>
        public bool DeactivateTimeOffRequestByID(int id)
        {
            try
            {
                RetrieveTimeOffRequestByID(id).Active = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
