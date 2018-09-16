using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class EquipmentTypeManager : IEquipmentTypeManager
    {
        private IEquipmentTypeAccessor _equipmentTypeAccessor;
        private IPrepChecklistAccessor _prepChecklistAccessor;
        private IInspectionChecklistAccessor _inspectionChecklistAccessor;

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/01
        /// 
        /// Default constructor
        /// </summary>
        public EquipmentTypeManager()
        {
            _equipmentTypeAccessor = new EquipmentTypeAccessor();
            _inspectionChecklistAccessor = new InspectionChecklistAccessor();
            _prepChecklistAccessor = new PrepChecklistAccessor();
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/1
        /// </summary>
        /// <param name="equipmentTypeAccessor"></param>
        public EquipmentTypeManager(IEquipmentTypeAccessor equipmentTypeAccessor, IPrepChecklistAccessor prepChecklistAccessor, IInspectionChecklistAccessor inspectionChecklistAccessor)
        {
            _equipmentTypeAccessor = equipmentTypeAccessor;
            _prepChecklistAccessor = prepChecklistAccessor;
            _inspectionChecklistAccessor = inspectionChecklistAccessor;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/1
        /// 
        /// Allows creation of equipment types
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <returns></returns>
        public int CreateEquipmentType(EquipmentType equipmentType)
        {
            var result = 0;

            if (equipmentType.EquipmentTypeID == "")
            {
                throw new ApplicationException("You must fill out the Equipment Type ID field.");
            }
            try
            {
                result = _equipmentTypeAccessor.CreateEquipmentType(equipmentType);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/01
        /// 
        /// Allows editing of an equipment type.
        /// </summary>
        /// <param name="oldEquipmentType"></param>
        /// <param name="newEquipmentType"></param>
        /// <returns></returns>
        public bool EditEquipmentType(EquipmentType oldEquipmentType, EquipmentType newEquipmentType)
        {
            var result = false;

            if (newEquipmentType.EquipmentTypeID == "")
            {
                throw new ApplicationException("You must fill the 'type' field.");
            }
            try
            {
                result = (0 != _equipmentTypeAccessor.EditEquipmentType(oldEquipmentType, newEquipmentType));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of equipment type items.
        /// </summary>
        /// <returns></returns>
        public List<EquipmentType> RetrieveEquipmentTypeList()
        {
            List<EquipmentType> equipmentTypeList = null;

            try
            {
                equipmentTypeList = _equipmentTypeAccessor.RetrieveEquipmentTypeList();
            }
            catch (Exception)
            {
                throw;
            }

            return equipmentTypeList;
        }

        /// <summary>
        /// Brady Feller
        /// Created: 2018/02/1
        /// 
        /// Retrieves the equipment type as a detail; giving it 
        /// added functionality.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EquipmentTypeDetail RetrieveEquipmentTypeDetail(EquipmentType equipmentType)
        {
            EquipmentTypeDetail equipmentTypeDetail = null;
            EquipmentType equipment = null;
            PrepChecklist prList = null;
            InspectionChecklist inList = null;

            try
            {
                equipment = _equipmentTypeAccessor.RetrieveEquipmentTypeByID(equipmentType.EquipmentTypeID);
                prList = _prepChecklistAccessor.RetrievePrepChecklistByID((int)equipmentType.PrepChecklistID);
                inList = _inspectionChecklistAccessor.RetrieveInspectionChecklistByID((int)equipmentType.InspectionChecklistID);


                equipmentTypeDetail = new EquipmentTypeDetail()
                {
                    EquipmentType = equipmentType,
                    InspectionChecklist = inList,
                    PrepChecklist = prList
                };
            }
            catch (Exception)
            {
                throw;
            }

            return equipmentTypeDetail;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Deactivates an EquipmentType by ID
        /// </summary>
        /// <param name="equipmentTypeID"></param>
        /// <returns></returns>
        public int DeactivateEquipmentTypeByID(string equipmentTypeID)
        {
            int result = 0;

            try
            {
                result = _equipmentTypeAccessor.DeactivateEquipmentTypeByID(equipmentTypeID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public List<EquipmentTypeDetail> RetrieveEquipmentTypeDetailList()
        {
            List<EquipmentTypeDetail> equipmentTypeDetailList = null;

            try
            {
                equipmentTypeDetailList = _equipmentTypeAccessor.RetrieveEquipmentTypeDetailList();
            }
            catch (Exception)
            {
                throw;
            }

            return equipmentTypeDetailList;
        }
    }
}
