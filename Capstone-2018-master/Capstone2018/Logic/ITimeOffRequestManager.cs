using System.Collections.Generic;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Weston Olund
    /// Created 2018/01/26
    /// 
    /// Interface for the TimeOffRequestManager
    /// </summary>
    /// QA Shilin Xiong 4/27/2018  need add the delete time off request.
    public interface ITimeOffRequestManager
    {
        List<TimeOffRequest> RetrieveTimeOffRequestList();

        bool CreateTimeOffRequest(TimeOffRequest timeOffRequest);

        int EditTimeOff(TimeOffRequest oldTimeOff, TimeOffRequest newTimeOff);

        /// <summary>
        /// Deactivates a TimeOffRequest by its ID
        /// </summary>
        /// <param name="timeOffID"></param>
        /// <returns>True if successful, false if unsuccessful</returns>
        /// <remarks>
        /// John Miller
        /// Updated 2018/03/02
        /// </remarks>
        bool DeactivateTimeOffRequestByID(int timeOffID);
    }
}