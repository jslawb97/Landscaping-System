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
    public class EquipmentDetailManager : IEquipmentDetailManager
    {
        EquipmentAccessor _equipmentAccessor = new EquipmentAccessor();
        MakeModelAccessor _makeModelAccessor = new MakeModelAccessor();
        private IMakeModelAccessor _iMakeModelAccessor;
        private IEquipmentAccessor _iEquipmentAccessor;

        //Constructor
        public EquipmentDetailManager()
        {
            this._iEquipmentAccessor = new EquipmentAccessor();
            this._iMakeModelAccessor = new MakeModelAccessor();
        }


        //for unit tests
        public EquipmentDetailManager(IEquipmentAccessor iEquipmentAccessor, IMakeModelAccessor iMakeModelAccessor)
        {
            this._iEquipmentAccessor = iEquipmentAccessor;
            this._iMakeModelAccessor = iMakeModelAccessor;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/08
        /// 
        /// Retrieves all active equipment with made model for display purposes
        /// </summary>
        public List<EquipmentDetail> RetrieveEquipmentDetailByActive(bool active = true)
        {
            List<EquipmentDetail> equipmentViewList = new List<EquipmentDetail>();
            try
            {
                foreach (Equipment equipment in _iEquipmentAccessor.RetrieveEquipmentListByActive())
                {
                    //initialize required variables
                    EquipmentDetail equipmentView = new EquipmentDetail();
                    MakeModel makeModel = new MakeModel();

                    //Create an equipment view object
                    equipmentView.Equipment = equipment;
                    equipmentView.Make = _iMakeModelAccessor.RetrieveMakeModelByID(equipmentView.Equipment.MakeModelID).Make;
                    equipmentView.Model = _iMakeModelAccessor.RetrieveMakeModelByID(equipmentView.Equipment.MakeModelID).Model;

                    //Add to the list
                    equipmentViewList.Add(equipmentView);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return equipmentViewList;

        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/08
        /// 
        /// Retrieves all active equipment with made model for display purposes
        /// </summary>
        public List<EquipmentDetail> RetrieveEquipmentDetailList(bool active = true)
        {
            List<EquipmentDetail> equipmentViewList = new List<EquipmentDetail>();
            try
            {
                foreach (Equipment equipment in _iEquipmentAccessor.RetrieveEquipmentList())
                {
                    //initialize required variables
                    EquipmentDetail equipmentView = new EquipmentDetail();
                    MakeModel makeModel = new MakeModel();

                    //Create an equipment view object
                    equipmentView.Equipment = equipment;
                    equipmentView.Make = _iMakeModelAccessor.RetrieveMakeModelByID(equipmentView.Equipment.MakeModelID).Make;
                    equipmentView.Model = _iMakeModelAccessor.RetrieveMakeModelByID(equipmentView.Equipment.MakeModelID).Model;

                    //Add to the list
                    equipmentViewList.Add(equipmentView);
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return equipmentViewList;

        }
    }
}
