using DataObjects;
using System.Collections.Generic;

namespace Logic {
    public interface IInspectionChecklistManager {

        List<InspectionChecklist> RetrieveInspectionChecklistItems();

        InspectionChecklist RetrieveInspectionChecklistByID(int id);

        int EditInspectionChecklist(InspectionChecklist oldItem, InspectionChecklist newItem);

        int AddInspectionChecklist(InspectionChecklist newItem);

        int DeactivateInspectionChecklist(int id);

    }
}
