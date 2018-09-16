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
    /// Interaction logic for frmAddEditSpecialOrderForm.xaml
    /// </summary>
    public partial class frmAddEditSpecialOrderForm : Window
    {
        private ISpecialOrderManager _specialOrderManager;
        private ISpecialOrderItemManager _specialOrderItemManager = new SpecialOrderItemManager();
        private ISpecialOrderLineManager _specialOrderLineManager = new SpecialOrderLineManager();
        private IVendorManager _vendorManager = new VendorManager();
        private DetailFormMode _mode;
        private SpecialOrder _specialOrder;
        private int _employeeID;
        private List<SpecialItem> _specialItemList;
        private List<Vendor> _vendorList;
        private List<SpecialOrderItemDetail> _entireOrderItemDetailList = new List<SpecialOrderItemDetail>();
        private List<SpecialOrderItemDetail> _filteredOrderItemDetailList = new List<SpecialOrderItemDetail>();

        private List<SpecialOrderLineDetail> _orderLineDetails = new List<SpecialOrderLineDetail>();
        private List<SpecialOrderLineDetail> _originalOrderLineDetails;
        private ISupplyStatusManager _supplyStatusManager = new SupplyStatusManager();


        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Constructor for edit mode
        /// </summary>
        /// <param name="specialOrderManager"></param>
        /// <param name="mode"></param>
        /// <param name="specialOrder"></param>
        public frmAddEditSpecialOrderForm(ISpecialOrderManager specialOrderManager,
            DetailFormMode mode, SpecialOrder specialOrder, List<SpecialItem> specialItemList,
            ISpecialOrderItemManager specialOrderItemManager, int employeeID)
        {
            this._specialOrderManager = specialOrderManager;
            this._mode = mode;
            this._specialOrder = specialOrder;
            this._specialItemList = specialItemList;
            this._specialOrderItemManager = specialOrderItemManager;
            this._employeeID = employeeID;
            InitializeComponent();
            setupStatusComboBox();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Constructor for Add mode
        /// </summary>
        /// <param name="specialOrderManager"></param>
        public frmAddEditSpecialOrderForm(ISpecialOrderManager specialOrderManager,
            List<SpecialItem> specialItemList,
            ISpecialOrderItemManager specialOrderItemManager, int employeeID)
        {
            this._specialOrderManager = specialOrderManager;
            this._specialOrderItemManager = specialOrderItemManager;
            this._specialItemList = specialItemList;
            this._employeeID = employeeID;
            InitializeComponent();
            setupStatusComboBox();
            populateVendorComboBox();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Constructor for _view mode
        /// </summary>
        /// <param name="specialOrderManager"></param>
        /// <param name="mode"></param>
        /// <param name="specialOrder"></param>
        public frmAddEditSpecialOrderForm(ISpecialOrderManager _specialOrderManager, DetailFormMode view, SpecialOrder specialOrder, List<SpecialItem> specialItemList)
        {
            this._specialOrderManager = _specialOrderManager;
            this._mode = view;
            this._specialOrder = specialOrder;
            this._specialItemList = specialItemList;
            InitializeComponent();
            setupStatusComboBox();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Used for making a mock list of statuses
        /// </summary>
        private void setupStatusComboBox()
        {
            //List<ResupplyOrder> resupplyOrderList = new List<ResupplyOrder>();
            List<string> supplyStatusIDList = new List<string>();
            try
            {
                supplyStatusIDList = _supplyStatusManager.RetrieveSupplyStatusList();
                this.cbxStatusID.ItemsSource = supplyStatusIDList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving a list of resupply orders" + ex);
            }
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// validates edited special order
        /// </summary>
        /// <param name="specialOrder"></param>
        /// <returns></returns>
        private bool validateInputs()
        {
            if (txtEmployeeID.Text == "")
            {
                MessageBox.Show("Employee ID cannot be blank.");
                return false;
            }
            //if (txtJobID.Text == "")
            //{
            //    MessageBox.Show("Job ID cannot be blank.");
            //    return false;
            //}
            //if (dpckDate.Text == "")
            //{
            //    MessageBox.Show("Date cannot be blank.");
            //    return false;
            //}
            if (cbxStatusID.Text == "")
            {
                MessageBox.Show("Status cannot be blank.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Sets the page controls for editing
        /// </summary>
        private void setupEditMode()
        {
            populateSpecialItemDetailGrid();

            lblHeader.Content = "Making changes to a Special Order";
            btnSave.Content = "Save Changes";
            Title = "Edit a Special Order";
            cbxStatusID.IsReadOnly = true;
            dgSpecialItems.ItemsSource = _filteredOrderItemDetailList;
            
            populateOrderItems();
            populateControls();

            copyOrderItems();
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Sets the page controls for adding a new order
        /// </summary>
        private void setupAddMode()
        {
            populateSpecialItemDetailGrid();
            btnSave.Content = "Add";
            Title = "Add a new Special Order";
            cbxStatusID.SelectedIndex = 0;
            cbxStatusID.IsEnabled = false;
            lblSpecialOrderDate.Content = DateTime.Now.ToString();
            dgSpecialItems.ItemsSource = _filteredOrderItemDetailList;
            txtEmployeeID.Text = _employeeID.ToString();
            
        }

        /// <summary>
        /// Reuben Cassell
        /// 3/21/2018
        /// 
        /// Closes out of the window when the Cancel button is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Sets the page controls for viewing an order.
        /// </summary>
        private void setupViewMode()
        {
            lblHeader.Content = "Viewing Special Order: " + _specialOrder.SpecialOrderID;
            Title = "Viewing a Special Order";
            btnAdd.Visibility = Visibility.Hidden;
            btnRemove.Visibility = Visibility.Hidden;
            cbxStatusID.IsReadOnly = true;
            txtJobID.IsReadOnly = true;
            cbxStatusID.IsEnabled = false;
            dgSpecialItems.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            lblSpecialOrderItem.Visibility = Visibility.Hidden;
            cbxVendor.Visibility = Visibility.Hidden;
            populateOrderItems();
            populateControls();

        }

        /// <summary>
        /// Reuben Cassell
        /// Created 4/6/2018
        /// 
        /// Creates SpecialOrderLineDetail list for display
        /// in dgSpecialOrderItems.
        /// </summary>
        private void populateOrderItems()
        {
            try
            {
                List<SpecialOrderLine> lines = new List<SpecialOrderLine>();
                lines = _specialOrderLineManager.RetrieveSpecialOrderLineBySpecialOrderID(_specialOrder.SpecialOrderID);

                if (_originalOrderLineDetails == null)
                {
                    _originalOrderLineDetails = new List<SpecialOrderLineDetail>();

                    foreach (var line in lines)
                    {
                        SpecialItem item = new SpecialItem();
                        item = _specialItemList.Find(n => n.SpecialOrderItemID == line.SpecialOrderItemID);

                        SpecialOrderLineDetail lineDetail = new SpecialOrderLineDetail();
                        lineDetail.ItemName = item.Name;
                        lineDetail.Line = line;

                        _originalOrderLineDetails.Add(lineDetail);
                    }
                }



                if (_originalOrderLineDetails.Count == 0 || _originalOrderLineDetails == null)
                {
                    MessageBox.Show("This order is empty.");
                }
                else
                {
                    dgSpecialOrderItems.ItemsSource = _originalOrderLineDetails;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("This Order is Empty");
            }
        }

        private void copyOrderItems()
        {
            if (_originalOrderLineDetails != null)
            {
                foreach (var line in _originalOrderLineDetails)
                {
                    _orderLineDetails.Add(new SpecialOrderLineDetail
                    {
                        ItemName = line.ItemName,
                        Line = new SpecialOrderLine
                        {
                            SpecialOrderLineID = line.Line.SpecialOrderLineID,
                            SpecialOrderID = line.Line.SpecialOrderID,
                            SpecialOrderItemID = line.Line.SpecialOrderItemID,
                            Quantity = line.Line.Quantity
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Populates the UI with data from the given order
        /// </summary>
        private void populateControls()
        {
            txtEmployeeID.Text = _specialOrder.EmployeeID.ToString();
            populateVendorComboBox();
            txtJobID.Text = _specialOrder.JobID.ToString();
            lblSpecialOrderDate.Content = _specialOrder.Date.Date.ToString();
            cbxStatusID.SelectedValue = _specialOrder.SupplyStatusID;
            cbxVendor.SelectedValue = _vendorList.Find(v => v.VendorID == _specialOrder.VendorID);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 3/21/2018
        /// 
        /// Sets up the window depending on what button was selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            switch (_mode)
            {
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                case DetailFormMode.View:
                    setupViewMode();
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.View)
            {
                _mode = DetailFormMode.Edit;
                setupEditMode();
                return;
            }
            SpecialOrder specialOrder = new SpecialOrder();

            switch (_mode)
            {
                case DetailFormMode.Add:

                    if (dgSpecialOrderItems.Items.Count == 0)
                    {
                        var result = MessageBox.Show("Are you sure you wish to cancel?",
                            "No items added to the order",
                            MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (result == MessageBoxResult.Yes)
                        {
                            this.DialogResult = false;
                        }
                        else
                        {
                            MessageBox.Show("Please add items to the order.");
                        }
                    }

                    int jobID;

                    SpecialOrder newOrder = new SpecialOrder()
                    {
                        EmployeeID = int.Parse(txtEmployeeID.Text),
                        Date = DateTime.Parse(lblSpecialOrderDate.Content.ToString()),
                        SupplyStatusID = (string)cbxStatusID.SelectedItem,
                        VendorID = ((Vendor)cbxVendor.SelectedItem).VendorID
   
                    };

                    if (int.TryParse(txtJobID.Text, out jobID) == false)
                    {
                        newOrder.JobID = null;
                    }
                    else
                    {
                        newOrder.JobID = jobID;
                    }

                    try
                    {
                        int newSpecialOrderID = _specialOrderManager.CreateSpecialOrder(newOrder);

                        foreach (var line in _orderLineDetails)
                        {
                            line.Line.SpecialOrderID = newSpecialOrderID;
                            _specialOrderLineManager.CreateSpecialOrderLine(line.Line);
                        }

                        MessageBox.Show("Special Order  #" + newSpecialOrderID + " Created");
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {

                        var message = ex.Message;
                        MessageBox.Show(message, "Add Failed!",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    break;
                case DetailFormMode.Edit:
                    if (validateInputs() == false)
                    {
                        return;
                    }


                    specialOrder = new SpecialOrder()
                    {
                        EmployeeID = int.Parse(txtEmployeeID.Text),
                        Date = DateTime.Parse(lblSpecialOrderDate.Content.ToString()),
                        SupplyStatusID = cbxStatusID.SelectedItem.ToString(),
                        VendorID = ((Vendor)cbxVendor.SelectedItem).VendorID
                    };

                    if (int.TryParse(txtJobID.Text, out jobID) == false)
                    {
                        specialOrder.JobID = null;
                    }
                    else
                    {
                        specialOrder.JobID = jobID;
                    }

                    try
                    {
                        foreach (var line in _originalOrderLineDetails)
                        {
                            if (_orderLineDetails.Exists(l => l.Line.SpecialOrderLineID == line.Line.SpecialOrderLineID) == false)
                            {
                                _specialOrderLineManager.DeleteSpecialOrderLine(line.Line.SpecialOrderLineID);
                            }
                        }
                        foreach (var line in _orderLineDetails)
                        {
                            if (_originalOrderLineDetails.Exists(l => l.Line.SpecialOrderLineID == line.Line.SpecialOrderLineID && l.ItemName.Equals(line.ItemName)))
                            {
                                var originalLine = _originalOrderLineDetails.Find(l => l.Line.SpecialOrderLineID == line.Line.SpecialOrderLineID && l.ItemName.Equals(line.ItemName));
                                if (originalLine.Line.Quantity != line.Line.Quantity)
                                {
                                    line.Line.SpecialOrderID = _specialOrder.SpecialOrderID;
                                    _specialOrderLineManager.EditSpecialOrderLine(originalLine.Line, line.Line);
                                }
                            }
                            else
                            {
                                line.Line.SpecialOrderID = _specialOrder.SpecialOrderID;
                                _specialOrderLineManager.CreateSpecialOrderLine(line.Line);
                            }
                        }

                        int rowCount = _specialOrderManager.EditSpecialOrder(specialOrder, _specialOrder);

                        MessageBox.Show("Special Order changed.\nRows Affected: " + rowCount);
                        this.DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                        MessageBox.Show(message, "Edit Failed!",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    break;
                case DetailFormMode.View:
                    break;
                default:
                    break;
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgSpecialItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to add to the order.");
            }
            else
            {
                
                SpecialItem selectedItem = ((SpecialOrderItemDetail)dgSpecialItems.SelectedItem).SpecialItem;
                if (_orderLineDetails == null)
                {
                    _orderLineDetails = new List<SpecialOrderLineDetail>();
                }

                // this should:
                // create a new specialorderlinedetail
                // pass it to an edit form
                // showdialog the form
                // if it returns true, add it to the list
                // else abandon it

                var frmAddOrderLine = new frmAddEditSpecialOrderLine(selectedItem.Name, selectedItem.SpecialOrderItemID, _orderLineDetails);
                var result = frmAddOrderLine.ShowDialog();
                if (result == true)
                {

                    //dgSpecialOrderItems.ItemsSource = null;
                    //dgSpecialOrderItems.ItemsSource = _orderLineDetails;

                    dgSpecialOrderItems.ItemsSource = null;
                    _orderLineDetails = frmAddOrderLine.OrderLineDetails;
                }
                dgSpecialOrderItems.ItemsSource = null;                
                dgSpecialOrderItems.ItemsSource = _orderLineDetails;
            }

            disableEnableVendorComboBox();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgSpecialOrderItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to remove.");
            }
            else
            {
                disableEnableVendorComboBox();
                SpecialOrderLineDetail specialOrderLineDetail = (SpecialOrderLineDetail)dgSpecialOrderItems.SelectedItem;
                var frmRemoveOrderItem =
                    new frmAddEditSpecialOrderLine(specialOrderLineDetail, _orderLineDetails, DetailFormMode.Edit);
                var result = frmRemoveOrderItem.ShowDialog();
                if (result == true)
                {
                    _orderLineDetails = frmRemoveOrderItem.OrderLineDetails;
                }
                dgSpecialOrderItems.ItemsSource = null;
                dgSpecialOrderItems.ItemsSource = _orderLineDetails;
            }
            disableEnableVendorComboBox();
        }

        private void populateVendorComboBox()
        {
            try
            {
                _vendorList = _vendorManager.RetrieveVendorListByActive().OrderBy(v => v.Name).ToList();

                
                if (_vendorList.Count == 0)
                {
                    MessageBox.Show("No Vendors avaiable to choose from.");
                }

                cbxVendor.ItemsSource = _vendorList;
                cbxVendor.DisplayMemberPath = "Name";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve Vendors", ex.Message);
            }


        }

        private void populateSpecialItemDetailGrid()
        {
            try
            {
                _entireOrderItemDetailList = _specialOrderItemManager.RetrieveSpecialOrderItemDetail();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Unable to retrieve Special Items", ex.Message);
            }
        }

        private void cbxVendor_DropDownClosed(object sender, EventArgs e)
        {
            filterSpecialItemsByVendor();
            dgSpecialItems.ItemsSource = null;
            dgSpecialItems.ItemsSource = _filteredOrderItemDetailList;
        }

        private void filterSpecialItemsByVendor()
        {
            _filteredOrderItemDetailList.Clear();
            var vendor = (Vendor)cbxVendor.SelectedItem;
            if (cbxVendor.SelectedItem != null)
            {
                foreach (var item in _entireOrderItemDetailList)
                {
                    if (item.VendorID == vendor.VendorID)
                    {
                        _filteredOrderItemDetailList.Add(item);
                    }
                }
            }
            
            
        }

        private void disableEnableVendorComboBox()
        {
            if (dgSpecialOrderItems.Items.Count == 0)
            {
                this.cbxVendor.IsEnabled = true;
            }
            else
            {
                this.cbxVendor.IsEnabled = false;
            }
        }

        private void cbxVendor_GotFocus(object sender, RoutedEventArgs e)
        {
            disableEnableVendorComboBox();
            if (cbxVendor.IsEnabled == false)
            {
                MessageBox.Show("Please remove all current order items before changing vendors");
            }
        }
    }
}
