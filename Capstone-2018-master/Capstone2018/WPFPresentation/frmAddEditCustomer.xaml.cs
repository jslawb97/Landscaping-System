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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using DataObjects;
using Logic;

namespace WPFPresentation
{
    /// <summary>
    /// Mike Mason
    /// Created: 2018/03/05
    /// 
    /// Variables to hold CustomerManager and Customer
    /// </summary>
    public partial class frmAddEditCustomer : Window
    {
        private ICustomerManager _customerManager;
        private Customer _customer;

        /// <summary>
        /// Mike Mason
        /// Created: 2018/03/05
        /// 
        /// Form loader for add
        /// </summary>
        public frmAddEditCustomer(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
            InitializeComponent();
            setUpAddForm();

        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/01
        /// 
        /// Constructor for editing a customer
        /// </summary>
        /// <param name="customer"></param>
        public frmAddEditCustomer(ICustomerManager customerManager, Customer customer)
        {
            _customer = customer;
            _customerManager = customerManager;
            InitializeComponent();
            setUpEditForm();
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/01
        /// 
        /// Fill in the text boxes, combo boxes, and check boxes with customer's information
        /// </summary>
        private void setUpEditForm()
        {
            lblHeader.Content = "Editing Customer " + _customer.FirstName + " " + _customer.LastName;
            btnAddEdit.Content = "Submit";
            chkActive.IsChecked = true;
            this.txtFirstName.Text = _customer.FirstName;
            this.txtLastName.Text = _customer.LastName;
            this.txtPhone.Text = _customer.PhoneNumber;
            this.txtEmail.Text = _customer.Email;
            setEditComboBoxes();
            cboCustomerType.SelectedItem = _customer.CustomerTypeID;
        }



        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Populates fields of the CustomerTypeID combobox
        /// </summary>
        private void setAddComboBoxes()
        {
            var customerList = _customerManager.RetrieveCustomerTypeList();
            this.cboCustomerType.ItemsSource = customerList.Select(n => n.CustomerTypeID).Distinct().ToList();
            this.cboCustomerType.SelectedIndex = 0;
        }

        private void setEditComboBoxes()
        {
            var customerType = _customerManager.RetrieveCustomerTypeList();
            cboCustomerType.ItemsSource = customerType.Select(n => n.CustomerTypeID).Distinct().ToList();
        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Populates fields to reflect the Add functionality
        /// </summary>
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding a New Customer";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;

            setAddComboBoxes();

        }

        /// <summary>
        /// Mike Mason
        /// Created 2018/03/05
        /// 
        /// Performs the add functionality
        /// </summary>
        private void performAdd()
        {
            if (validate())
            {
                var newCustomer = new Customer()
                {
                    CustomerTypeID = cboCustomerType.Text,
                    Email = txtEmail.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    PhoneNumber = txtPhone.Text,
                    // Active = (bool) chkActive.IsChecked

                };


                try
                {
                    var result = _customerManager.CreateCustomer(newCustomer);
                    MessageBox.Show(newCustomer.FirstName + " " + newCustomer.LastName + " was successfully added!");
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
        /// Mike Mason
        /// Created 2018/02/15
        /// 
        /// Button Event for Creating a Customer
        /// </summary>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_customer == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        /// <summary>
        /// Jayden Tollefson
        /// Created 2018/03/01
        /// 
        /// Validates feilds and submits edited data
        /// <remarks>Added CustomerTypeID to match form - Mike Mason</remarks>
        /// </summary>
        private void performEdit()
        {
            if (validate())
            {
                var newCustomer = new Customer()
                {
                    CustomerID = 0,
                    CustomerTypeID = cboCustomerType.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Active = true
                };
                try
                {
                    _customerManager.EditCustomer(_customer, newCustomer);
                    MessageBox.Show(newCustomer.FirstName + " " + newCustomer.LastName + " was successfully edited!");
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
        /// Jayden Tollefson
        /// Created 2018/03/02
        /// 
        /// Validates the user input
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtFirstName.Text))
            {
                MessageBox.Show("You must provide a first name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtFirstName.Text, 100))
            {
                MessageBox.Show("Your first name cannot be over 100 characters.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtLastName.Text))
            {
                MessageBox.Show("You must provide a last name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtLastName.Text, 100))
            {
                MessageBox.Show("Your last name cannot be over 100 characters.");
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

            return true;
        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/02/15
        /// 
        /// Button Event for cancelling the creation of a customer
        /// </summary>
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
