using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/04
    /// 
    /// Interface for the MakeModelAccessor
    /// </summary>
    public interface IMakeModelAccessor
    {
        int CreateMakeModel(MakeModel makeModel);
        List<MakeModel> RetrieveMakeModelList();
        List<MakeModel> RetrieveMakeModelListByActive(bool active = true);
        int EditMakeModel(MakeModel oldMakeModel, MakeModel newMakeModel);
        MakeModel RetrieveMakeModelByID(int makeModelID);
        int DeactivateMakeModelByID(int makeModelID);
        List<MakeModelDetail> RetrieveMakeModelDetailList();
        List<MakeModel> RetrieveMakeModelListByMake(string make);
    }
}
