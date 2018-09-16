using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{
    public interface IPrepChecklistManager
    {
        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Retreives a list of the Prep CheckLists
        /// </summary>
        /// <returns>Prep Checklist</returns>
        /// 
        /// <remarks>QA Shilin Xiong Update 4/20/2018  
        List<PrepChecklist> RetrievePrepChecklist();


        /// Amanda Tampir
        /// Created: 2018/02/07
        /// 
        /// add prep check list 
        /// <param name="prepChecklist">The PrepChecklist</param>
        /// </summary>
        int AddPrepChecklist(PrepChecklist prepChecklist);


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Edits a specified Prep Checklist with new data 
        /// </summary>
        /// <param name="oldPrepChecklist">The old Prep Checklist being edited</param>
        /// <param name="newPrepChecklist">The new PrepChecklist containing new data</param>
        /// <returns>The number of records affected</returns>
        ///     /// <remarks>QA Shilin Xiong Update 4/20/2018  
        int EditPrepChecklist(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist);


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Deactivates specified Prep Checklist by PrepChecklistID
        /// </summary>
        /// <param name="PrepChecklistID">The Prep Checklist ID</param>
        /// <returns>The number of records affected</returns>
        ///     /// <remarks>QA Shilin Xiong Update 4/20/2018  
        int DeactivatePrepChecklistByID(int prepChecklistID);




        //<remarks>QA Shilin Xiong Update 4/20/2018
       PrepChecklist RetrievePrepChecklistByID(int prepChecklistID);

        //List<PrepChecklist> RetrievePrepChecklistByActive(bool Active);

        //------may not be using the following functions        
        //int EditPrepChecklistActive(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist);
        //int DeletePrepChecklistByID(int prepChecklistID);  
    }
}
