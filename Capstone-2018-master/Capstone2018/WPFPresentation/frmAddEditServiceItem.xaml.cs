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
    /// Interaction logic for frmAddEditServiceItem.xaml
    /// </summary>
    public partial class frmAddEditServiceItem : Window
    {

        private IServiceItemManager _serviceItemManager;
        private ServiceOfferingItemManager _serviceOfferingItemManager = new ServiceOfferingItemManager();
        private ServiceItem _serviceItem;
        private List<ServiceOffering> _serviceOfferings;
        private List<ServiceOfferingItem> _serviceOfferingItems;

        public frmAddEditServiceItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays a dialog with the fields required to create a new Inspection Checklist
        /// </summary>
        /// <param name="serviceItemManager">The ServiceItem Manager</param>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        public bool? ShowAddDialog(IServiceItemManager serviceItemManager)
        {
            this.lblHeader.Content = "Adding a new Service Item";

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new ServiceItem
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value,
                    ServiceItemOffering = new ServiceItemOffering
                    {
                        ServiceItemOfferingID = 1000000
                    }
                };

                serviceItemManager.AddServiceItem(newItem);

                Close();
            };

            return ShowDialog();
        }

        /// <summary>
        /// Displays a dialog with the fields required to edit an existing ServiceItem
        /// </summary>
        /// <param name="serviceItemManager">The ServiceItem Manager</param>
        /// <param name="serviceItem">The ServiceItem Manager</param>
        /// ///
        /// <returns>A System.Nullable`1 value of type System.Boolean that specifies whether the activity</returns>
        /// <remarks>
        /// Zach Murphy
        /// Updated 2018/02/1
        /// </remarks>
        public bool? ShowEditDialog(IServiceItemManager serviceItemManager,
            ServiceItem serviceItem)
        {
            this.lblHeader.Content = "Editing an existing Service Item";
            this.btnAddEdit.Content = "Edit";
            this.txtName.Text = serviceItem.Name;
            this.txtDescription.Text = serviceItem.Description;
            this.chkActive.IsChecked = serviceItem.Active;

            this.btnAddEdit.Click += (object sender, RoutedEventArgs e) =>
            {
                var newItem = new InspectionChecklist
                {
                    Name = this.txtName.Text,
                    Description = this.txtDescription.Text,
                    Active = this.chkActive.IsChecked.Value
                };

                if (serviceItem.Description.Equals(newItem.Description) && serviceItem.Active && !newItem.Active)
                {
                    // TODO: Deactivate
                }
                else
                {
                    // TODO: Edit
                }

                Close();
            };

            return ShowDialog();
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/03/08
        /// 
        /// Window Constructor for edit service item 
        /// 
        /// Updated: 2018/04/06
        /// Added a list of service offering items for the constructor
        /// </summary>
        /// <param name="_serviceItemManager">Dependency for a IServiceItemManager</param>
        public frmAddEditServiceItem(IServiceItemManager _serviceItemManager, ServiceItem serviceItem, List<ServiceOffering> serviceOfferings)
        {
            this._serviceItemManager = _serviceItemManager;
            this._serviceItem = serviceItem;
            this._serviceOfferings = serviceOfferings;

            InitializeComponent();
            setupEditForm();
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Window Constructor for adding Service Item
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/06
        /// 
        /// Added a list of service offering items for the constructor
        /// </summary>
        /// <param name="serviceItemManager">Dependency for a IServiceItemManager</param>
        public frmAddEditServiceItem(IServiceItemManager serviceItemManager, List<ServiceOffering> serviceOfferings)
        {
            this._serviceItemManager = serviceItemManager;
            this._serviceOfferings = serviceOfferings;

            InitializeComponent();
            setupAddForm();
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Sets fields to Add functionality.
        /// </summary>
        private void setupAddForm()
        {
            lblHeader.Content = "Adding a new Service Item";
            btnAddEdit.Content = "Add";
            lbServiceOfferings.ItemsSource = _serviceOfferings;
            lbServiceOfferings.DisplayMemberPath = "Name";
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/24
        /// 
        /// Sets fields to Edit functionality.
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/06
        /// 
        /// Added populate ListBox to show current service offerings associated with the current item
        /// </summary>
        private void setupEditForm()
        {
            lblHeader.Content = "Editing Service Item: " + _serviceItem.ServiceItemID;
            txtName.Text = _serviceItem.Name;
            txtDescription.Text = _serviceItem.Description;
            chkActive.IsChecked = _serviceItem.Active;
            btnAddEdit.Content = "Save";
            lbServiceOfferings.ItemsSource = _serviceOfferings;
            lbServiceOfferings.DisplayMemberPath = "Name";
            _serviceOfferingItems = _serviceOfferingItemManager.RetrieveServiceOfferingItemByServiceItemID(_serviceItem.ServiceItemID);
            List<ServiceOffering> selectedOfferings = new List<ServiceOffering>();
            foreach (ServiceOffering item in lbServiceOfferings.Items)
            {
                if (_serviceOfferingItems.Exists(s => s.ServiceOfferingID == item.ServiceOfferingID))
                {
                    // set the items as selected as they are found somehow...
                    selectedOfferings.Add(item);
                }
            }
            lbServiceOfferings.SelectedItemsOverride = selectedOfferings;
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/24
        /// 
        /// Validates fields and submits Edit data
        /// </summary>
        private void performEdit()
        {
            if (validateFields())
            {
                var newServiceItem = new ServiceItem()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Active = (bool)chkActive.IsChecked

                };

                try
                {
                    int result = _serviceItemManager.EditServiceItemByID(_serviceItem, newServiceItem);
                    if (result != 1)
                    {
                        throw new ApplicationException("Could not update Service Item to the database");
                    }
                    List<ServiceOffering> newServiceItems = (List<ServiceOffering>)lbServiceOfferings.SelectedItems;
                    foreach (var serviceOffering in _serviceOfferingItems)
                    {
                        if (newServiceItems.Exists(s => s.ServiceOfferingID == serviceOffering.ServiceOfferingID) == false)
                        {
                            _serviceOfferingItemManager.DeleteServiceOfferingItem(serviceOffering);
                        }
                    }
                    foreach (var serviceOffering in newServiceItems)
                    {
                        if (_serviceOfferingItems.Exists(s => s.ServiceOfferingID == serviceOffering.ServiceOfferingID) == false)
                        {
                            _serviceOfferingItemManager.CreateServiceOfferingItem(new ServiceOfferingItem()
                            {
                                ServiceItemID = _serviceItem.ServiceItemID,
                                ServiceOfferingID = serviceOffering.ServiceOfferingID
                            });
                        }
                    }
                    MessageBox.Show(_serviceItem.ServiceItemID + " was successfully edited!");
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
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Validates all the fields with proper error messages when invalid
        /// </summary>
        /// <returns>True if all fields are valid, false otherwise</returns>
        private bool validateFields()
        {
            if (!StringValidations.IsValidNamePropertyMaxSize(txtDescription.Text, 1000))
            {
                MessageBox.Show("Description cannot be over 1000 characters!");
                return false;
            }
            else if (!StringValidations.IsValidNamePropertyEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description  cannot be empty!");
                return false;
            }

            if (!StringValidations.IsValidNamePropertyMaxSize(txtName.Text, 100))
            {
                MessageBox.Show("Name cannot be over 100 characters!");
                return false;
            }
            else if (!StringValidations.IsValidNamePropertyEmpty(txtName.Text))
            {
                MessageBox.Show("Name  cannot be empty!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Amanda Tampir
        /// Created: 2018/02/15
        /// 
        /// Validates fields and submits Add data
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/06
        /// 
        /// Added service offering connections to database.
        /// </summary>
        private void performAdd()
        {
            if (validateFields())
            {
                var serviceItem = new ServiceItem()
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    Active = (bool)chkActive.IsChecked
                };

                try
                {

                    int result = _serviceItemManager.AddServiceItem(serviceItem);
                    MessageBox.Show(result + " was successfully added!");
                    // Create service offering item connections in database.
                    foreach (ServiceOffering serviceOffering in lbServiceOfferings.SelectedItems)
                    {
                        var offeringItem = new ServiceOfferingItem()
                        {
                            ServiceItemID = result,
                            ServiceOfferingID = serviceOffering.ServiceOfferingID
                        };
                        _serviceOfferingItemManager.CreateServiceOfferingItem(offeringItem);
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

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_serviceItem == null)
            {
                performAdd();
            }
            else
            {
                performEdit();
            }


        }

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
