using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;
using System.Numerics;
using DataObjects;

namespace Logic
{
    /// <summary>
    /// John Miller
    /// Created: 2018/02/15
    /// 
    /// Validation logic for integer values
    /// </summary>
    public static class IntegerValidations
    {
		
		/// <summary>
        /// Checks that integer is greater than or equal to zero
        /// </summary>
        /// <param name="i">the integer to check</param>
        /// <returns>true, if integer is valid, false otherwise</returns>
        public static bool IsValidQuantity(this Int32 i)
        {
            return (i >= 0);
        }
		
        public static bool IsValidNumber(string number)
        {
            try
            {
                BigInteger.Parse(number);
                return true;
            }
            catch(FormatException)
            {
                return false;
            }
        }

        public static bool IsNonNegativeNumber(string number)
        {
            bool isPositive = true;
            try
            {
                return BigInteger.Parse(number) > 0;
            }
            catch (FormatException)
            {
                isPositive = false;
            }
            return isPositive;
        }

        /// <summary>
        /// James McPherson
        /// Created 2018/03/07
        /// 
        /// Validation logic for IDs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsValidID(this int id)
        {
            return id >= Constants.IDSTARTVALUE;
        }
    }
}
