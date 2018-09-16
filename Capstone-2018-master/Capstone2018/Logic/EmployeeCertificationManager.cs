using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataObjects;

namespace Logic
{
    public class EmployeeCertificationManager : IEmployeeCertificationManager
    {
        IEmployeeCertificationAccessor _employeeCertificationAccessor;

        CertificationAccessor _certificationAccessor = new CertificationAccessor();
        EmployeeAccessor _employeeAccessor = new EmployeeAccessor();

        // Real run
        public EmployeeCertificationManager()
        {
            this._employeeCertificationAccessor = new EmployeeCertificationAccessor();
        }

        // Test run
        public EmployeeCertificationManager(IEmployeeCertificationAccessor employeeCertificationAccessor)
        {
            this._employeeCertificationAccessor = employeeCertificationAccessor;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate a certification by ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        /// QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public int DeactivateEmployeeCertificationByID(int employeeID, int certificationID)
        {
            int result = 0;

            try
            {
                result = _employeeCertificationAccessor.DeactivateEmployeeCertificationByID(employeeID, certificationID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Mike Mason
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of Employee Certifications
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public List<EmployeeCertificationDetail> RetrieveEmployeeCertificationList()
        {

            List<EmployeeCertificationDetail> employeeCertificationList = null;

            try
            {
                employeeCertificationList = _employeeCertificationAccessor.RetrieveEmployeeCertificationList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to process. Try again later.", ex);
            }
            return employeeCertificationList;
        }



        /// <summary>
        /// Mike Mason
        /// Created: 2018/02/01
        /// 
        /// Retrieves a list of Employee Certification Details
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public List<EmployeeCertificationDetail> RetrieveEmployeeCertificationDetailList()
        {
            var insertDetailList = new List<EmployeeCertificationDetail>();
            try
            {
                var empCertList = RetrieveEmployeeCertificationList();

                foreach (var item in empCertList)
                {
                    var empID = _employeeAccessor.RetrieveEmployeeByID(item.Employee.EmployeeID);

                    var certID = _certificationAccessor.RetrieveCertificationByID(item.Certification.CertificationID);
                    var detail = new EmployeeCertificationDetail { Employee = empID, Certification = certID, EndDate = item.EndDate, Active = item.Active };
                    insertDetailList.Add(detail);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return insertDetailList;

        }



        /// <summary>
        /// Mike Mason
        /// Created: 2018/02/01
        /// 
        /// Retrieves an Employee Certification Detail
        /// </summary>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public EmployeeCertificationDetail RetrieveEmployeeCertificationDetail(EmployeeCertification employeeCertification)
        {
            EmployeeCertificationDetail employeeCertificationDetail = null;

            try
            {
                var empID = _employeeAccessor.RetrieveEmployeeByID(employeeCertification.EmployeeID);

                var certID = _certificationAccessor.RetrieveCertificationByID(employeeCertification.CertificationID);


                employeeCertificationDetail = new EmployeeCertificationDetail()
                {
                    Employee = empID,
                    Certification = certID,
                    EndDate = employeeCertificationDetail.EndDate,
                    Active = employeeCertificationDetail.Active
                };
            }
            catch (Exception)
            {
                throw;
            }

            return employeeCertificationDetail;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Creates an EmployeeCertification record
        /// </summary>
        /// <param name="employeeCertification"></param>
        /// <returns></returns>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public int CreateEmployeeCertification(EmployeeCertification employeeCertification)
        {
            var result = 0;

            try
            {
                result = _employeeCertificationAccessor.CreateEmployeeCertification(employeeCertification);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Brady Feller
        /// Created 2018/03/22
        /// 
        /// Edits an EmployeeCertification record
        /// </summary>
        /// <param name="oldEmployeeCertification"></param>
        /// <param name="newEmployeeCertification"></param>
        /// <returns></returns>QA ShilinXiong 5/4/18 Add,Updated,Delete EmployeeCertification</remark>
        public bool EditEmployeeCertification(EmployeeCertification oldEmployeeCertification, EmployeeCertification newEmployeeCertification)
        {
            var result = false;

            try
            {
                result = (0 != _employeeCertificationAccessor.EditEmployeeCertification(oldEmployeeCertification, newEmployeeCertification));
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}
