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
using DataObjects;
using Logic;

namespace WPFPresentation
{
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/01/31
    /// 
    /// Interaction logic for frmAddEditSpecialOrderItem.xaml where users can add or edit special order items
    /// </summary>
    public partial class frmAddEditSpecialOrderItem : Window
    {
        private ISpecialOrderItemManager _specialOrderItemManager;
        private SpecialItem _specialItem;

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Window Constructor for adding special order items
        /// </summary>
        /// <param name="_specialOrderItemManager">Dependency for a ISpecialOrderItemManager</param>
        public frmAddEditSpecialOrderItem(ISpecialOrderItemManager _specialOrderItemManager)
        {
            this._specialOrderItemManager = _specialOrderItemManager;
            InitializeComponent();
            setupAddForm();
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Window Constructor for editing a special order item
        /// </summary>
        /// <param name="_specialOrderItemManager">Dependency for a ISpecialOrderItemManager</param>
        /// <param name="specialItem">The special order item to be edited</param>
        public frmAddEditSpecialOrderItem(ISpecialOrderItemManager _specialOrderItemManager, SpecialItem specialItem)
        {
            this._specialOrderItemManager = _specialOrderItemManager;
            this._specialItem = specialItem;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Sets fields to reflect Add functionality.
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Special Order Item";
            btnAddEdit.Content = "Add";
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Sets fields to reflect Edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Special Order Item: " + _specialItem.SpecialOrderItemID;
            txtName.Text = _specialItem.Name;
            chkActive.IsChecked = _specialItem.Active;
            btnAddEdit.Content = "Save";
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Button click event to submit Add or Edit functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_specialItem == null)
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
        /// Created: 2018/01/31
        /// 
        /// Validates fields and submits Edit data
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                var newItem = new SpecialItem()
                {
                    Name = txtName.Text,
                    Active = (bool)chkActive.IsChecked

                };

                try
                {
                    var result = _specialOrderItemManager.EditSpecialOrderItem(_specialItem, newItem);
                    if (!result)
                    {
                        throw new ApplicationException("Could not update Special Order Item to the database");
                    }
                    MessageBox.Show(_specialItem.SpecialOrderItemID + " was successfully edited!");
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
                    MessageBox.Show(message, "Edit Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Validates all the fields with proper error messages when invalid
        /// </summary>
        /// <returns>True if all fields are valid, false otherwise</returns>
        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters!");
                return false;
            }
            else if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/01/31
        /// 
        /// Validates fields and submits Add data
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                var newItem = new SpecialItem()
                {
                    Name = txtName.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    var result = _specialOrderItemManager.AddSpecialOrderItem(newItem);
                    if (result)
                    {

                        MessageBox.Show("Special Item was successfully added!");
                    }
                    else
                    {

                        MessageBox.Show("Special Item was not added!");
                    }
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
        /// Created: 2018/01/31
        /// 
        /// Button click event to prompt user to exit the window
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
