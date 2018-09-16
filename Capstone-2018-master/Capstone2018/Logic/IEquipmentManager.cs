using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IEquipmentManager
    {
        Equipment RetrieveEquipmentByID(int equipmentID);
        List<Equipment> RetrieveEquipmentList();
        bool DeactivateEquipmentByID(int equipmentID);
        bool CreateEquipment(Equipment equipment);
        bool EditEquipment(Equipment oldEquipment, Equipment newEquipment);

        List<Equipment> RetrieveEquipmentListByTypeAndAvailability(EquipmentType equipmentType, DateTime? startDate, DateTime? endDate);
        List<Equipment> RetreiveAvailableEquipmentByJobID(int jobID);

        bool ReactivateEquipmentByID(int equipmentID);
    }
}
