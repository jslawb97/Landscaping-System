using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccess;

namespace Logic
{
	public class CertificationManager : ICertificationManager
	{
        ICertificationAccessor _certificationAccessor;


        // For real run
        public CertificationManager()
        {
            _certificationAccessor = new CertificationAccessor();
        }

        // For unit tests
        public CertificationManager(ICertificationAccessor certificationAccessor)
        {
            _certificationAccessor = certificationAccessor;
        }
        /// <summary>
        ///	Weston Olund
        ///	Created on 2018/01/26
        ///	
        /// Method to retrieve a list of all certifications from data access layer 
        /// </summary>
        /// <returns></returns>
        public List<Certification> RetrieveCertificationList()
        {
            List<Certification> certificationList = null;

            try
            {
                certificationList = _certificationAccessor.RetrieveCertificationList();
            }
            catch (Exception)
            {
                throw;
            }
            return certificationList;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/13
        /// 
        /// Method to deactivate a certification by ID
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns>Rows affected</returns>
        public int DeactivateCertificationByID(int certificationID)
        {
            int result = 0;

            try
            {
                result = _certificationAccessor.DeactivateCertificationByID(certificationID);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public bool CreateCertification(Certification certification)
        {
            if (certification.CertificationName == null)
            {
                throw new ApplicationException("You must enter a name.");
            }
            if (certification.CertificationName.Length <= 0)
            {
                throw new ApplicationException("You must enter a name.");
            }
            if (certification.CertificationName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }
            if (certification.CertificationDescription == null)
            {
                throw new ApplicationException("You must enter a description.");
            }
            if (certification.CertificationDescription.Length <= 0)
            {
                throw new ApplicationException("You must enter a description.");
            }
            if (certification.CertificationDescription.Length > Constants.MAXDESCRIPTIONLENGTH)
            {
                throw new ApplicationException("The description must be less than 1000 characters.");
            }
            try
            {
                return (0 != _certificationAccessor.CreateCertification(certification));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to edit a certification
        /// </summary>
        /// <param name="oldCertification"></param>
        /// <param name="newCertification"></param>
        /// <returns></returns>
        public bool EditCertification(Certification oldCertification, Certification newCertification)
        {
            if (newCertification.CertificationName == null)
            {
                throw new ApplicationException("You must enter a certification name.");
            }
            if (newCertification.CertificationName.Length < 1)
            {
                throw new ApplicationException("You must enter a certification name.");
            }
            if (newCertification.CertificationName.Length > Constants.MAXNAMELENGTH)
            {
                throw new ApplicationException("The name must be shorter than 100 characters.");
            }
            if (newCertification.CertificationDescription == null)
            {
                throw new ApplicationException("You must enter a description for the certification.");
            }
            if (newCertification.CertificationDescription.Length < 1)
            {
                throw new ApplicationException("You must enter a description for the certification.");
            }
            if (newCertification.CertificationDescription.Length > Constants.MAXDESCRIPTIONLENGTH)
            {
                throw new ApplicationException("The description must be less than 1000 characters.");
            }
            var result = false;
            try
            {
                result = (0 != _certificationAccessor.EditCertification(oldCertification, newCertification));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/07
        /// 
        /// Method to retrieve a certification by id
        /// </summary>
        /// <param name="certificationID"></param>
        /// <returns></returns>
        public Certification RetrieveCertificationByID(int certificationID)
        {
            if (certificationID < Constants.IDSTARTVALUE)
            {
                throw new ApplicationException("Bad ID value");
            }
            Certification cert = new Certification();
            try
            {
                return cert = _certificationAccessor.RetrieveCertificationByID(certificationID);
            }
            catch (Exception)
            {
                throw;
            }
        }
		
	}
}
