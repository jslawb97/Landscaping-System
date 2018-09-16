using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{
    public class MakeModelAccessorMock : IMakeModelAccessor
    {
        private List<MakeModel> _makeModelList = new List<MakeModel>();
        private List<MaintenanceChecklist> _maintenanceChecklists = new List<MaintenanceChecklist>();

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock constructor to add data to the MakeModel list
        /// </summary>
        public MakeModelAccessorMock()
        {
            _makeModelList.Add(new MakeModel()
            {
                MakeModelID = 1000000,
                Make = "TestMake1",
                Model = "TestModel1",
                MaintenanceChecklistID = 1000000,
                Active = true
            });
            _makeModelList.Add(new MakeModel()
            {
                MakeModelID = 1000001,
                Make = "TestMake2",
                Model = "TestModel2",
                MaintenanceChecklistID = 1000001,
                Active = true
            });
            _makeModelList.Add(new MakeModel()
            {
                MakeModelID = 1000002,
                Make = "TestMake3",
                Model = "TestModel3",
                MaintenanceChecklistID = 1000002,
                Active = true
            });
            _makeModelList.Add(new MakeModel()
            {
                MakeModelID = 1000003,
                Make = "TestMake4",
                Model = "TestModel4",
                MaintenanceChecklistID = 1000003,
                Active = false
            });

            _maintenanceChecklists.Add(new MaintenanceChecklist
            {
                MaintenanceChecklistID = 1000000,
                Name = "TestChecklist1"
            });
            _maintenanceChecklists.Add(new MaintenanceChecklist
            {
                MaintenanceChecklistID = 1000001,
                Name = "TestChecklist2"
            });
            _maintenanceChecklists.Add(new MaintenanceChecklist
            {
                MaintenanceChecklistID = 1000002,
                Name = "TestChecklist3"
            });
            _maintenanceChecklists.Add(new MaintenanceChecklist
            {
                MaintenanceChecklistID = 1000003,
                Name = "TestChecklist4"
            });
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock method to add a new MakeModel
        /// </summary>
        /// <param name="makeModel"></param>
        /// <returns></returns>
        public int CreateMakeModel(MakeModel makeModel)
        {
            _makeModelList.Add(makeModel);
            return 1000000;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Mock method to deactivate a MakeModel by ID
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns></returns>
        public int DeactivateMakeModelByID(int makeModelID)
        {
            var result = 0;

            foreach(MakeModel mm in _makeModelList)
            {
                if(mm.MakeModelID == makeModelID)
                {
                    mm.Active = false;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock method to edit an existing MakeModel
        /// </summary>
        /// <param name="oldMakeModel"></param>
        /// <param name="newMakeModel"></param>
        /// <returns></returns>
        public int EditMakeModel(MakeModel oldMakeModel, MakeModel newMakeModel)
        {
            var result = 0;

            foreach(MakeModel mm in _makeModelList)
            {
                if(oldMakeModel.MakeModelID == mm.MakeModelID &&
                    oldMakeModel.Make.Equals(mm.Make) &&
                    oldMakeModel.Model.Equals(mm.Model) &&
                    oldMakeModel.MaintenanceChecklistID == mm.MaintenanceChecklistID)
                {
                    mm.Make = newMakeModel.Make;
                    mm.Model = newMakeModel.Model;
                    mm.MaintenanceChecklistID = newMakeModel.MaintenanceChecklistID;
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock method to retrieve a MakeModel by ID
        /// </summary>
        /// <param name="makeModelID"></param>
        /// <returns></returns>
        public MakeModel RetrieveMakeModelByID(int makeModelID)
        {
            MakeModel makeModel = null;

            foreach(MakeModel mm in _makeModelList)
            {
                if(mm.MakeModelID == makeModelID)
                {
                    makeModel = mm;
                    break;
                }
            }

            return makeModel;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/04/26
        /// 
        /// Mock method to retrieve a list of MakeModelDetails
        /// </summary>
        /// <returns></returns>
        public List<MakeModelDetail> RetrieveMakeModelDetailList()
        {
            List<MakeModelDetail> detailList = new List<MakeModelDetail>();

            foreach(var makeModel in _makeModelList)
            {
                var detail = new MakeModelDetail
                {
                    MakeModelID = makeModel.MakeModelID,
                    Make = makeModel.Make,
                    Model = makeModel.Model,
                    MaintenanceChecklistID = makeModel.MaintenanceChecklistID,
                    Active = makeModel.Active
                };
                foreach(var maintenanceChecklist in _maintenanceChecklists)
                {
                    if(maintenanceChecklist.MaintenanceChecklistID == detail.MaintenanceChecklistID)
                    {
                        detail.MaintenanceChecklistName = maintenanceChecklist.Name;
                    }
                }
                detailList.Add(detail);
            }

            return detailList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/04
        /// 
        /// Mock method to retrieve a list of MakeModels
        /// </summary>
        /// <returns></returns>
        public List<MakeModel> RetrieveMakeModelList()
        {
            return _makeModelList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/16
        /// 
        /// Mock method to retrieve a list of MakeModels by active
        /// </summary>
        /// <returns></returns>
        public List<MakeModel> RetrieveMakeModelListByActive(bool active = true)
        {
            List<MakeModel> makeModelList = new List<MakeModel>();

            foreach(MakeModel mm in _makeModelList) {
                if(mm.Active == active) {
                    makeModelList.Add(mm);
                }
            }

            return makeModelList;
        }

        /// <summary>
        /// Noah Davison
        /// Created 2018/02/14
        /// 
        /// Mock Data Access method to retrieve a make model ID by inputted make
        /// </summary>
        public List<MakeModel> RetrieveMakeModelListByMake(string make)
        {
            List<MakeModel> matchingMakeModels = new List<MakeModel>();
            foreach (MakeModel makeModel in _makeModelList)
            {
                if (makeModel.Make == make)
                {
                    matchingMakeModels.Add(makeModel);
                }
            }
            return matchingMakeModels;
        }
    }
}
