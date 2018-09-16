using DataObjects;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IInspectionChecklistAccessor
    {

        List<InspectionChecklist> RetrieveInspectionChecklistList();

        InspectionChecklist RetrieveInspectionChecklistByID(int id);

        int EditInspectionChecklistItem(InspectionChecklist oldItem, InspectionChecklist newItem);

        int CreateInspectionChecklist(InspectionChecklist newItem);

        int DeactivateInspectionChecklistByID(int id);

        int DeleteInspectionChecklistByID(int id);

    }
}
