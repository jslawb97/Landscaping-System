using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccess
{
    /// <summary>
    /// Weston Olund
    /// Created 01/26/2018
    /// 
    /// Interface for the Certification Accessor
    /// </summary>
	public interface ICertificationAccessor
    {
        List<Certification> RetrieveCertificationList();
        int CreateCertification(Certification certification);
        Certification RetrieveCertificationByID(int certificationID);
        int EditCertification(Certification oldCertification, Certification newCertification);
        int DeactivateCertificationByID(int certificationID);
    }
}
