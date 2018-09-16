using DataAccess;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{



    /// <summary>
    /// Brady Feller
    /// Created 2018/03/04
    /// </summary>
    public class EquipmentManager : IEquipmentManager
    {
        private IEquipmentAccessor _equipmentAccessor;

        public EquipmentManager()
        {
            _equipmentAccessor = new EquipmentAccessor();
        }

        public Equipment RetrieveEquipmentByID(int equipmentID)
        {
            throw new NotImplementedException();
        }

        // Constructor for unit tests
        public EquipmentManager(IEquipmentAccessor iEquipmentAccessor)
        {
            this._equipmentAccessor = iEquipmentAccessor;
        }

        public List<Equipment> RetrieveEquipmentList()
        {
            List<Equipment> equipment = null;

            try
            {
                equipment = _equipmentAccessor.RetrieveEquipmentList();
            }
            catch (Exception)
            {

                throw;
            }

            return equipment;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/08
        /// 
        /// Deactivates an equipment object by ID
        /// </summary>
        public bool DeactivateEquipmentByID(int equipmentID)
        {
            bool result = false;
            try
            {
                if (1 == _equipmentAccessor.DeactivateEquipmentByID(equipmentID))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/08
        /// 
        /// Adds a new equipment item.
        /// </summary>
        public bool CreateEquipment(Equipment equipment)
        {
            bool successfullyCreated = false;

            try
            {
                if(validateEquipment(equipment))
                {
                    if (1 == _equipmentAccessor.CreateEquipment(equipment))
                    {
                        successfullyCreated = true;
                    }
                }
                
            }
            catch
            {
                throw;
            }

            return successfullyCreated;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/04/02
        /// 
        /// Edits an existing equipment item.
        /// </summary>
        public bool EditEquipment(Equipment oldEquipment, Equipment newEquipment)
        {
            bool successfullyEdited = false;

            try
            {
                if(validateEquipment(newEquipment))
                {
                    if (1 == _equipmentAccessor.EditEquipment(oldEquipment, newEquipment))
                    {
                        successfullyEdited = true;
                    }
                }
                
                
            }
            catch
            {
                throw;
            }

            return successfullyEdited;
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created 2018/04/06
        /// 
        /// Method to request to retrieve a Equipment List by type and availability
        /// </summary>
        /// <param name="equipmentType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Equipment> RetrieveEquipmentListByTypeAndAvailability(EquipmentType equipmentType, DateTime? startDate, DateTime? endDate)
        {
            List<Equipment> equipment = null;

            try
            {
                equipment = _equipmentAccessor.RetrieveEquipmentListByTypeAndAvailability(equipmentType, startDate, endDate);
            }
            catch (Exception)
            {

                throw;
            }

            return equipment;
        }

        public List<Equipment> RetreiveAvailableEquipmentByJobID(int jobID)
        {
            List<Equipment> equipment = null;

            try
            {
                equipment = _equipmentAccessor.RetreiveAvailableEquipmentByJobID(jobID);
            }
            catch (Exception)
            {

                throw;
            }

            return equipment;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/05/03
        /// 
        /// Reactivates an equipment object by ID
        /// </summary>
        public bool ReactivateEquipmentByID(int equipmentID)
        {
            bool result = false;
            try
            {
                if (1 == _equipmentAccessor.ReactivateEquipmentByID(equipmentID))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/05/04
        /// 
        /// Validates inputted equipment for create and edit
        /// </summary>
        public bool validateEquipment(Equipment equipment)
        {
            if (equipment.EquipmentTypeID == null)
            {
                throw new ApplicationException("You must enter a equipment type.");
            }
            if (equipment.EquipmentTypeID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The equipment type must be shorter than 100 characters");
            }
            if (equipment.Name == null)
            {
                throw new ApplicationException("You must enter a name.");
            }
            if (equipment.Name.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }
            if (equipment.EquipmentStatusID == null)
            {
                throw new ApplicationException("You must enter an equipment status ID.");
            }
            if (equipment.EquipmentStatusID.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("You must enter an equipment status ID.");
            }
            if (equipment.EquipmentDetails == null)
            {
                throw new ApplicationException("You must enter details about the equipment.");
            }
            if (equipment.EquipmentDetails.Length > Constants.MAXDESCRIPTIONLENGTH)
            {
                throw new ApplicationException("You must enter details about the equipment.");
            }
            return true;
        }
    }
}
