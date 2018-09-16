using System.Collections.Generic;
using DataObjects;

namespace DataAccess
{/// <summary>
    /// Weston Olund
    /// Created 01/26/2018
    /// 
    /// Interface for the Time Off Request Accessor
    /// </summary>
    ///QA Shilin Xiong 4/27/2018  need add the delete time of request
    public interface ITimeOffRequestAccessor
    {
        List<TimeOffRequest> RetrieveTimeOffRequestList();

        /// <summary>
        /// John Miller
        /// Created 2018/03/02
        /// 
        /// Deactivates the TimeOffRequest with the given TimeOffID
        /// </summary>
        /// <param name="timeOffID"></param>
        bool DeactivateTimeOffRequestByID(int timeOffID);

        int CreateTimeOffRequest(TimeOffRequest timeOffRequest);

        int EditTimeOff(TimeOffRequest oldTimeOff, TimeOffRequest newTimeOff);

    }
}