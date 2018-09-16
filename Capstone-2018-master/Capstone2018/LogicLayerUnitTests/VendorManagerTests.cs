using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessMocks;
using DataObjects;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerUnitTests
{
    [TestClass]
    public class VendorManagerTests
    {
        private IVendorManager _vendorManager;


        public VendorManagerTests()
        {
            this._vendorManager = new VendorManager(new VendorAccessorMock());
        }

        [TestInitialize]
        public void TestSetup()
        {
            _vendorManager = new VendorManager(new VendorAccessorMock());
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/02/23
        /// 
        /// Verifies the retrieval of all sample data
        /// </summary>
        [TestMethod]
        public void TestRetrieveVendorList()
        {
            Assert.AreEqual(3, this._vendorManager.RetrieveVendorList().Count());
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/02/23
        /// 
        /// Verifies the retrieval of sample data by active
        /// </summary>
        [TestMethod]
        public void TestRetrieveVendorListByActive()
        {
            Assert.AreEqual(3, this._vendorManager.RetrieveVendorListByActive().Count());
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created on 2018/04/12
        /// 
        /// Verifies the retrieval of a vendor
        /// </summary>
        [TestMethod]
        public void TestRetrieveVendorByID()
        {
            // arrange
            Vendor vendor = null;

            // act
            vendor = _vendorManager.RetrieveVendorByID(1000000);

            // assert
            Assert.AreEqual(1000000, vendor.VendorID);
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/02/23
        /// 
        /// Verifies that sample data can be edited
        /// 
        /// Jacob Slaubaugh
        /// Updated 2018/05/03
        /// </summary>
        [TestMethod]
        public void TestEditVendorGood()
        {
            Vendor oldVender = new Vendor
            {
                VendorID = Constants.IDSTARTVALUE,
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                VendorID = Constants.IDSTARTVALUE,
                Name = "Test",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with a null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestEditVendorNullName()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = null,
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with a null rep
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestEditVendorNullRep()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = null,
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with a null address
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestEditVendorNullAddress()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = null,
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with a null phone
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestEditVendorNullPhone()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = null,
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with an empty name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid data")]
        public void TestEditVendorEmptyName()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "1234657890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with an empty rep
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid data")]
        public void TestEditVendorEmptyRep()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = "",
                Address = "Test",
                Website = "Test",
                Phone = "1234657890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with an empty address
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid data")]
        public void TestEditVendorEmptyAddress()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "",
                Website = "Test",
                Phone = "1234657890",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be edited with an empty phone
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalid data")]
        public void TestEditVendorEmptyPhone()
        {
            Vendor oldVender = new Vendor
            {
                Name = "Test Name",
                Rep = "Test Rep",
                Address = "Test Address",
                Website = "Test Website",
                Phone = "Test Phone Number",
                Active = true
            };

            Vendor newVender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "",
                Active = true
            };

            bool _vender = _vendorManager.EditVendor(oldVender, newVender);
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/02/23
        /// 
        /// Verifies the creation of given sample data
        /// 
        /// Marshall Sejkora
        /// Updated 2018/04/27
        /// Chageded to match Validations
        /// </summary>
        [TestMethod]
        public void TestCreateVendorGood()
        {

            bool _vender = _vendorManager.CreateVendor(new Vendor
            {
                Name = "Updated Test Name",
                Rep = "Updated Test Rep",
                Address = "Updated Test Address",
                Website = "Updated Test Website",
                Phone = "1111111",
                Active = true
            });

            Assert.AreEqual(true, _vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be created with a null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestCreateVendorNameNull()
        {
            Vendor vender = new Vendor
            {
                Name = null,
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be created with a null rep
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestCreateVendorRepNull()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = null,
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be created with a null address
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestCreateVendorAddressNull()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = null,
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that a vendor won't be created with a null phone
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Null data")]
        public void TestCreateVendorPhoneNull()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = null,
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Updated 2018/05/03
        /// 
        /// Tests that a vendor won't be created with an empty name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestCreateVendorEmptyName()
        {
            Vendor vender = new Vendor
            {
                Name = "",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Updated 2018/05/03
        /// 
        /// Tests that a vendor won't be created with an empty rep
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestCreateVendorEmptyRep()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = "",
                Address = "Test",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Updated 2018/05/03
        /// 
        /// Tests that a vendor won't be created with an empty address
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestCreateVendorEmptyAddress()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "",
                Website = "Test",
                Phone = "1234567890",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Updated 2018/05/03
        /// 
        /// Tests that a vendor won't be created with an empty phone
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Invalide data")]
        public void TestCreateVendorEmptyPhone()
        {
            Vendor vender = new Vendor
            {
                Name = "Test",
                Rep = "Test",
                Address = "Test",
                Website = "Test",
                Phone = "",
                Active = true
            };

            bool _vender = _vendorManager.CreateVendor(vender);
        }

        /// <summary>
        /// John Miller
        /// Created on 2018/02/23
        /// 
        /// Verifies the deactivation of given sample data
        /// </summary>
        [TestMethod]
        public void TestDeactivateVendorByID()
        {
            Assert.AreEqual(true, this._vendorManager.DeactivateVendorByID(1000000));
        }

        /// <summary>
        /// Jacob Slaubaugh
        /// Created 2018/05/03
        /// 
        /// Tests that the deactivate will fail with a bad ID
        /// </summary>
        [TestMethod]
        public void TestDeactivateVendorByBadID()
        {
            try
            {
                bool result = _vendorManager.DeactivateVendorByID(5);

                // assert
                Assert.Fail("Delete should fail because of the bad ID");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        [TestCleanup]
        public void TestTearDown()
        {
            this._vendorManager = null;
        }
    }
}
