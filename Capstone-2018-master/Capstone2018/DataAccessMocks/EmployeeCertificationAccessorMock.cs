using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace DataAccessMocks
{


    public class EmployeeCertificationAccessorMock : IEmployeeCertificationAccessor
    {
        private List<EmployeeCertification> _employeeCerts = new List<EmployeeCertification>();
        private List<EmployeeCertificationDetail> _employeeCertsDetail = new List<EmployeeCertificationDetail>();
        

        /// <summary>
        /// James McPherson
        /// 2018/02/15
        /// 
        /// Mock constructor to add data to the EmployeeCertification list
        /// </summary>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public EmployeeCertificationAccessorMock()
        {
            _employeeCerts.Add(new EmployeeCertification
            {
                CertificationID = 1000000,
                EmployeeID = 1000000,
                EndDate = DateTime.Now,
                Active = true
            });
            _employeeCerts.Add(new EmployeeCertification
            {
                CertificationID = 1000001,
                EmployeeID = 1000000,
                EndDate = DateTime.Now,
                Active = true
            });

            foreach (var cert in _employeeCerts)
            {
                var ec = new EmployeeCertificationDetail()
               {
                   Certification = new Certification() { CertificationID = cert.CertificationID, CertificationName = "Ouch" },
                   Employee = new Employee() { Email = "test@test.com", EmployeeID = cert.EmployeeID },
                   EndDate = cert.EndDate,
                   Active = cert.Active
               };
                _employeeCertsDetail.Add(ec);
            }
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Mock method to deactivate an EmployeeCertification by ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public int DeactivateEmployeeCertificationByID(int employeeID, int certificationID)
        {
            int result = 0;

            foreach(EmployeeCertification ec in _employeeCerts)
            {
                if(ec.EmployeeID == employeeID && ec.CertificationID == certificationID)
                {
                    ec.Active = false;
                    result++;
                }
            }

            return result;
        }

        /// /// <summary>
        /// Mike Mason
        /// Created on 2018/01/26
        /// 
        /// method to return mock data
        /// </summary>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public List<DataObjects.EmployeeCertificationDetail> RetrieveEmployeeCertificationList()
        {
            return _employeeCertsDetail;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Mock method to create an EmployeeCertification
        /// </summary>
        /// <param name="employeeCertification"></param>
        /// <returns></returns>
        /// <remarks>
        /// Noah Davison
        /// Modified 2018/4/09
        /// 
        /// Fixed method so tests pass.
        /// Previously was checking if a value that was set to DateTime.Now 
        /// equaled DateTime.Now, but the check returned false since the
        /// check was executed slightly after the set.
        /// </remarks>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>

        public int CreateEmployeeCertification(EmployeeCertification employeeCertification)
        {
            if (employeeCertification.EmployeeID >= 1000000 &&
                employeeCertification.CertificationID >= 1000000)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Mock method to Edit an EmployeeCertification
        /// </summary>
        /// <param name="oldEmployeeCertification"></param>
        /// <param name="newEmployeeCertification"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>

        public int EditEmployeeCertification(EmployeeCertification oldEmployeeCertification, EmployeeCertification newEmployeeCertification)
        {
            if (oldEmployeeCertification.EmployeeID >= 1000000 &&
                oldEmployeeCertification.CertificationID >= 1000000 &&
                newEmployeeCertification.EmployeeID >= 1000000 &&
                newEmployeeCertification.CertificationID >= 1000000)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException("Invalid Field Values");
            }
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Mock method to Retrieve an EmployeeCertification by their ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public EmployeeCertification RetrieveEmployeeCertificationByID(int employeeID, int certificationID)
        {
            return this._employeeCerts.Find(employeeCertification => employeeCertification.EmployeeID.Equals(employeeID) && employeeCertification.CertificationID.Equals(certificationID));
        }
    }
}
