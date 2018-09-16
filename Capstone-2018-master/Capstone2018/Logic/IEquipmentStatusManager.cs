using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Jacob Slaubaugh
    /// Created 2018/02/15
    /// 
    /// Interface for EquipmentStatus
    /// </summary>
    /// <returns></returns>
    public interface IEquipmentStatusManager
    {
        List<EquipmentStatus> RetrieveEquipmentStatusList();
        int DeleteEquipmentStatus(string equipmentStatusID);
        bool AddEquipmentStatus(EquipmentStatus equipmentStatus);
        EquipmentStatus RetrieveEquipmentStatusByID(string equipmentStatusID);
        int EditEquipmentStatus(EquipmentStatus oldEquipmentStatus, EquipmentStatus newEquipmentStatus);
    }
}
