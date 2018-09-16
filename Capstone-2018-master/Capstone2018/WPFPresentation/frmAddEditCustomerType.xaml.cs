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
    /// Badis Saidani
    /// Created 2018/4/10
    /// Interaction logic for frmAddEditCustomerType.xaml
    /// </summary>
    public partial class frmAddEditCustomerType : Window
    {
        /// <summary>
        /// Mike Mason
        /// Created: 2018/03/05
        /// 
        /// Variables to hold EmployeeManager and Employee
        /// </summary>
        private ICustomerTypeManager _customerTypeManager;
        private CustomerType _customerType;



        /// <summary>
        /// Mike Mason
        /// Created: 2018/04/12
        /// 
        /// Form loader for add CustomerType
        /// </summary>
        public frmAddEditCustomerType(ICustomerTypeManager customerTypeManager)
        {
            _customerTypeManager = customerTypeManager;
            InitializeComponent();
            setUpAddForm();

        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/10
        /// 
        /// Constructor for editing a customerType
        /// </summary>
        /// <param name="customerType"></param>
        public frmAddEditCustomerType(ICustomerTypeManager customerTypeManager, CustomerType customerType)
        {
            _customerType = customerType;
            _customerTypeManager = customerTypeManager;
            InitializeComponent();
            setUpEditForm();
        }

        /// <summary>
        /// Badis Saidani
        /// Created 2018/04/10
        /// 
        /// Fill in the text boxe,  with customerType information
        /// </summary>
        private void setUpEditForm()
        {
            lblHeader.Content = "Editing Customer Type";
            btnAddEdit.Content = "Submit";
            this.txtCustomerType.Text = _customerType.CustomerTypeID;


        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/12
        /// 
        /// Populates fields to reflect the CustomerTpye Add functionality
        /// </summary>
        private void setUpAddForm()
        {
            lblHeader.Content = "Adding a New Customer Type";
            btnAddEdit.Content = "Add";


        }


        /// <summary>
        /// Mike Mason
        /// Created 2018/04/12
        /// 
        /// 
        /// Performs the add customerType functionality
        /// </summary>
        private void performAdd()
        {
            if (validate())
            {
                var newCustomerType = new CustomerType()
                {
                    CustomerTypeID = txtCustomerType.Text,


                };


                try
                {
                    var result = _customerTypeManager.CreateCustomerType(newCustomerType);
                    MessageBox.Show(newCustomerType.CustomerTypeID + " " + " was successfully added!");
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
        /// Badis Saiadni
        /// Updated 2018/04/27
        /// 
        /// Performs the edit customerType functionality
        /// </summary>
        private void performEdit()
        {
            var newCustomerType = new CustomerType()
            {
                CustomerTypeID = txtCustomerType.Text,

            };

            try
            {
                _customerTypeManager.EditCustomerType(_customerType, newCustomerType);
                this.Close();
            }
            catch (Exception e)
            {
                var message = e.Message + "\n\n" + e.InnerException;
                MessageBox.Show(message,
             "Editing Error",
             MessageBoxButton.OK);

            }
        }


        /// <summary>
        /// Badis Saiadni
        /// Updated 2018/04/27
        /// 
        /// add/edit customerType button functionality 
        /// </summary>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_customerType == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }

        }


        /// <summary>
        /// Badis Saiadni
        /// Created 2018/04/27
        /// 
        /// cancelling the add/edit
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

        /// <summary>
        /// Mike Mason
        /// Created 2018/04/12
        /// 
        /// Validates the user input
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtCustomerType.Text))
            {
                MessageBox.Show("You must provide a customer type.");
                return false;
            }
            if (!StringValidations.IsValidNamePropertyMaxSize(txtCustomerType.Text, 100))
            {
                MessageBox.Show("Your customer type cannot be over 100 characters.");
                return false;
            }

            return true;
        }
    }
}
