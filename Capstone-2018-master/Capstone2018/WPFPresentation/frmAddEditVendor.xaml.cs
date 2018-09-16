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
using Logic;
using DataObjects;

namespace WPFPresentation
{
    /// <summary>
    /// John Miller
    /// Created 2018/02/15
    /// 
    /// Interaction logic for frmAddEditVendor.xaml
    /// </summary>
    public partial class frmAddEditVendor : Window
    {
        private IVendorManager _vendorManager;
        private Vendor _vendor;

        /// <summary>
        /// John Miller
        /// Created 2018/02/15
        /// 
        /// Constructor for add function
        /// </summary>
        /// <param name="vendorManager"></param>
        public frmAddEditVendor(IVendorManager vendorManager)
        {
            _vendorManager = vendorManager;
            InitializeComponent();
            setupAddForm();
        }

        public frmAddEditVendor(IVendorManager vendorManager, Vendor vendor)
        {
            _vendorManager = vendorManager;
            _vendor = vendor;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// John Miller
        /// Created 2018/02/15
        /// 
        /// Populates fields to reflect the Edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Vendor " + _vendor.Name;
            btnAddEdit.Content = "Save";
            txtName.Text = _vendor.Name;
            txtRep.Text = _vendor.Rep;
            txtAddress.Text = _vendor.Address;
            txtPhone.Text = _vendor.Phone;
            txtWebsite.Text = _vendor.Website;
            chkActive.IsChecked = _vendor.Active;
        }

        /// <summary>
        /// John Miller
        /// Created 2018/02/15
        /// 
        /// Populates fields to reflect the Add functionality
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Vendor";
            btnAddEdit.Content = "Add";
            chkActive.IsChecked = true;
            chkActive.IsEnabled = false;
        }

        private void performEdit()
        {
            if (validateFields())
            {
                var newVendor = new Vendor()
                {
                    Name = txtName.Text,
                    Rep = txtRep.Text,
                    Address = txtAddress.Text,
                    Website = txtWebsite.Text,
                    Phone = txtPhone.Text,
                    Active = (bool)chkActive.IsChecked
                };
                try
                {
                    var result = _vendorManager.EditVendor(_vendor, newVendor);
                    MessageBox.Show(_vendor.Name + " was successfully edited!");
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
                    MessageBox.Show(message, "Edit Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
        }

        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("You must provide a name.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters in length.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtRep.Text))
            {
                MessageBox.Show("Rep field cannot be empty.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtRep.Text, 100))
            {
                MessageBox.Show("Rep cannot be over 100 characters in length.");
                return false;
            }


            if (!StringValidations.IsValidNamePropertyEmpty(txtAddress.Text))
            {
                MessageBox.Show("You must provide an address.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtAddress.Text, 250))
            {
                MessageBox.Show("Address cannot be over 250 characters in length.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtWebsite.Text))
            {
                MessageBox.Show("You must provide a website.");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtWebsite.Text, 250))
            {
                MessageBox.Show("Website cannot be over 250 characters in length.");
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
        /// John Miller
        /// Created 2018/02/15
        /// 
        /// Form Cancel Event called by user clicking cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vendor == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        private void performAdd()
        {
            if (validateFields())
            {
                var newItem = new Vendor()
                {
                    Name = txtName.Text,
                    Rep = txtRep.Text,
                    Address = txtAddress.Text,
                    Website = txtWebsite.Text,
                    Phone = txtPhone.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    var result = _vendorManager.CreateVendor(newItem);
                    MessageBox.Show(newItem.Name + " was successfully added!");
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
    }
}
