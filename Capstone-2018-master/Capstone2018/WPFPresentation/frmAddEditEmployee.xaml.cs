using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using DataObjects;
using Logic;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditEmployee.xaml
    /// </summary>
    /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
    public partial class frmAddEditEmployee : Window
    {

        /// <summary>
        /// Mike Mason
        /// Created: 2018/03/05
        /// 
        /// Variables to hold EmployeeManager and Employee
        /// </summary>
        private IEmployeeManager _employeeManager;
        private Employee _employee;

        /// <summary>
        /// Mike Mason
        /// Created: 2018/03/05
        /// 
        /// Form loader for add
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public frmAddEditEmployee(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
            InitializeComponent();
            setUpAddForm();
        }

        /// <summary>
        /// Mike Mason
        /// Created: 2018/03/05
        /// 
        /// Form loader for edit and view
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        public frmAddEditEmployee(IEmployeeManager employeeManager, Employee employee)
        {
            _employeeManager = employeeManager;
            _employee = employee;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Populates fields to reflect the Edit functionality
        /// </summary>
        /// <remarks>
        /// Dan Cable
        /// 2018/03/29
        /// Updated to show selected employees information
        /// </remarks>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing " + _employee.FirstName + " " + _employee.LastName;
            btnAddEdit.Content = "Save";
            this.txtFirstName.Text = _employee.FirstName;
            this.txtLastName.Text = _employee.LastName;
            this.txtAddress.Text = _employee.Address;
            this.txtPhone.Text = _employee.PhoneNumber;
            this.txtEmail.Text = _employee.Email;
            chkActive.IsChecked = _employee.Active;
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Populates fields to reflect the Add functionality
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding a New Employee";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;



        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Performs the add functionality
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void performAdd()
        {
            if (validateFields())
            {
                var newEmployee = new Employee()
                {

                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Address = txtAddress.Text,
                    PhoneNumber = txtPhone.Text,
                    Email = txtEmail.Text,
                    Active = (bool)chkActive.IsChecked

                };


                try
                {
                    var result = _employeeManager.CreateEmployee(newEmployee);
                    MessageBox.Show(newEmployee.FirstName + " " + newEmployee.LastName + " was successfully added!");
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "\n\n" + ex.InnerException.Message;
                    }
                    // display the error
                    MessageBox.Show(message, "Add Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Dan Cable
        /// Created 2018/03/29
        /// 
        /// Populates fields with employee information
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void performEdit()
        {
            if (validateFields())
            {
                var newEmployee = new Employee()
                {
                    EmployeeID = 0,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Active = true
                };
                try
                {
                    _employeeManager.EditEmployee(_employee, newEmployee);
                    MessageBox.Show(newEmployee.FirstName + " " + newEmployee.LastName + " was successfully updated!");
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception e)
                {
                    var message = e.Message + "\n\n" + e.InnerException;
                    throw new ApplicationException(message);
                }
            }
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Performs the validations on user input
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtFirstName.Text))
            {
                MessageBox.Show("You must provide a First Name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtFirstName.Text, 100))
            {
                MessageBox.Show("Your first name cannot be over 100 characters.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtLastName.Text))
            {
                MessageBox.Show("You must providea Last Name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtLastName.Text, 100))
            {
                MessageBox.Show("Your last name cannot be over 100 characters.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtAddress.Text))
            {
                MessageBox.Show("You must provide an address.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtAddress.Text, 250))
            {
                MessageBox.Show("Your last name cannot be over 250 characters.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtPhone.Text))
            {
                MessageBox.Show("You must provide a phone number.");
                return false;
            }

            if (!StringValidations.IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Phone number must be less than 15 characters in length.");
                return false;
            }

            if (!IntegerValidations.IsValidNumber(txtPhone.Text))
            {
                MessageBox.Show("Phone number must be a number.");
                return false;
            }

            if (!IntegerValidations.IsNonNegativeNumber(txtPhone.Text))
            {
                MessageBox.Show("Phone number must be a positive number");
                return false;
            }
            if (!StringValidations.IsValidNamePropertyEmpty(txtEmail.Text))
            {
                MessageBox.Show("You must provide an email.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtEmail.Text, 100))
            {
                MessageBox.Show("Email cannot be over 100 characters in length.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/02/15
        /// 
        /// Button Event for Creating an Employee
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {

            if (_employee == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }

        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/02/15
        /// 
        /// Button Event for cancelling the creation of an employee
        /// </summary>
        /// <remarks>QA ShilinXiong 2018/04/11 approved</remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?\nCanceling will discard any unsaved changes.",
               "Cancel Warning",
               MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
