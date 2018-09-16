using System;
using DataAccess;
using DataObjects;
using System.Collections.Generic;
using System.Windows;

namespace Logic
{
    public class InspectionChecklistManager : IInspectionChecklistManager
    {
        private readonly IInspectionChecklistAccessor _inspectionChecklistAccessor;

        public InspectionChecklistManager()
        {
            _inspectionChecklistAccessor = new InspectionChecklistAccessor();
        }

        // For unit tests
        public InspectionChecklistManager(IInspectionChecklistAccessor inspectionChecklistAccessor)
        {
            _inspectionChecklistAccessor = inspectionChecklistAccessor;
        }

        /// <summary>
        /// Adds an InspectionChecklist item 
        /// </summary>
        /// <param name="newItem">The item to add</param>
        /// <returns>the id of the newly added item</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/1
        /// </remarks>
        public int AddInspectionChecklist(InspectionChecklist newItem)
        {
            try
            {
                return _inspectionChecklistAccessor.CreateInspectionChecklist(newItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Creating Inspection Checklist.");
                return 0;
            }
        }

        /// <summary>
        /// Edits an InspectionChecklist Item from an old entry to a new entry
        /// </summary>
        /// <param name="oldItem">The item being edited</param>
        /// <param name="newItem">The item with the new data</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/1
        /// </remarks>
        public int EditInspectionChecklist(InspectionChecklist oldItem, InspectionChecklist newItem)
        {
            try
            {
                return _inspectionChecklistAccessor.EditInspectionChecklistItem(oldItem, newItem);
            } catch (Exception)
            {
                MessageBox.Show("Error Editing Inspection Checklist.");
                return 0;
            }
        }

        /// <summary>
        /// Retrieves an InspectionChecklist by it's ID
        /// </summary>
        /// <param name="id">The ID of the Inspection Checklist to retrieve.</param>
        /// <returns>An InspectionChecklist item from the database</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/2
        /// </remarks>
        public InspectionChecklist RetrieveInspectionChecklistByID(int id)
        {
            try
            {
                return _inspectionChecklistAccessor.RetrieveInspectionChecklistByID(id);
            } catch (Exception)
            {
                MessageBox.Show("Error Retrieving Inspection Checklist By ID.");
                return null;
            }
        }

        /// <summary>
        /// Gets a list of InspectionChecklists. 
        /// </summary>
        /// <returns>Returns a collection of InspectionChecklist</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/1
        /// </remarks>
        public List<InspectionChecklist> RetrieveInspectionChecklistItems()
        {
            try
            {
                return _inspectionChecklistAccessor.RetrieveInspectionChecklistList();
            } catch (Exception)
            {
                MessageBox.Show("Error Retrieving Inspection Checklist List.");
                return null;
            }
        }

        /// <summary>
        /// Deactivates an InspectionChecklist Item in the database
        /// </summary>
        /// <param name="id">The id of the item being deactivated</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/16
        /// </remarks>
        public int DeactivateInspectionChecklist(int id)
        {
            if (id < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException();
            }

            try
            {
                return _inspectionChecklistAccessor.DeactivateInspectionChecklistByID(id);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Inspection Checklist could not be deleted.", e);
            }
        }

        /// <summary>
        /// Deletes an InspectionChecklist Item in the database
        /// </summary>
        /// <param name="id">The id of the item being deleted</param>
        /// <returns>1, if the edit was successful, 0 otherwise.</returns>
        /// <remarks>
        /// Author: Zach Murphy
        /// Date Modified: 2018/2/16
        /// </remarks>
        public int DeleteInspectionChecklist(int id)
        {
            return _inspectionChecklistAccessor.DeleteInspectionChecklistByID(id);
        }
    }
}