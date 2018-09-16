using DataObjects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Logic;
using System;

namespace WPFPresentation {
    /// <summary>
    /// Zachary Hall
    /// Created: 2018/02/01
    /// 
    /// Add and Edit form for Supply Items
    /// </summary>
    public partial class frmAddEditSupply : Window
    {

        private ISupplyItemManager _supplyItemManager;
        private SupplyItem _supplyItem;

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Constructor that is used to perform the Add function
        /// </summary>
        /// <param name="supplyItemManager"></param>
        public frmAddEditSupply(ISupplyItemManager supplyItemManager)
        {
            _supplyItemManager = supplyItemManager;
            InitializeComponent();
            setupAddForm();
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Constructor that is used to perform the Edit function
        /// </summary>
        /// <param name="supplyItemManager"></param>
        /// <param name="supplyItem"></param>
        public frmAddEditSupply(ISupplyItemManager supplyItemManager, SupplyItem supplyItem)
        {
            _supplyItemManager = supplyItemManager;
            _supplyItem = supplyItem;
            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Populates fields to reflect the Edit functionality
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Supply Item " + _supplyItem.SupplyItemID;
            btnAddEdit.Content = "Save";
            txtName.Text = _supplyItem.Name;
            txtDescription.Text = _supplyItem.Description;
            txtLocation.Text = _supplyItem.Location;
            numInStock.Value = _supplyItem.QuantityInStock;
            numReorderAmount.Value = _supplyItem.ReorderQuantity;
            numReorderLevel.Value = _supplyItem.ReorderLevel;
            chkActive.IsChecked = _supplyItem.Active;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Populates fields to reflect the Add functionality
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Supply Item";
            btnAddEdit.Content = "Add";
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Submit form event from data filled out by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_supplyItem == null)
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
        /// Created: 2018/02/01
        /// 
        /// Validates and submits Edit form data
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                var newItem = new SupplyItem()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Location = txtLocation.Text,
                    QuantityInStock = (int)numInStock.Value,
                    ReorderLevel = (int)numReorderLevel.Value,
                    ReorderQuantity = (int)numReorderAmount.Value,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    bool result = _supplyItemManager.EditSupplyItem(_supplyItem, newItem);
                    if (!result)
                    {
                        throw new ApplicationException("Supply Item was not updated properly!");
                    }

                    MessageBox.Show(_supplyItem.SupplyItemID.ToString() + " was successfully edited!");
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
        /// Created: 2018/02/01
        /// 
        /// Validates and submits Add form data
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                var newItem = new SupplyItem()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Location = txtLocation.Text,
                    QuantityInStock = (int)numInStock.Value,
                    ReorderLevel = (int)numReorderLevel.Value,
                    ReorderQuantity = (int)numReorderAmount.Value,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {
                    bool result = _supplyItemManager.CreateSupplyItem(newItem);
                    if (result)
                    {
                        MessageBox.Show("Supply Item was successfully added!");
                    }
                    else
                    {
                        MessageBox.Show("Supply Item was not added!");
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
        /// Created: 2018/02/01
        /// 
        /// Validates all fields with appropriate Error messaging.
        /// </summary>
        /// <returns>True if all fields are valid, false otherwise</returns>
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

            if (!StringValidations.IsValidNamePropertyEmpty(txtLocation.Text))
            {
                MessageBox.Show("Location cannot be empty!");
                return false;
            }
            if (!StringValidations.IsValidNamePropertyMaxSize(txtLocation.Text, 100))
            {
                MessageBox.Show("Location cannot be over 100 characters!");
                return false;
            }

            if (numInStock.Value == null)
            {
                MessageBox.Show("In Stock cannot be empty!");
                return false;
            }

            if (!IntegerValidations.IsValidQuantity((int)numInStock.Value))
            {
                MessageBox.Show("In Stock cannot be a negative value!");
                return false;
            }

            if (numReorderLevel.Value == null)
            {
                MessageBox.Show("Reorder Level cannot be empty!");
                return false;
            }

            if (!IntegerValidations.IsValidQuantity((int)numReorderLevel.Value))
            {
                MessageBox.Show("Reorder Level cannot be a negative value!");
                return false;
            }

            if (numReorderAmount.Value == null)
            {
                MessageBox.Show("Reorder Amount cannot be empty!");
                return false;
            }

            if (!IntegerValidations.IsValidQuantity((int)numReorderAmount.Value))
            {
                MessageBox.Show("Reorder Amount cannot be a negative value!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Zachary Hall
        /// Created: 2018/02/01
        /// 
        /// Form Cancel Event called by user clciking cancel button.
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
