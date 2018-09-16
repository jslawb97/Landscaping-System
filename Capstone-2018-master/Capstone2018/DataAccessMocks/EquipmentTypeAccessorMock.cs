using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class EquipmentTypeAccessorMock : IEquipmentTypeAccessor
    {
        private List<EquipmentType> _equipmentTypes = new List<EquipmentType>();

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Mock constructor to add data to the EquipmentType list
        /// </summary>
        public EquipmentTypeAccessorMock()
        {
            _equipmentTypes.Add(new EquipmentType
            {
                EquipmentTypeID = "tractor",
                InspectionChecklistID = 1000000,
                PrepChecklistID = 1000000,
                Active = true
            });
            _equipmentTypes.Add(new EquipmentType
            {
                EquipmentTypeID = "lawn mower",
                InspectionChecklistID = 1000000,
                PrepChecklistID = 1000000,
                Active = true
            });
        }

        public int CreateEquipmentType(EquipmentType equipmentType)
        {
            if (equipmentType.EquipmentTypeID != "" &&
                equipmentType.EquipmentTypeID.Length <= 100 &&
                equipmentType.InspectionChecklistID >= 1000000 &&
                equipmentType.PrepChecklistID >= 1000000)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        public int EditEquipmentType(EquipmentType oldEquipmentType, EquipmentType newEquipmentType)
        {
            if (oldEquipmentType.EquipmentTypeID != "" &&
                oldEquipmentType.EquipmentTypeID.Length <= 100 &&
                oldEquipmentType.InspectionChecklistID >= 1000000 &&
                oldEquipmentType.PrepChecklistID >= 1000000 &&
                newEquipmentType.EquipmentTypeID != "" &&
                newEquipmentType.EquipmentTypeID.Length <= 100 &&
                newEquipmentType.InspectionChecklistID >= 1000000 &&
                newEquipmentType.PrepChecklistID >= 1000000)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        public List<EquipmentType> RetrieveEquipmentTypeList()
        {
            return _equipmentTypes;
        }

        public EquipmentType RetrieveEquipmentTypeByID(string equipmentTypeID)
        {
            return this._equipmentTypes.Find(equipmentType => equipmentType.EquipmentTypeID.Equals(equipmentTypeID));
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Mock method to deactivate an EquipmentType by ID
        /// </summary>
        /// <param name="equipmentTypeID"></param>
        /// <returns></returns>
        public int DeactivateEquipmentTypeByID(string equipmentTypeID)
        {
            int result = 0;

            foreach(EquipmentType et in _equipmentTypes)
            {
                if(et.EquipmentTypeID == equipmentTypeID)
                {
                    et.Active = false;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/09
        /// 
        /// Data access mock to retrieve equipment type detail list.
        /// </summary>
        /// <returns></returns>
        public List<EquipmentTypeDetail> RetrieveEquipmentTypeDetailList()
        {
            List<EquipmentTypeDetail> equipmentTypeDetailList = new List<EquipmentTypeDetail>();
            foreach(EquipmentType et in _equipmentTypes)
            {
                EquipmentTypeDetail equipmentTypeDetail = new EquipmentTypeDetail();
                equipmentTypeDetail.EquipmentType = et;
                equipmentTypeDetailList.Add(equipmentTypeDetail);
            }

            return equipmentTypeDetailList;

        }
    }
}
