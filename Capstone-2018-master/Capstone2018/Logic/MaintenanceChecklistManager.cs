using System;
using System.Collections.Generic;
using System.Windows;
using DataObjects;
using DataAccess;

namespace Logic
{

    //QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
    public class MaintenanceChecklistManager : IMaintenanceChecklistManager
    {
		

       // QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        private readonly IMaintenanceChecklistAccessor _maintenanceChecklistAccessor;

        // Constructor for real run
        public MaintenanceChecklistManager()
        {
            _maintenanceChecklistAccessor = new MaintenanceChecklistAccessor();
        }

        // Constructor for test run
        public MaintenanceChecklistManager(IMaintenanceChecklistAccessor maintenanceChecklistAccessor)
        {
            _maintenanceChecklistAccessor = maintenanceChecklistAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Method to retrieve a list of MaintenanceChecklists
        /// </summary>
        /// <returns>A list of MaintenanceChecklists</returns>
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        public List<MaintenanceChecklist> RetrieveMaintenanceChecklistList()
        {
            List<MaintenanceChecklist> maintenanceChecklistList;

            try
            {
                maintenanceChecklistList = _maintenanceChecklistAccessor.RetrieveMaintenanceChecklistList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Retrieving Maintenance Checklist.");
                return null;
            }

            return maintenanceChecklistList;
        }
		
        /// <summary>
        /// Adds an MaintenanceChecklist item 
        /// </summary>
        /// <param name="newItem">The item to add</param>
        /// <returns>the id of the newly added item</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/2
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        /// </remarks>
        public int AddMaintenanceChecklist(MaintenanceChecklist newItem)
        {
            try
            {
                return _maintenanceChecklistAccessor.CreateMaintenanceChecklist(newItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Adding Maintenance Checklist.");
                return 0;
            }
        }

        /// <summary>
        /// Edits an MaintenanceChecklist Item from an old entry to a new entry
        /// </summary>
        /// <param name="oldItem">The item being edited</param>
        /// <param name="newItem">The item with the new data</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/2
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        /// </remarks>
        public int EditMaintenanceChecklist(MaintenanceChecklist oldItem, MaintenanceChecklist newItem)
        {
            try
            {
                return _maintenanceChecklistAccessor.EditMaintenanceChecklistItem(oldItem, newItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Editing Maintenance Checklist.");
                return 0;
            }
        }

        /// <summary>
        /// Retrieves a MaintenanceChecklist by it's ID
        /// </summary>
        /// <param name="id">The ID of the MaintenanceChecklist to retrieve.</param>
        /// <returns>An MaintenanceChecklist item from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        /// </remarks>
        public MaintenanceChecklist RetrieveMaintenanceChecklistByID(int id)
        {
            try
            {
                return _maintenanceChecklistAccessor.RetrieveMaintenanceChecklistByID(id);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Retrieving Maintenance Checklist By ID.");
                return null;
            }
        }
        
        /// <summary>
        /// Deactivates a MaintenanceChecklist item in the database
        /// </summary>
        /// <param name="id">The id of the item being deactivated</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/16
        /// QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
        /// </remarks>
        public int DeactivateMaintenanceChecklist(int id)
        {
            try
            {
                return _maintenanceChecklistAccessor.DeactivateMaintenanceChecklistByID(id);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Adding Maintenance Checklist.");
                return 0;
            }
        }

    }
}
