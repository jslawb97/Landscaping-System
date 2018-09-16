using System;
using System.Collections.Generic;
using DataAccess;
using DataObjects;

namespace DataAccessMocks {
    public class InspectionChecklistAccessorMock : IInspectionChecklistAccessor {
        private readonly List<InspectionChecklist> _checklistList = new List<InspectionChecklist>();

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// </summary>
        public InspectionChecklistAccessorMock() {
            this._checklistList.Add(new InspectionChecklist {
                InspectionChecklistID = Constants.IDSTARTVALUE,
                Description =
                    "Tractor Inspection CheckList: Tires, Fuel, Oil, Pedals, Grease Fittings, Seals and Belts, Guages, Leaks"
            });
            this._checklistList.Add(new InspectionChecklist {
                InspectionChecklistID = Constants.IDSTARTVALUE + 1,
                Description = "Lawnmower Inspection CheckList: Fuel, Tires, Blades, Belts, Deck, Oil"
            });
            this._checklistList.Add(new InspectionChecklist {
                InspectionChecklistID = Constants.IDSTARTVALUE + 2,
                Description =
                    "Wheel Barrel Inspection CheckList: Hand Grips, Washers and Bolts, Frame, Bracking, Bucket, Stands, Wheels"
            });
            this._checklistList.Add(new InspectionChecklist {
                InspectionChecklistID = Constants.IDSTARTVALUE + 3,
                Description =
                    "Mini Excavator Inspection CheckList: Guages, Lights, Horn, Mirrors, Oil, Fuel, Radiator, Battery, Main Frame, Leaks, Hoses, Fan, Belts"
            });
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Gets all checklist items
        /// </summary>
        public List<InspectionChecklist> RetrieveInspectionChecklistList() {
            return this._checklistList;
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Gets a checklist item by id
        /// </summary>
        public InspectionChecklist RetrieveInspectionChecklistByID(int id) {
            return this._checklistList.Find(checklist => checklist.InspectionChecklistID.Equals(id));
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Replces the data in an checklist item
        /// </summary>
        public int EditInspectionChecklistItem(InspectionChecklist oldItem, InspectionChecklist newItem) {
            var found = 0;

            this._checklistList.ForEach(checklist => {
                if (checklist == oldItem)
                {
                    checklist.Name = newItem.Name;
                    checklist.Description = newItem.Description;
                    checklist.Active = newItem.Active;
                    found = 1;
                }
            });

            return found;
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Adds a new checklist item to the list.
        /// </summary>
        public int CreateInspectionChecklist(InspectionChecklist newItem) {
            try {
                this._checklistList.Add(newItem);
                return 1;
            } catch (Exception) {
                return 0;
            }
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Deactivates a checklist item by id
        /// </summary>
        public int DeactivateInspectionChecklistByID(int id) {
            try {
                RetrieveInspectionChecklistByID(id).Active = false;
                return 1;
            } catch (Exception) {
                return 0;
            }
        }

        /// <summary>
        /// Created by: Zach Murphy
        /// Last Updated: 2/22/2018
        /// 
        /// Deletes a checklist item by id
        /// </summary>
        public int DeleteInspectionChecklistByID(int id) {
            try {
                this._checklistList.Remove(RetrieveInspectionChecklistByID(id));
                return 1;
            } catch (Exception) {
                return 0;
            }
        }
    }
}