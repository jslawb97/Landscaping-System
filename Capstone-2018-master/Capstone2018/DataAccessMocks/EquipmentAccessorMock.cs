using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class EquipmentAccessorMock : IEquipmentAccessor
    {
        private List<Equipment> _equipmentList = new List<Equipment>();

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Mock constructor to add data to the equipment list
        /// </summary>
        public EquipmentAccessorMock()
        {
            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000000,
                EquipmentTypeID = "Vehicle",
                Name = "Fork Lift",
                MakeModelID = 1000001,
                DatePurchased = new DateTime(2008, 12, 25),
                DateLastRepaired = null,
                PriceAtPurchase = 14000.00M,
                CurrentValue = 8000.00M,
                WarrantyUntil = new DateTime(2013, 12, 30),
                EquipmentStatusID = "normal",
                EquipmentDetails = "Fork lift for the warhouse",
                Active = true
            });
            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000001,
                EquipmentTypeID = "Vehicle",
                Name = "Bulldozer",
                MakeModelID = 1000002,
                DatePurchased = new DateTime(2008, 12, 25),
                DateLastRepaired = null,
                PriceAtPurchase = 38000.00M,
                CurrentValue = 17000.00M,
                WarrantyUntil = new DateTime(2016, 12, 25),
                EquipmentStatusID = "normal",
                EquipmentDetails = "Fork lift for the warhouse",
                Active = true
            });

            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000002,
                EquipmentTypeID = "Vehicle",
                Name = "Bulldozer",
                MakeModelID = 1000002,
                DatePurchased = new DateTime(2009, 12, 25),
                DateLastRepaired = null,
                PriceAtPurchase = 38000.00M,
                CurrentValue = 18500.00M,
                WarrantyUntil = new DateTime(2017, 12, 25),
                EquipmentStatusID = "normal",
                EquipmentDetails = "Fork lift for the warhouse",
                Active = false
            });

        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/04/03
        /// 
        /// Mock accessor to create equipment
        /// </summary>
        public int CreateEquipment(Equipment equipment)
        {
            int oldRowCount = _equipmentList.Count();
            _equipmentList.Add(new Equipment()
            {
                EquipmentID = 1000002,
                EquipmentTypeID = equipment.EquipmentTypeID,
                Name = equipment.Name,
                MakeModelID = equipment.MakeModelID,
                DatePurchased = equipment.DatePurchased,
                DateLastRepaired = equipment.DateLastRepaired,
                PriceAtPurchase = equipment.PriceAtPurchase,
                CurrentValue = equipment.CurrentValue,
                WarrantyUntil = equipment.WarrantyUntil,
                EquipmentStatusID = equipment.EquipmentStatusID,
                EquipmentDetails = equipment.EquipmentDetails,
                Active = true
            });
            return _equipmentList.Count() - oldRowCount;
        }


        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Mock accessor to deactivate equipment
        /// </summary>
        public int DeactivateEquipmentByID(int equipmentID)
        {
            int result = 0;

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.EquipmentID == equipmentID)
                {
                    equipment.Active = false;
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/03
        /// 
        /// Mock accessor to reactivate equipment
        /// </summary>
        public int ReactivateEquipmentByID(int equipmentID)
        {
            int result = 0;

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.EquipmentID == equipmentID && equipment.Active == false)
                {
                    equipment.Active = true;
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Mock accessor to edit equipment
        /// </summary>
        public int EditEquipment(Equipment oldEquipment, Equipment newEquipment)
        {
            int rowsAffected = 0;
            foreach (Equipment equipment in _equipmentList)
            {
                if (oldEquipment.EquipmentID == equipment.EquipmentID
                    && oldEquipment.Name == equipment.Name
                    && oldEquipment.MakeModelID == equipment.MakeModelID
                    && oldEquipment.DatePurchased == equipment.DatePurchased
                    && oldEquipment.PriceAtPurchase == equipment.PriceAtPurchase
                    && oldEquipment.CurrentValue == equipment.CurrentValue
                    && oldEquipment.WarrantyUntil == equipment.WarrantyUntil
                    && oldEquipment.EquipmentStatusID == equipment.EquipmentStatusID
                    && oldEquipment.EquipmentDetails == equipment.EquipmentDetails
                    && newEquipment.EquipmentID == oldEquipment.EquipmentID
                    )
                {
                    rowsAffected++;
                }
            }
            return rowsAffected;
        }

        public List<Equipment> RetreiveAvailableEquipmentByJobID(int jobID)
        {
            throw new NotImplementedException();
        }

        public Equipment RetrieveEquipmentByID(int equipmentID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Noah Davison
        /// 2018-05-03
        /// 
        /// Mock accessor to retrieve complete equipment list
        /// </summary>
        /// <returns></returns>
        public List<Equipment> RetrieveEquipmentList()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            foreach (Equipment equipment in _equipmentList)
            {                
                equipmentList.Add(equipment);               
            }

            return equipmentList;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Mock accessor to retrieve equipment list
        /// </summary>
        public List<Equipment> RetrieveEquipmentListByActive(bool active = true)
        {
            List<Equipment> equipmentList = new List<Equipment>();

            foreach (Equipment equipment in _equipmentList)
            {
                if (equipment.Active == active)
                {
                    equipmentList.Add(equipment);
                }
            }

            return equipmentList;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/06
        /// 
        /// Method to Retrieve a Equipment List by type and availability
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveEquipmentListByTypeAndAvailability(EquipmentType equipmentType, DateTime? startDate, DateTime? endDate)
        {
            List<Equipment> equipmentList = new List<Equipment>();
            Equipment tractor = new Equipment{
                EquipmentID = 10000,
                EquipmentTypeID = "tractor",
                Name = "Big Red",
                MakeModelID = 12200,
                DatePurchased = new DateTime(2020, 1, 1),
                PriceAtPurchase = 1144.00M,
                CurrentValue = 606.00M,
                EquipmentStatusID = "Ready",
                EquipmentDetails = "Its Big and Red",
                Active = true
            };
            equipmentList.Add(tractor);

            return equipmentList;
        }

    }
}