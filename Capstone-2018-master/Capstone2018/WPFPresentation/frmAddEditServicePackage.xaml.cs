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
    /// Interaction logic for frmAddEditServicePackage.xaml
    /// </summary>
    public partial class frmAddEditServicePackage : Window
    {

        private IServicePackageManager _servicePackageManager;
        private ServicePackage _servicePackage;

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Form constructor to be called to add a service package
        /// </summary>
        /// <param name="servicePackageManager"></param>
        public frmAddEditServicePackage(IServicePackageManager servicePackageManager)
        {
            _servicePackageManager = servicePackageManager;
            InitializeComponent();
            setupAddForm();
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Form constructor to be called to edit a service package
        /// </summary>
        /// <param name="servicePackageManager"></param>
        /// <param name="servicePackage"></param>
        public frmAddEditServicePackage(IServicePackageManager servicePackageManager, ServicePackage servicePackage)
        {
            _servicePackageManager = servicePackageManager;
            _servicePackage = servicePackage;
            InitializeComponent();
            setupEditForm();
        }


        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Populates the controls for editing
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Service Package " + _servicePackage.ServicePackageID;
            btnAddEdit.Content = "Save";
            txtName.Text = _servicePackage.Name;
            txtDescription.Text = _servicePackage.Description;
            chkActive.IsChecked = _servicePackage.Active;
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Populates the controls for adding
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Service Package";
            btnAddEdit.Content = "Add";
        }

        /// <summary>
        /// Click event for submiting the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_servicePackage == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Container for edit form logic
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                var newPackage = new ServicePackage()
                {
                    Name = txtName.Text,
                    Description = txtName.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    var result = _servicePackageManager.EditServicePackage(_servicePackage, newPackage);
                    if (!result)
                    {
                        throw new ApplicationException("Service Package was not updated properly!");
                    }
                    MessageBox.Show(_servicePackage.ServicePackageID.ToString() + " was successfully edited!");
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
                    MessageBox.Show(message, "Edit Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Container for add form logic
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                var newPackage = new ServicePackage()
                {
                    Name = txtName.Text,
                    Description = txtName.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    var result = _servicePackageManager.AddServicePackage(newPackage);
                    if (!result)
                    {
                        throw new ApplicationException("Package was not properly added");
                    }
                    MessageBox.Show("Service Package was successfully added!");
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
                    //have to display the error
                    MessageBox.Show(message, "Add Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created 2018/02/22
        /// 
        /// Validates the controls in preparation for adding or editing
        /// </summary>
        /// <returns></returns>
        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!");
                return false;
            }
            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters!");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description cannot be empty!");
                return false;
            }
            if (!StringValidations.IsValidNamePropertyMaxSize(txtDescription.Text, 1000))
            {
                MessageBox.Show("Description cannot be over 1000 characters!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Click event to hangle canceling out of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Cancel?\nCanceling will discard any unsaved changes!", "Cancel Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}