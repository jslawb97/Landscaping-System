using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/04
    /// 
    /// Interface for the MaintenanceChecklistManager
    /// </summary>
    public interface IMaintenanceChecklistManager
    {
        List<MaintenanceChecklist> RetrieveMaintenanceChecklistList();

        MaintenanceChecklist RetrieveMaintenanceChecklistByID(int id);

        int EditMaintenanceChecklist(MaintenanceChecklist oldItem, MaintenanceChecklist newItem);

        int AddMaintenanceChecklist(MaintenanceChecklist newItem);
        int DeactivateMaintenanceChecklist(int id);
    }
}
