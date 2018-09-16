using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// James McPherson
    /// Created 2018/02/13
    /// 
    /// Interface for the EmployeeCertificationManager
    /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
    public interface IEmployeeCertificationManager
    {
        int DeactivateEmployeeCertificationByID(int employeeID, int certificationID);
        List<EmployeeCertificationDetail> RetrieveEmployeeCertificationList();
        EmployeeCertificationDetail RetrieveEmployeeCertificationDetail(EmployeeCertification employeeCertification);
        List<EmployeeCertificationDetail> RetrieveEmployeeCertificationDetailList();
        int CreateEmployeeCertification(EmployeeCertification employeeCertification);
        bool EditEmployeeCertification(EmployeeCertification oldEmployeeCertification, EmployeeCertification newEmployeeCertification);
    }
}
