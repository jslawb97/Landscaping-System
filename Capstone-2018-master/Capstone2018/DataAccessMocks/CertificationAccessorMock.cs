using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessMocks
{
    public class CertificationAccessorMock : ICertificationAccessor
    {
        private List<Certification> _certificationList = new List<Certification>();

        /// /// <summary>
        /// Weston Olund
        /// Created on 2018/01/26
        /// 
        /// Mock constructor to add data to the certification list
        /// </summary>
        public CertificationAccessorMock()
        {
            _certificationList.Add(new Certification()
            {
                CertificationID = 1000000,
                CertificationName = "CertificationNameTestItem1",
                CertificationDescription = "CertificationDescriptionTestItem1",
            });
            _certificationList.Add(new Certification()
            {
                CertificationID = 1000001,
                CertificationName = "CertificationNameTestItem2",
                CertificationDescription = "CertificationDescriptionTestItem2"
            });
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method to deactivate mock certifications
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        public int DeactivateCertificationByID(int certificationID)
        {
            int result = 0;

            foreach(Certification c in _certificationList)
            {
                if(c.CertificationID == certificationID)
                {
                    c.Active = false;
                    result++;
                }
            }

            return result;
        }


        /// /// <summary>
        /// Weston Olund
        /// Created on 2018/01/26
        /// 
        /// method to return mock data
        /// </summary>
        /// <returns></returns>
        public List<DataObjects.Certification> RetrieveCertificationList()
        {
            return _certificationList;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/01
        /// 
        /// method to return mock data
        /// </summary>
        /// <param name="certification"></param>
        /// <returns></returns>
        public int CreateCertification(Certification certification)
        {
            if (certification.CertificationID == Constants.IDSTARTVALUE * 500)
            {
                throw new ApplicationException("Database access error");
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to return mock data
        /// </summary>
        /// <param name="oldCertification"></param>
        /// <param name="newCertification"></param>
        /// <returns></returns>
        public int EditCertification(Certification oldCertification, Certification newCertification)
        {
            var rowsAffected = 0;
            foreach (var c in _certificationList)
            {
                if (oldCertification.CertificationID == c.CertificationID
                    && newCertification.CertificationID == c.CertificationID)
                {
                    rowsAffected++;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("No rows affected by edit");
            }
            return rowsAffected;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Returns mock data for tests
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        public Certification RetrieveCertificationByID(int certificationID)
        {
            Certification cert = null;
            foreach (var c in _certificationList)
            {
                if (c.CertificationID == certificationID)
                {
                    cert = c;
                }
            }
            if (cert == null)
            {
                throw new ApplicationException("Certification Record not found.");
            }
            return cert;
        }
    }
}
