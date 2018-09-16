using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;
using System.Data.SqlClient;

namespace Logic
{
    public class EquipmentStatusManager : IEquipmentStatusManager
    {
        IEquipmentStatusAccessor _equipmentStatusAccessor;

        // Actual run
        public EquipmentStatusManager()
        {
            _equipmentStatusAccessor = new EquipmentStatusAccessor();
        }

        // For testing
        public EquipmentStatusManager(IEquipmentStatusAccessor equipmentStatusAccessor)
        {
            _equipmentStatusAccessor = equipmentStatusAccessor;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/18
        /// 
        /// Method to add a new equipment status
        /// </summary>
        /// <param name="equipmentStatus"></param>
        /// <returns></returns>
        public bool AddEquipmentStatus(EquipmentStatus equipmentStatus)
        {
            bool result = false;

            if (equipmentStatus.EquipmentStatusID == "")
            {
                throw new ApplicationException("You must fill out the field.");
            }
            try
            {
                result = (null != _equipmentStatusAccessor.CreateEquipmentStatus(equipmentStatus));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/16
        /// 
        /// Method to delete an equipment status
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public int DeleteEquipmentStatus(string equipmentStatusID)
        {
            int result = 0;

            try
            {
                result = _equipmentStatusAccessor.DeleteEquipmentStatus(equipmentStatusID);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/15
        /// 
        /// Method to retrieve the list of equipment status
        /// </summary>
        /// <returns></returns>
        public List<EquipmentStatus> RetrieveEquipmentStatusList()
        {
            List<EquipmentStatus> equipmentStatusList = null;

            try
            {
                equipmentStatusList = _equipmentStatusAccessor.RetrieveEquipmentStatusList();
            }
            catch (Exception)
            {
                throw;
            }
            return equipmentStatusList;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/23
        /// 
        /// Method to retrieve equipment status by ID
        /// </summary>
        /// <param name="equipmentStatusID"></param>
        /// <returns></returns>
        public EquipmentStatus RetrieveEquipmentStatusByID(string equipmentStatusID)
        {
            EquipmentStatus equipmentStatus = null;

            try
            {
                equipmentStatus = _equipmentStatusAccessor.RetrieveEquipmentStatusByID(equipmentStatusID);
            }
            catch (Exception)
            {
                throw;
            }

            return equipmentStatus;
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/02/23
        /// 
        /// Method to edit an existing status
        /// </summary>
        /// <param name="oldEquipmentStatus"></param>
        /// <param name="newEquipmentStatus"></param>
        /// <returns></returns>
        public int EditEquipmentStatus(EquipmentStatus oldEquipmentStatus, EquipmentStatus newEquipmentStatus)
        {
            int result = 0;

            if (newEquipmentStatus.EquipmentStatusID == "")
            {
                throw new ApplicationException("You must fill out everything.");
            }
            try
            {
                result = (_equipmentStatusAccessor.EditEquipmentStatus(newEquipmentStatus, oldEquipmentStatus));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
