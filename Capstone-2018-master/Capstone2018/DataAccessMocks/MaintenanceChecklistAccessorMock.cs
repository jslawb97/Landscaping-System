using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class MaintenanceChecklistAccessorMock : IMaintenanceChecklistAccessor
    {
        private List<MaintenanceChecklist> _maintenanceChecklists = new List<MaintenanceChecklist>();

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock constructor to add data to the MaintenanceChecklist list
        /// </summary>
        public MaintenanceChecklistAccessorMock()
        {
            _maintenanceChecklists.Add(new MaintenanceChecklist()
            {
                MaintenanceChecklistID = 1000000,
                Description = "TestMaintenanceChecklist 1"
            });
            _maintenanceChecklists.Add(new MaintenanceChecklist()
            {
                MaintenanceChecklistID = 1000001,
                Description = "TestMaintenanceChecklist 2"
            });
            _maintenanceChecklists.Add(new MaintenanceChecklist()
            {
                MaintenanceChecklistID = 1000002,
                Description = "TestMaintenanceChecklist 3"
            });
        }

        public int CreateMaintenanceChecklist(MaintenanceChecklist newItem)
        {
            throw new NotImplementedException();
        }

        public int DeactivateMaintenanceChecklistByID(int id)
        {
            throw new NotImplementedException();
        }

        public int EditMaintenanceChecklistItem(MaintenanceChecklist oldItem, MaintenanceChecklist newItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock method to retrieve a MaintenanceChecklist by ID
        /// </summary>
        /// <param name="MaintenanceChecklistID"></param>
        /// <returns></returns>
        public MaintenanceChecklist RetrieveMaintenanceChecklistByID(int maintenanceChecklistID)
        {
            MaintenanceChecklist maintenanceChecklist = null;

            foreach(MaintenanceChecklist mc in _maintenanceChecklists)
            {
                if(mc.MaintenanceChecklistID == maintenanceChecklistID)
                {
                    maintenanceChecklist = mc;
                    break;
                }
            }

            return maintenanceChecklist;
        }

        /// <summary>
        /// James McPherson
        /// 2018/02/04
        /// 
        /// Mock method to retrieve a list of MaintenanceChecklists
        /// </summary>
        /// <returns></returns>
        public List<MaintenanceChecklist> RetrieveMaintenanceChecklistList()
        {
            return _maintenanceChecklists;
        }
    }
}
