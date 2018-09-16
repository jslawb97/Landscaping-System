using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{

    /// <summary>
    /// Amanda Tampir
    /// Created: 2018/2/01
    /// 
    /// Public interface for accessing Prep CheckList data
    /// </summary>
    public interface IPrepChecklistAccessor
    {
        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Creates new Prep CheckList
        /// </summary>
        /// <param name="prepChecklist">The new Prep Checklist item</param>
        /// <returns>The ID of the added Prep Checklist item</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        int CreatePrepChecklist(PrepChecklist prepChecklist);

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retreives a list of the Prep CheckLists
        /// </summary>
        /// <returns>Prep Checklist</returns>
        /// /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        List<PrepChecklist> RetrievePrepChecklistList();
        

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Edits Prep Checklist with new data 
        /// </summary>
        /// <param name="oldPrepChecklist">The old Prep Checklist being edited</param>
        /// <param name="newPrepChecklist">The new PrepChecklist containing new data</param>
        /// <returns>The number of records affected</returns>
        /// /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        int EditPrepChecklist(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist);


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Deactivates specified Prep Checklist by PrepChecklist ID
        /// </summary>
        /// <param name="PrepChecklistID">The Prep Checklist ID</param>
        /// <returns>The number of records affected</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        int DeactivatePrepChecklistByID(int PrepChecklistID);



        //may not need code below at this time.

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        ///  ---------------------------are we keeping this delete function
        /// Delete specified Prep Checklist by specified ID 
        /// </summary>
        /// <param name="PrepChecklistID">The Prep Checklist ID</param>
        /// <returns>The number of records affected</returns>
        //int DeletePrepChecklistByID(int PrepChecklistID);


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retrieves all active Prep CheckList
        /// </summary>
        /// <param name="Active">Boolean Active for Prep Checklist </param>
        /// <returns>All active Prep Checklists</returns>
        //List<PrepChecklist> RetrievePrepChecklistByActive(bool Active);

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/01
        /// 
        /// Retrieves Prep CheckList by specified ID number
        /// </summary>
        /// <param name="prepChecklistID">The Prep Checklist ID</param>
        /// <returns>Prep Checklist item with the specified ID</returns>
        /// <remarks>QA Shilin Xiong 4/20/2018 need add the checkbox and No test past
        PrepChecklist RetrievePrepChecklistByID(int PrepChecklistID);

    }
}
