using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace DataAccessMocks
{
    public class EquipmentStatusAccessorMock : IEquipmentStatusAccessor
    {
        private List<EquipmentStatus> _equipmentStatusList = new List<EquipmentStatus>();

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/15
        /// 
        /// A mock constructor to add sample data to the equipment status list
        /// </summary>
        public EquipmentStatusAccessorMock()
        {
            _equipmentStatusList.Add(new EquipmentStatus()
            {
                EquipmentStatusID = "Needs Preparation"
            });
            _equipmentStatusList.Add(new EquipmentStatus()
            {
                EquipmentStatusID = "Needs Repairs"
            });
            _equipmentStatusList.Add(new EquipmentStatus()
            {
                EquipmentStatusID = "Needs Maintenance"
            });
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/02
        /// 
        /// Method adds a mock status
        /// </summary>
        /// <param name="equipmentStatus"></param>
        /// <returns></returns>
        public string CreateEquipmentStatus(EquipmentStatus equipmentStatus)
        {
            _equipmentStatusList.Add(equipmentStatus);
            return "Needs Washed";
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Method deletes a mock status
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public int DeleteEquipmentStatus(string equipmentStatusID)
        {
            int result = 0;

            foreach (EquipmentStatus equipmentStatus in _equipmentStatusList)
            {
                if(equipmentStatus.EquipmentStatusID == equipmentStatusID)
                {
                    equipmentStatus.EquipmentStatusID = "";
                    result++;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/15
        /// 
        /// Method returns the mock data
        /// </summary>
        /// <returns></returns>
        public List<DataObjects.EquipmentStatus> RetrieveEquipmentStatusList()
        {
            return _equipmentStatusList;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Method to edit an existing EquipmentStatus
        /// </summary>
        /// <param name="oldEquipmentStatus"></param>
        /// <param name="newEquipmentStatus"></param>
        /// <returns></returns>
        public int EditEquipmentStatus(EquipmentStatus oldEquipmentStatus, EquipmentStatus newEquipmentStatus)
        {
            _equipmentStatusList.Add(oldEquipmentStatus);
            foreach (var es in _equipmentStatusList)
            {
                if (es.EquipmentStatusID == oldEquipmentStatus.EquipmentStatusID)
                {
                    es.EquipmentStatusID = newEquipmentStatus.EquipmentStatusID;
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/03/08
        /// 
        /// Method to retrieve an Equipment Status by ID
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public EquipmentStatus RetrieveEquipmentStatusByID(string equipmentStatusID)
        {
            EquipmentStatus equipmentStatus = null;

            foreach (EquipmentStatus es in _equipmentStatusList)
            {
                if(es.EquipmentStatusID == equipmentStatusID)
                {
                    equipmentStatus = es;
                    break;
                }
            }

            return equipmentStatus;
        }
    }
}