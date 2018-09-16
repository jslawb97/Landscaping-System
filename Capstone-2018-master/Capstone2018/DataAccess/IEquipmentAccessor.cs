using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IEquipmentAccessor
    {
        Equipment RetrieveEquipmentByID(int equipmentID);

        List<Equipment> RetrieveEquipmentList();

        int DeactivateEquipmentByID(int equipmentID);

        List<Equipment> RetrieveEquipmentListByActive(bool active = true);

        int CreateEquipment(Equipment equipment);

        int EditEquipment(Equipment oldEquipment, Equipment newEquipment);

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/06
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        List<Equipment> RetrieveEquipmentListByTypeAndAvailability(EquipmentType equipmentType, DateTime? startDate, DateTime? endDate);
        List<Equipment> RetreiveAvailableEquipmentByJobID(int jobID);
        int ReactivateEquipmentByID(int equipmentID);
    }
}
