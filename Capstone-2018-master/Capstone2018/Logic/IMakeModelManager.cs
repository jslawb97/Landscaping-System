using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// James Mcpherson
    /// Created 2018/02/04
    /// 
    /// Interface for the MakeModelManager
    /// </summary>
    public interface IMakeModelManager
    {
        bool CreateMakeModel(MakeModel makeModel);
        List<MakeModel> RetrieveMakeModelList();
        List<MakeModel> RetrieveMakeModelListByActive(bool active = true);
        int EditMakeModel(MakeModel oldMakeModel, MakeModel newMakeModel);
        MakeModel RetrieveMakeModelByID(int makeModelID);
        bool DeactivateMakeModelByID(int makeModelID);
        List<MakeModelDetail> RetrieveMakeModelDetailList();
        List<MakeModel> RetrieveMakeModelListByMake(string make);
    }
}
