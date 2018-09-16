using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    /// <summary>
    /// Sam Dramstad
    /// Created 2018/02/20
    /// 
    /// Class for the creation of Customer objects
    /// </summary>
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required]
        public string CustomerTypeID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public bool Active { get; set; }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Returns display data for the customers full name
        /// </summary>
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/03/10
        /// 
        /// Returns last name, first name, and email formatted to be displayed in a dropdown list.
        /// </summary>
        public string DropdownDisplay
        {
            get
            {
                return LastName + ", " + FirstName + " - " + Email;
            }
        }
    }
}
