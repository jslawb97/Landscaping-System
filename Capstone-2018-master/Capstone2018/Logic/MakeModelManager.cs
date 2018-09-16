using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
    public class MakeModelManager : IMakeModelManager
    {
        IMakeModelAccessor _makeModelAccessor;

        // Constructor for real run
        public MakeModelManager()
        {
            _makeModelAccessor = new MakeModelAccessor();
        }

        // Constructor for unit tests
        public MakeModelManager(IMakeModelAccessor makeModelAccessor)
        {
            this._makeModelAccessor = makeModelAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Creates a MakeModel
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>The success/failure of the creation</returns>
        public bool CreateMakeModel(MakeModel makeModel)
        {
            var result = false;

            if(makeModel.Make == null || makeModel.Model == null
                || makeModel.Make == "" || makeModel.Model == "")
            {
                throw new ApplicationException("Required MakeModel fields not filled out");
            }
            if (makeModel.MaintenanceChecklistID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad MaintenanceChecklist ID Value");
            }
            try
            {
                result = (0 != _makeModelAccessor.CreateMakeModel(makeModel));
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Edits a MakeModel
        /// </summary>
        /// <param name="oldMakeModel"></param>
        /// <param name="newMakeModel"></param>
        /// <returns>Rows affected</returns>
        public int EditMakeModel(MakeModel oldMakeModel, MakeModel newMakeModel)
        {
            var result = 0;

            if (newMakeModel.Make == null || newMakeModel.Model == null
                || newMakeModel.Make == "" || newMakeModel.Model == "")
            {
                throw new ApplicationException("Required MakeModel fields not filled out");
            }
            if (oldMakeModel.MakeModelID < Constants.IDSTARTVALUE || newMakeModel.MakeModelID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad MakeModel ID Value");
            }
            if (oldMakeModel.MakeModelID != newMakeModel.MakeModelID)
            {
                throw new ArgumentOutOfRangeException("MakeModel ID Value Mismatch");
            }
            if (oldMakeModel.MaintenanceChecklistID < Constants.IDSTARTVALUE || newMakeModel.MaintenanceChecklistID < Constants.IDSTARTVALUE)
            {
                throw new ArgumentOutOfRangeException("Bad MaintenanceChecklist ID Value");
            }
            try
            {
                result = _makeModelAccessor.EditMakeModel(oldMakeModel, newMakeModel);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/26
        /// 
        /// Method to retrieve a list of MakeModelDetails
        /// </summary>
        /// <returns></returns>
        public List<MakeModelDetail> RetrieveMakeModelDetailList()
        {
            List<MakeModelDetail> makeModelList = null;

            try
            {
                makeModelList = _makeModelAccessor.RetrieveMakeModelDetailList();
            } catch
            {
                throw;
            }

            return makeModelList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Retrieves a list of all MakeModels
        /// </summary>
        /// <returns>List of MakeModel</returns>
        public List<MakeModel> RetrieveMakeModelList()
        {
            List<MakeModel> makeModelList = null;

            try
            {
                makeModelList = _makeModelAccessor.RetrieveMakeModelList();
            }
            catch
            {
                throw;
            }

            return makeModelList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/16
        /// 
        /// Retrieves a list of MakeModels by active
        /// </summary>
        /// <returns>List of MakeModel</returns>
        public List<MakeModel> RetrieveMakeModelListByActive(bool active = true)
        {
            List<MakeModel> makeModelList = null;

            try
            {
                makeModelList = _makeModelAccessor.RetrieveMakeModelListByActive(active);
            }
            catch
            {
                throw;
            }

            return makeModelList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Retrieves a MakeModel by ID
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns>MakeModel</returns>
        public MakeModel RetrieveMakeModelByID(int makeModelID)
        {
            MakeModel makeModel = null;

            try
            {
                makeModel = _makeModelAccessor.RetrieveMakeModelByID(makeModelID);
            }
            catch
            {
                throw;
            }

            return makeModel;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate a MakeModel by ID
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns></returns>
        public bool DeactivateMakeModelByID(int makeModelID)
        {
            bool result = false;

            try
            {
                result = (1 == _makeModelAccessor.DeactivateMakeModelByID(makeModelID));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Noah Davison
        /// Created: 2018/02/14
        /// 
        /// Retrieves a list of MakeModels by make
        /// </summary>
        public List<MakeModel> RetrieveMakeModelListByMake(string make)
        {
            List<MakeModel> makeModelList = null;
            try
            {
                makeModelList = _makeModelAccessor.RetrieveMakeModelListByMake(make);
            }
            catch (Exception ex)
            {
                throw;
            }

            return makeModelList;
        }
    }
}
