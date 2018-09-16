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
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/16
    /// 
    /// Test class for the String Validations logic class
    /// </summary>
    [TestClass]
    public class StringValidationsTests
    {
        
        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/16
        /// 
        /// Tests tje IsValidNamePropertyMaxSize method for invalid value
        /// </summary>
        [TestMethod]
        public void TestIsValidNamePropertyMaxSizeInvalid()
        {
            // arrange
            string name = "This string is made to exceed the max size";
            int maxSize = 5;
            bool isValid;
            // act
            isValid = StringValidations.IsValidNamePropertyMaxSize(name, maxSize);
            // assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/16
        /// 
        /// Tests the IsValidNamePropertyMaxSize method for valid value
        /// </summary>
        [TestMethod]
        public void TestIsValidNamePropertyMaxSizeValid()
        {
            // arrange
            string name = "Good Name String";
            int maxSize = 50;
            bool isValid;
            // act
            isValid = StringValidations.IsValidNamePropertyMaxSize(name, maxSize);
            // assert
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/16
        /// 
        /// Tests the IsValidNamePropertyEmpty method for invalid values
        /// </summary>
        [TestMethod]
        public void TestIsValidNamePropertyEmptyInvalid()
        {
            // arrange
            string name = "";
            bool isValid;
            // act
            isValid = StringValidations.IsValidNamePropertyEmpty(name);
            // assert
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/16
        /// 
        /// Tests the IsValidNamePropertyEmpty method for a valid value
        /// </summary>
        [TestMethod]
        public void TestIsValidNamePropertyEmptyValid()
        {
            // arrange
            string name = "Valid Name";
            bool isValid;
            // act
            isValid = StringValidations.IsValidNamePropertyEmpty(name);
            // assert
            Assert.IsTrue(isValid);
        }


    }
}
