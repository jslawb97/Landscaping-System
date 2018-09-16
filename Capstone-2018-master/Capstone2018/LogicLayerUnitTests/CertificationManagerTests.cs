using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using DataObjects;
using Logic;
using DataAccessMocks;


namespace LogicLayerUnitTests
{
    [TestClass]
    public class CertificationManagerTests
    {
        private CertificationManager _certificationManager;

        [TestInitialize]
        public void TestSetup()
        {
            _certificationManager = new CertificationManager(new CertificationAccessorMock());
            
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/01/26
        /// 
        /// Method that verifies retrieve certification returns correct number of items
        /// </summary>
        [TestMethod]
        public void TestRetrieveCertificationList()
        {
            // arrange
            List<Certification> certList; 


            // act
            certList = _certificationManager.RetrieveCertificationList();


            // assert
            Assert.AreEqual(2, certList.Count);

        }

        /// <summary>
        /// James McPherson
        /// Created 2018/02/15
        /// 
        /// Method that verifies DeactivateCertificationByID deactivates a certification
        /// </summary>
        [TestMethod]
        public void TestDeactivateCertificationByID()
        {
            // Arrange
            int rowsAffected = 0;

            // Act
            rowsAffected = _certificationManager.DeactivateCertificationByID(1000001);

            // Assert
            Assert.AreEqual(1, rowsAffected);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/01
        /// 
        /// Method to test create certification
        /// </summary>
        [TestMethod]
        public void TestCreateCertificationCreated()
        {
            //arrange
            bool returnedNewCertificationID;
            Certification cert = new Certification();
            cert.CertificationName = "Valid Name";
            cert.CertificationDescription = "Valid Description";

            //act 
            returnedNewCertificationID = _certificationManager.CreateCertification(cert);

            //assert 
            Assert.AreEqual(true, returnedNewCertificationID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test null name value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Null Name value.")]
        public void TestCreateCertificationNullName()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            cert.CertificationName = null;
            cert.CertificationDescription = "Description";

            //act
            _certificationManager.CreateCertification(cert);

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test too short of name value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name value too short.")]
        public void TestCreateCertificationShortName()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            cert.CertificationName = "";
            cert.CertificationDescription = "Description";

            //act
            _certificationManager.CreateCertification(cert);

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test name value too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name value too long.")]
        public void TestCreateCertificationNameTooLong()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            string certName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                certName += "a";
            }
            cert.CertificationName = certName;
            cert.CertificationDescription = "Description";

            //act
            _certificationManager.CreateCertification(cert);
        }

        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test name value too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Description value null.")]
        public void TestCreateCertificationNullDescription()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            cert.CertificationName = "ValidName";
            cert.CertificationDescription = null;

            //act
            _certificationManager.CreateCertification(cert);
        }

        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test name value too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name value too short.")]
        public void TestCreateCertificationDescriptionTooShort()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            cert.CertificationName = "ValidName";
            cert.CertificationDescription = "";

            //act
            _certificationManager.CreateCertification(cert);
        }

        /// Weston Olund
        /// Created on 2018/03/02
        /// 
        /// Method to test name value too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Name value too long.")]
        public void TestCreateCertificationDescriptionTooLong()
        {
            //arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;
            cert.CertificationName = "ValidName";
            string certDescription = "";
            for (int i = 0; i < Constants.MAXDESCRIPTIONLENGTH + 1; i++)
            {
                certDescription += "a";
            }
            cert.CertificationDescription = certDescription;

            //act
            _certificationManager.CreateCertification(cert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/02
        /// Method to test when certification is not created
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Database error.")]
        public void TestCreateCertificationNotCreated()
        {
            //arrange
            bool newCertificationCreated;
            Certification cert = new Certification();
            cert.CertificationName = "Name";
            cert.CertificationDescription = "Description";
            cert.CertificationID = Constants.IDSTARTVALUE * 500;

            //act
            newCertificationCreated = _certificationManager.CreateCertification(cert);

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to ensure retrieve certification works
        /// </summary>
        [TestMethod]
        public void TestRetrieveCertificationByIDFound()
        {
            // arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE;

            // act
            cert = _certificationManager.RetrieveCertificationByID(cert.CertificationID);

            // assert
            Assert.AreEqual("CertificationNameTestItem1", cert.CertificationName);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to ensure exception thrown if bad id value passed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Bad ID value.")]
        public void TestRetrieveCertificationByIDBadID()
        {
            // arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE - 1;

            // act
            cert = _certificationManager.RetrieveCertificationByID(cert.CertificationID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to ensure exception thrown if certification not found
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Employee record not found.")]
        public void TestRetrieveCertificationByIDNotFound()
        {
            // arrange
            Certification cert = new Certification();
            cert.CertificationID = Constants.IDSTARTVALUE * 500;

            // act
            cert = _certificationManager.RetrieveCertificationByID(cert.CertificationID);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new name is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification name is null")]
        public void TestEditCertificationNullNameValue()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = null;
            newCert.CertificationDescription = ("ValidDescription");


            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new name length is too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification name is too short")]
        public void TestEditCertificationNameTooShort()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = "";
            newCert.CertificationDescription = ("ValidDescription");


            // act
            _certificationManager.EditCertification(oldCert, newCert);

        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new name length is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification name is too long")]
        public void TestEditCertificationNameTooLong()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            string newCertName = "";
            for (int i = 0; i < Constants.MAXNAMELENGTH + 1; i++)
            {
                newCertName += "a";
            }
            newCert.CertificationName = newCertName;
            newCert.CertificationDescription = ("ValidDescription");


            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new description is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification description is null")]
        public void TestEditCertificationDescriptionIsNull()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = "Valid Name";
            newCert.CertificationDescription = null;


            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new description length too short
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification description is too short")]
        public void TestEditCertificationDescriptionIsTooShort()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = "Valid Name";
            newCert.CertificationDescription = "";


            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to verify exception thrown if new description length is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "Certification name is too long")]
        public void TestEditCertificationDescriptionTooLong()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = ("ValidName");
            string newCertDescription = "";
            for (int i = 0; i < Constants.MAXDESCRIPTIONLENGTH + 1; i++)
            {
                newCertDescription += "a";
            }
            newCert.CertificationDescription = newCertDescription;

            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to ensure exception thrown if no rows affected by edit
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException),
        "No rows affected by edit")]
        public void TestEditCertificationNoRowsAffected()
        {
            // arrange
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE * 500;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE * 500;
            newCert.CertificationName = "Valid Name";
            newCert.CertificationDescription = "Valid Description";

            // act
            _certificationManager.EditCertification(oldCert, newCert);
        }

        /// <summary>
        /// Weston Olund
        /// Created on 2018/03/08
        /// 
        /// Method to ensure only 1 row affected by edit
        /// </summary>
        [TestMethod]
        public void TestEditCertificationGood()
        {
            // arrange
            bool editSuccess;
            Certification newCert = new Certification();
            Certification oldCert = new Certification();
            oldCert.CertificationID = Constants.IDSTARTVALUE;
            oldCert.CertificationName = "Valid Name";
            oldCert.CertificationDescription = "Valid Description";
            newCert.CertificationID = Constants.IDSTARTVALUE;
            newCert.CertificationName = "Valid Name";
            newCert.CertificationDescription = "Valid Description";

            // act
            editSuccess = _certificationManager.EditCertification(oldCert, newCert);

            // assert
            Assert.AreEqual(true, editSuccess);
        }


        [TestCleanup]
        public void TestTearDown()
        {
            _certificationManager = null;
        }
        

    }
}
