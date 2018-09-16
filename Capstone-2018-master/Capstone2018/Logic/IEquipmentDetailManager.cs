using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IEquipmentDetailManager
    {
        List<EquipmentDetail> RetrieveEquipmentDetailByActive(bool active = true);
        
        List<EquipmentDetail> RetrieveEquipmentDetailList(bool active = true);
    }
}

