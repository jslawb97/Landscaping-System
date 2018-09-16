using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/13
    /// 
    /// Interface for the EquipmentTypeAccessor
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/03/7
    /// </remarks>
    /// </summary>
    public interface IEquipmentTypeAccessor
    {
        List<EquipmentType> RetrieveEquipmentTypeList();

        int CreateEquipmentType(EquipmentType equipmentType);

        int EditEquipmentType(EquipmentType oldEquipmentType, EquipmentType newEquipmentType);

        EquipmentType RetrieveEquipmentTypeByID(string equipmentTypeID);

        int DeactivateEquipmentTypeByID(string equipmentTypeID);

        List<EquipmentTypeDetail> RetrieveEquipmentTypeDetailList();
    }
}
