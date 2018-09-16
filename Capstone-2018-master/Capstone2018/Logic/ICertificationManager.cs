using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// Weston Olund
    /// Created 2018/01/26
    /// 
    /// Interface for the CertificationManager
    /// </summary>
	public interface ICertificationManager
	{
		List<Certification> RetrieveCertificationList();
        int DeactivateCertificationByID(int certificationID);

        bool CreateCertification(Certification certification);
        Certification RetrieveCertificationByID(int certificationID);
        bool EditCertification(Certification oldCertification, Certification newCertification);
	}
}
