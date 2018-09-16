using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class PrepChecklistManager : IPrepChecklistManager
    {

        private IPrepChecklistAccessor _prepChecklistAccessor;

        /// Amanda Tampir
        /// Created: 2018/02/07
        /// 
        /// default Manager Constructor for accessing accessor
        /// </summary>
        /// <remarks>QA Shilin Xiong Update 4/20/2018
        public PrepChecklistManager()
        {
            _prepChecklistAccessor = new PrepChecklistAccessor();
        }

        /// Amanda Tampir
        /// Created: 2018/02/07
        /// 
        /// Manager Constructor for accessing accessor
        /// </summary>

        /// <param name="prepChecklistAccessor".> </param>
        /// <remarks>QA Shilin Xiong Update 4/20/2018
        public PrepChecklistManager(IPrepChecklistAccessor prepChecklistAccessor)
        {
            _prepChecklistAccessor = prepChecklistAccessor;
        }

        

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Retreives a list of the Prep CheckLists
        /// </summary>
        /// <returns>prepList</returns>
        /// <remarks>QA Shilin Xiong Update 4/20/2018
        public List<PrepChecklist> RetrievePrepChecklist()
        {
            var prepList = new List<PrepChecklist>();

            try
            {
                prepList = _prepChecklistAccessor.RetrievePrepChecklistList();
            }
            catch (Exception)
            {

                throw;
            }

            return prepList;
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Creates new Prep CheckList
        /// </summary>
        /// <param name="prepChecklist">The new Prep Checklist item</param>
        /// <returns name = "id">The ID of the added Prep Checklist item</returns>
        /// <remarks>QA Shilin Xiong Update 4/20/2018
        public int AddPrepChecklist(PrepChecklist prepChecklist)
        {
            int id = 0;

            try
            {
                id = _prepChecklistAccessor.CreatePrepChecklist(prepChecklist);
            }
            catch (Exception)
            {

                throw;
            }

            return id;
        }


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Deactivates specified Prep Checklist by PrepChecklistID
        /// </summary>
        /// <param name="PrepChecklistID">The Prep Checklist ID</param>
        /// <returns name ="result">The number of records affected</returns>
        public int DeactivatePrepChecklistByID(int prepChecklistID)
        {
            int result = 0;

            if (prepChecklistID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Invalid ID Number");
            }
            try
            {
                result = _prepChecklistAccessor.DeactivatePrepChecklistByID(prepChecklistID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Thing not deleted", ex);
            }

            return result;
        }


        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/2/07
        /// 
        /// Edits a specified Prep Checklist with new data 
        /// </summary>
        /// <param name="oldPrepChecklist">The old Prep Checklist being edited</param>
        /// <param name="newPrepChecklist">The new PrepChecklist containing new data</param>
        /// <returns name = "result">The result is number of records affected</returns>
        public int EditPrepChecklist(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist)
        {
            int result = 0;

            try
            {
                result = _prepChecklistAccessor.EditPrepChecklist(oldPrepChecklist, newPrepChecklist);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }



        /// <summary>
        /// Ammanda Tampir
        /// Created 2018/02/15
        /// 
        /// retreives check lists with id
        /// </summary>
        /// <param name="prepChecklistID"></param>
        /// <returns name="prepListID">A list of prep</returns>
        public PrepChecklist RetrievePrepChecklistByID(int prepChecklistID)
        {
            

            try
            {
                return _prepChecklistAccessor.RetrievePrepChecklistByID(prepChecklistID);
            }
            catch (Exception)
            {

                throw;
            }

        
        }



        ///----may not be using the following functions
        /// <summary>
        /// Ammanda Tampir
        /// Created 2018/02/15
        /// 
        /// retreives check lists with Active status
        /// </summary>
        /// <param name="Active"></param>
        /// <returns name="prepList">A list of prep</returns>
        //public List<PrepChecklist> RetrievePrepChecklistByActive(bool Active = true)
        //{
        //    List<PrepChecklist> prepList = new List<PrepChecklist>();

        //    try
        //    {
        //        prepList = _prepChecklistAccessor.RetrievePrepChecklistByActive(true);
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return prepList;
        //}




        //public int DeletePrepChecklistByID(int prepChecklistID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
