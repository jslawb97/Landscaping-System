using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Jacob Slaubaugh
    /// Created 2018/02/18
    /// 
    /// Interface for the EquipmentStatusAccessor
    /// </summary>
    public interface IEquipmentStatusAccessor
    {
        List<EquipmentStatus> RetrieveEquipmentStatusList();
        int DeleteEquipmentStatus(string equipmentStatusID);
        string CreateEquipmentStatus(EquipmentStatus equipmentStatus);
        int EditEquipmentStatus(EquipmentStatus oldEquipmentStatus, EquipmentStatus newEquipmentStatus);
        EquipmentStatus RetrieveEquipmentStatusByID(string equipmentStatusID);
    }
}
