using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessMocks
{
    public class PrepChecklistAccessorMock : IPrepChecklistAccessor
    {

        private List<PrepChecklist> _prepChecklist = new List<PrepChecklist>();

        /// <remarks>QA Shilin Xiong Update 4/20/2018  
        public PrepChecklistAccessorMock()
        {
            _prepChecklist.Add(new PrepChecklist()
            {
                PrepChecklistID = 1000000,
                Name = "PrepChecklistTest1",
                Description = "Lengthy text description here 1",
                Active = true
            });
            _prepChecklist.Add(new PrepChecklist()
            {
                PrepChecklistID = 1000001,
                Name = "PrepChecklistTest2",
                Description = "Lengthy text description here 2",
                Active = true
            });
            _prepChecklist.Add(new PrepChecklist()
            {
                PrepChecklistID = 1000002,
                Name = "PrepChecklistTest3",
                Description = "Lengthy text description here 3",
                Active = true
            });
        }

        /// <remarks>QA Shilin Xiong Update 4/20/2018  
        public int CreatePrepChecklist(PrepChecklist prepChecklist)
        {
            try
            {
                this._prepChecklist.Add(prepChecklist);
                return 1;
            }catch (Exception)
            {
                return 0;
            }

        }
        /// <remarks>QA Shilin Xiong Update 4/20/2018  
        public int DeactivatePrepChecklistByID(int prepChecklistID)
        {
            
            try
            {
                RetrievePrepChecklistByID(prepChecklistID).Active = false;

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }

           
        }

        public int EditPrepChecklist(PrepChecklist oldPrepChecklist, PrepChecklist newPrepChecklist)
        {
            var found = 0;

            this._prepChecklist.ForEach(prepchecklist =>
            {
                if (prepchecklist == oldPrepChecklist)
                {
                    prepchecklist.Name = newPrepChecklist.Name;
                    prepchecklist.Description = newPrepChecklist.Description;
                    prepchecklist.Active = newPrepChecklist.Active;
                    found = 1;
                }
            });
            return found;
        }

        public PrepChecklist RetrievePrepChecklistByID(int PrepChecklistID)
        {
            return this._prepChecklist.Find(prepchecklist => prepchecklist.PrepChecklistID.Equals(PrepChecklistID));
        }

        public List<PrepChecklist> RetrievePrepChecklistList()
        {
            return _prepChecklist;
        }
    }
}
