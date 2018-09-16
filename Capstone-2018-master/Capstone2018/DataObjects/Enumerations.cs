using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/04
    /// 
    /// An enumeration to show an AddEdit form's current mode
    /// </summary>
    public enum DetailFormMode
    {
        Add,
        Edit,
        View
    }
    public enum AddEditMode
    {
        view,
        edit,
        add
    }

    public enum SupplyStatus
    {
        Ordered,
        Received
    }
}
