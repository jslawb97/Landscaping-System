using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/04
    /// 
    /// Interface for the MaintenanceChecklistAccessor
    /// </summary> QA add,edit, delete MaintenanceChecklist ShilinXiong T 5/4//18
    public interface IMaintenanceChecklistAccessor
    {
        List<MaintenanceChecklist> RetrieveMaintenanceChecklistList();

        MaintenanceChecklist RetrieveMaintenanceChecklistByID(int id);

        int EditMaintenanceChecklistItem(MaintenanceChecklist oldItem, MaintenanceChecklist newItem);

        int CreateMaintenanceChecklist(MaintenanceChecklist newItem);

        int DeactivateMaintenanceChecklistByID(int id);

    }
}
