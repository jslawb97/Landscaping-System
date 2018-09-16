using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/01
    /// 
    /// Validation Logic for String values
    /// </summary>
    public static class StringValidations
    {

        /// <summary>
        /// John Miller
        /// Created 2018/02/15
        /// 
        /// Checks if Name property's length meets specifications for max size
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxSize"></param>
        /// <returns>True if the length of Name does not pass max size specification. False if Name is too large</returns>
        public static bool IsValidNamePropertyMaxSize(string text, int maxSize)
        {
            return text.Length <= maxSize;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Checks if the Name property is empty
        /// </summary>
        /// <param name="text">The name property to check</param>
        /// <returns>True, if the Name property is not empty. False if Name is empty</returns>
        /// <remarks>
        /// Zachary Hall
        /// Updated 2018/01/31
        /// </remarks>
        public static bool IsValidNamePropertyEmpty(string text)
        {
            return text.Length > 0;
        }
        /// <summary>
        /// Validates a string representing a description property
        /// </summary>
        /// <param name="s">The string to validate</param>
        /// <returns>A bool indicating whether or not the length of the string was between 1 and 1000</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/2/1
        /// </remarks>
        public static bool IsValidDescriptionProperty(this String s)
        {
            return (s.Length > 0 && s.Length <= 1000);
        }


        /// <summary>
        /// John Miller
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length < 16 && phoneNumber.Length > 0;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Checks if a string is a valid email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
