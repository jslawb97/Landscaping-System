using DataObjects;
using Logic;
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

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmAddEditServiceOffering.xaml
    /// </summary>
    public partial class frmAddEditServiceOffering : Window
    {
        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// variables to hold ServiceOfferingManager, ServiceOffering, and AddEditMode
        /// </summary>
        private IServiceOfferingManager _serviceOfferingManager;
        private IServiceOfferingItemManager _serviceOfferingItemManager;
        private ServiceOffering _serviceOffering;
        private List<ServiceItem> _serviceItemList;
        private List<ServiceOfferingItem> _serviceOfferingItems;
        private DetailFormMode _mode;


        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// frm loader for edit and view
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/12
        /// 
        /// Added Service Item list to constructor
        /// </summary>
        public frmAddEditServiceOffering(IServiceOfferingManager serviceOfferingManager, IServiceOfferingItemManager serviceOfferingItemManager, List<ServiceItem> serviceItems, ServiceOffering serviceOffering, DetailFormMode mode)
        {
            _serviceOfferingManager = serviceOfferingManager;
            _serviceOfferingItemManager = serviceOfferingItemManager;
            _serviceItemList = serviceItems;
            _serviceOffering = serviceOffering;
            _mode = mode;
            _serviceOfferingItems = _serviceOfferingItemManager.RetrieveServiceOfferingItemByID(_serviceOffering.ServiceOfferingID);

            InitializeComponent();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// frm loader for add
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/12
        /// 
        /// Added Service Item list to constructor
        /// </summary>
        public frmAddEditServiceOffering(IServiceOfferingManager serviceOfferingManager, IServiceOfferingItemManager serviceOfferingItemManager, List<ServiceItem> serviceItems)
        {
            _serviceOfferingManager = serviceOfferingManager;
            _serviceOfferingItemManager = serviceOfferingItemManager;
            _serviceItemList = serviceItems;
            _mode = DetailFormMode.Add;

            InitializeComponent();
        }

        /// <summary>
        /// Marshall Sejkora
        /// Created: 2018/02/21
        /// 
        /// Jacob Conley
        /// Updated: 2018/04/12
        /// 
        /// Changed to use DetailFormMode and removed the service package combobox
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.View:
                    setupVeiwMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                default:
                    break;
            }
        }

        private void setupVeiwMode()
        {
            populateControls();
            setInputs();
            this.btnAddEdit.Visibility = Visibility.Hidden;
        }

        private void setupEditMode()
        {
            populateControls();
            setInputs();
        }

        private void setupAddMode()
        {
            setInputs();
        }

        private void setInputs()
        {
            switch (_mode)
            {
                case DetailFormMode.View:
                    lbServiceItems.ItemsSource = _serviceItemList;
                    lbServiceItems.DisplayMemberPath = "Name";
                    lbServiceItems.IsEnabled = false;
                    this.txtName.IsReadOnly = true;
                    this.txtDescription.IsReadOnly = true;
                    displayConnectedServiceItems();
                    break;
                case DetailFormMode.Edit:
                    lbServiceItems.ItemsSource = _serviceItemList;
                    lbServiceItems.DisplayMemberPath = "Name";
                    this.txtName.IsReadOnly = false;
                    this.txtDescription.IsReadOnly = false;
                    this.btnAddEdit.Content = "Save";
                    this.lblHeader.Content = "Editing Service Offering";
                    this.Title = "Edit Service Offering";
                    displayConnectedServiceItems();
                    break;
                case DetailFormMode.Add:
                    lbServiceItems.ItemsSource = _serviceItemList;
                    lbServiceItems.DisplayMemberPath = "Name";
                    this.txtName.IsReadOnly = false;
                    this.txtDescription.IsReadOnly = false;
                    this.Title = "Add Service Offering";
                    break;
                default:
                    break;
            }
        }

        private void displayConnectedServiceItems()
        {
            List<ServiceItem> selectedItems = new List<ServiceItem>();
            foreach (ServiceItem item in lbServiceItems.Items)
            {
                if (_serviceOfferingItems.Exists(s => s.ServiceItemID == item.ServiceItemID))
                {
                    // set the items as selected as they are found somehow...
                    selectedItems.Add(item);
                }
            }
            lbServiceItems.SelectedItemsOverride = selectedItems;
        }

        private void populateControls()
        {
            //this.cmbServicePackage.SelectedItem = _serviceOffering.ServicePackageID;
            this.txtName.Text = _serviceOffering.Name;
            this.txtDescription.Text = _serviceOffering.Description;
        }



        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtName.Text == "")
            {
                MessageBox.Show("Invalid Name", "ServiceOffering must have a Name!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (this.txtDescription.Text == "")
            {
                MessageBox.Show("Invalid Description", "ServiceOffering must have a Description!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var newServiceOffering = new ServiceOffering()
            {
                ServiceOfferingID = 0,
                Name = this.txtName.Text,
                Description = this.txtDescription.Text
            };

            var result = 0;

            try
            {
                if (_mode == DetailFormMode.Add)
                {
                    result = _serviceOfferingManager.CreateServiceOffering(newServiceOffering);
                    // Create service offering item connections in database.
                    foreach (ServiceItem serviceItem in lbServiceItems.SelectedItems)
                    {
                        var offeringItem = new ServiceOfferingItem()
                        {
                            ServiceItemID = serviceItem.ServiceItemID,
                            ServiceOfferingID = result
                        };
                        _serviceOfferingItemManager.CreateServiceOfferingItem(offeringItem);
                    }

                }
                else if (_mode == DetailFormMode.Edit)
                {
                    result = _serviceOfferingManager.EditServiceOffering(_serviceOffering, newServiceOffering); List<ServiceItem> newServiceItems = (List<ServiceItem>)lbServiceItems.SelectedItems;
                    foreach (var serviceItem in _serviceOfferingItems)
                    {
                        if (newServiceItems.Exists(s => s.ServiceItemID == serviceItem.ServiceItemID) == false)
                        {
                            _serviceOfferingItemManager.DeleteServiceOfferingItem(serviceItem);
                        }
                    }
                    foreach (var serviceItem in newServiceItems)
                    {
                        if (_serviceOfferingItems.Exists(s => s.ServiceItemID == serviceItem.ServiceItemID) == false)
                        {
                            _serviceOfferingItemManager.CreateServiceOfferingItem(new ServiceOfferingItem()
                            {
                                ServiceItemID = serviceItem.ServiceItemID,
                                ServiceOfferingID = _serviceOffering.ServiceOfferingID
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong!", ex.Message + ex.InnerException.Message,
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
