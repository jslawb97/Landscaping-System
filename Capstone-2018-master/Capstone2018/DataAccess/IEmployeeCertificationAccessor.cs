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
    /// Created 2018/02/13
    /// 
    /// Interface for the EmployeeCertificationAccessor
    /// <remarks>
    /// Brady Feller
    /// Revised 2018/04/04
    /// 
    /// Added Create, Edit, and RetrieveByID methods
    /// </remarks>
    /// </summary>
    /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
    public interface IEmployeeCertificationAccessor
    {
        int DeactivateEmployeeCertificationByID(int employeeID, int certificationID);
        List<EmployeeCertificationDetail> RetrieveEmployeeCertificationList();
        int CreateEmployeeCertification(EmployeeCertification employeeCertification);
        int EditEmployeeCertification(EmployeeCertification oldEmployeeCertification, EmployeeCertification newEmployeeCertification);
        EmployeeCertification RetrieveEmployeeCertificationByID(int employeeID, int certificationID);
    }
}
