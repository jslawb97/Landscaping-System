using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/13
    /// 
    /// Interface for the EquipmentTypeManager
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/03/07
    /// </remarks>
    /// </summary>
    public interface IEquipmentTypeManager
    {
        int CreateEquipmentType(EquipmentType equipmentType);

        List<EquipmentType> RetrieveEquipmentTypeList();

        bool EditEquipmentType(EquipmentType oldEquipmentType, EquipmentType newEquipmentType);

        EquipmentTypeDetail RetrieveEquipmentTypeDetail(EquipmentType equipmentType);

        int DeactivateEquipmentTypeByID(string equipmentTypeID);

        List<EquipmentTypeDetail> RetrieveEquipmentTypeDetailList();
    }
}
