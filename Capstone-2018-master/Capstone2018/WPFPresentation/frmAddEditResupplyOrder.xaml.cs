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
    /// Interaction logic for frmAddEditResupplyOrder.xaml
    /// </summary>
    public partial class frmAddEditResupplyOrder : Window
    {
        private Logic.IResupplyOrderManager _resupplyOrderManager;
        private ResupplyOrder _resupplyOrder;
        private DetailFormMode _mode;
        private ISupplyItemManager _supplyItemManager = new SupplyItemManager();
        private User _user;
        private List<ResupplyOrderLineDetail> _oldResupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
        private List<ResupplyOrderLineDetail> _resupplyOrderLineDetailList = new List<ResupplyOrderLineDetail>();
        private IResupplyOrderLineManager _resupplyOrderLineManager = new ResupplyOrderLineManager();
        private List<SupplyItemDetail> _supplyItemDetailList = new List<SupplyItemDetail>();
        private List<SupplyItemDetail> _entireSupplyItemDetailList = new List<SupplyItemDetail>();
        private IVendorManager _vendorManager = new VendorManager();
        private ISupplyStatusManager _supplyStatusManager = new SupplyStatusManager();

        public frmAddEditResupplyOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="_resupplyOrderManager"></param>
        /// <param name="user"></param>
        public frmAddEditResupplyOrder(IResupplyOrderManager _resupplyOrderManager, User user)
        {
            // TODO: Complete member initialization
            this._resupplyOrderManager = _resupplyOrderManager;
            this._mode = DetailFormMode.Add;
            this._user = user;
            InitializeComponent();
        }

        /// <summary>
        ///  Weston Olund
        /// 2018/04/05
        /// Constructor for edit mode
        /// </summary>
        /// <param name="_resupplyOrderManager"></param>
        /// <param name="resupplyOrder"></param>
        /// <param name="user"></param>
        /// <param name="resupplyOrderLineDetailList"></param>
        public frmAddEditResupplyOrder(IResupplyOrderManager _resupplyOrderManager, ResupplyOrder resupplyOrder, User user, List<ResupplyOrderLineDetail> resupplyOrderLineDetailList)
        {
            // TODO: Complete member initialization
            this._resupplyOrderManager = _resupplyOrderManager;
            this._resupplyOrder = resupplyOrder;
            this._mode = DetailFormMode.Edit;
            this._user = user;
            this._oldResupplyOrderLineDetailList = resupplyOrderLineDetailList;
            foreach (var item in _oldResupplyOrderLineDetailList)
            {
                var lineItem = new ResupplyOrderLineDetail();
                lineItem.NameOfItem = item.NameOfItem;
                lineItem.Price = item.Price;
                lineItem.Quantity = item.Quantity;
                lineItem.ResupplyOrderID = item.ResupplyOrderID;
                lineItem.ResupplyOrderLineID = item.ResupplyOrderLineID;
                lineItem.SupplyItemID = item.SupplyItemID;
                lineItem.VendorName = item.VendorName;
                lineItem.VendorID = item.VendorID;
                _resupplyOrderLineDetailList.Add(lineItem);
            }
            InitializeComponent();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to setup add mode
        /// </summary>
        private void setupAddMode()
        {
            this.lblHeader.Content = "Add a Resupply Order";
            this.txtEmployee.Text = _user.Employee.FirstName;
            this.lblOrderDate.Content = DateTime.Now.ToShortDateString();
            populateSupplyStatusIDComboBox();
            populateVendorIDComboBox();
            refreshSupplyItemDetailList();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        ///  method to get a list of items that have prices attached
        /// </summary>
        private void refreshSupplyItemDetailList()
        {
            dgItems.ItemsSource = null;
            dgItems.Items.Clear();
            dgItems.ItemsSource = _supplyItemDetailList;
        }

		/// <summary>
		///  Weston Olund
		/// 2018/04/20
		/// Retrieves a list of all supply items
		/// </summary>
		private void retrieveListOfSupplyItems()
        {
            try
            {
                _entireSupplyItemDetailList = _supplyItemManager.RetrieveSupplyItemDetailList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error retrieving data");
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to populate combo box
        /// </summary>
        private void populateSupplyStatusIDComboBox()
        {

            List<ResupplyOrder> resupplyOrderList = new List<ResupplyOrder>();
            List<string> supplyStatusIDList = new List<string>();
            try
            {
                supplyStatusIDList = _supplyStatusManager.RetrieveSupplyStatusList();
                this.cboStatus.ItemsSource = supplyStatusIDList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue retrieving a list of resupply orders" + ex);
            }

        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/13
        /// Populates the Vendor Combo box
        /// </summary>
        private void populateVendorIDComboBox()
        {
            var vendorList = new List<Vendor>();
            try
            {
                vendorList = _vendorManager.RetrieveVendorListByActive().OrderBy(v => v.Name).ToList();
                this.cboVendor.ItemsSource = vendorList;
                this.cboVendor.DisplayMemberPath = "Name";
                if(_mode == DetailFormMode.Edit)
                {
                    this.cboVendor.SelectedItem = vendorList.Find(v => v.VendorID == _resupplyOrder.VendorID);
                    this.cboVendor.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error retrieving a list of vendors.");
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to propely setup window when loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            retrieveListOfSupplyItems();
            this.Title = "Resupply Order";
            switch (_mode)
            {
                case DetailFormMode.Add:
                    setupAddMode();
                    break;
                case DetailFormMode.Edit:
                    setupEditMode();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05 
        /// method to setup edit mode
        /// </summary>
        private void setupEditMode()
        {
            this.lblHeader.Content = "Edit a Resupply Order";
            this.txtEmployee.Text = _user.Employee.FirstName;
            this.lblOrderDate.Content = _resupplyOrder.Date;
            this.btnAddOrder.Content = "Save";
            populateSupplyStatusIDComboBox();
            populateVendorIDComboBox();
            cboStatus.SelectedItem = _resupplyOrder.SupplyStatusID;
            if(cboVendor.SelectedItem != null)
            {
                populateSupplyItemBasedOnVendorSelected();
            }
            refreshSupplyItemDetailList();
            refreshOrderItemsList();
            cboVendorEnableDisable();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// When the add or save button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
                case DetailFormMode.Add:
                    addNewResupplyOrder();
                    break;
                case DetailFormMode.Edit:
                    editResupplyOrder();
                    break;
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to handle editing a supply order
        /// </summary>
        private void editResupplyOrder()
        {
            if (cboStatus.SelectedItem == null)
            {
                MessageBox.Show("You must select a status for the order.");
                return;
            }
            if (_resupplyOrderLineDetailList.Count < 1)
            {
                MessageBox.Show("You must add an item to the order.");
                return;
            }
            foreach (var item in _resupplyOrderLineDetailList)
            {
                if (item.Quantity <= 0)
                {
                    MessageBox.Show(item.NameOfItem + "'s quantity must be greater than 0");
                    return;
                }
            }
            var newResupplyOrder = new ResupplyOrder()
            {
                SupplyStatusID = this.cboStatus.SelectedValue.ToString(),
                ResupplyOrderID = _resupplyOrder.ResupplyOrderID,
                EmployeeID = _resupplyOrder.EmployeeID,
                Date = _resupplyOrder.Date
            };

            try
            {
                // edit resupply order
                if (_resupplyOrderManager.EditResupplyOrder(_resupplyOrder, newResupplyOrder))
                {
                    if (_oldResupplyOrderLineDetailList.Count == 0)
                    {
                        foreach (var item in _resupplyOrderLineDetailList)
                        {
                            var resupplyOrderLineDetailItem = new ResupplyOrderLineDetail();
                            resupplyOrderLineDetailItem.NameOfItem = item.NameOfItem;
                            resupplyOrderLineDetailItem.Price = item.Price;
                            resupplyOrderLineDetailItem.Quantity = item.Quantity;
                            resupplyOrderLineDetailItem.ResupplyOrderID = _resupplyOrder.ResupplyOrderID;
                            resupplyOrderLineDetailItem.ResupplyOrderLineID = item.ResupplyOrderLineID;
                            resupplyOrderLineDetailItem.SupplyItemID = item.SupplyItemID;
                            _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLineDetailItem);
                        }
                    }
                    else
                    {

                        // loop to add new line items to the order
                        for (int i = 0; i < _resupplyOrderLineDetailList.Count; i++)
                        {
                            for (int j = 0; j < _oldResupplyOrderLineDetailList.Count; j++)
                            {
                                if (_resupplyOrderLineDetailList[i].ResupplyOrderLineID == _oldResupplyOrderLineDetailList[j].ResupplyOrderLineID)
                                {
                                    break;
                                }
                                else if (_resupplyOrderLineDetailList[i].ResupplyOrderLineID != _oldResupplyOrderLineDetailList[j].ResupplyOrderLineID
                                    && j + 1 == _oldResupplyOrderLineDetailList.Count)
                                {
                                    var resupplyOrderLineDetailItem = new ResupplyOrderLineDetail();
                                    resupplyOrderLineDetailItem.NameOfItem = _resupplyOrderLineDetailList[i].NameOfItem;
                                    resupplyOrderLineDetailItem.Price = _resupplyOrderLineDetailList[i].Price;
                                    resupplyOrderLineDetailItem.Quantity = _resupplyOrderLineDetailList[i].Quantity;
                                    resupplyOrderLineDetailItem.ResupplyOrderID = _resupplyOrder.ResupplyOrderID;
                                    resupplyOrderLineDetailItem.ResupplyOrderLineID = _resupplyOrderLineDetailList[i].ResupplyOrderLineID;
                                    resupplyOrderLineDetailItem.SupplyItemID = _resupplyOrderLineDetailList[i].SupplyItemID;
                                    _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLineDetailItem);
                                }
                            }
                        }

                        // loops to edit existing line items and delete ones that no longer exist
                        for (int i = 0; i < _oldResupplyOrderLineDetailList.Count; i++)
                        {
                            for (int j = 0; j < _resupplyOrderLineDetailList.Count; j++)
                            {
                                // edit if already exists
                                if (_oldResupplyOrderLineDetailList[i].ResupplyOrderLineID == _resupplyOrderLineDetailList[j].ResupplyOrderLineID)
                                {
                                    try
                                    {
                                        _resupplyOrderLineManager.EditResupplyOrderLine(_oldResupplyOrderLineDetailList[i], _resupplyOrderLineDetailList[j]);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("There was an error editing the resupply order line item." + ex);
                                    }
                                }
                                // delete if in old list but not the updated list
                                else if (_oldResupplyOrderLineDetailList[i].ResupplyOrderLineID != _resupplyOrderLineDetailList[j].ResupplyOrderLineID
                                    && j + 1 == _resupplyOrderLineDetailList.Count)
                                {
                                    try
                                    {
                                        _resupplyOrderLineManager.DeleteResupplyOrderLineByResupplyOrderLineID(_oldResupplyOrderLineDetailList[i].ResupplyOrderLineID);
                                        

                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("There was an error deleting the resupply order line item." + ex);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error editing the Resupply Order." + ex);
                this.DialogResult = false;
            }
            MessageBox.Show("The resupply order was successfully edited");
            this.DialogResult = true;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to add a new resupply order
        /// </summary>
        private void addNewResupplyOrder()
        {
            int newResupplyOrderID = 0;

            if(cboVendor.SelectedItem == null)
            {
                MessageBox.Show("You must select a vendor for the order.");
                return;
            }
            
            if (cboStatus.SelectedItem == null)
            {
                MessageBox.Show("You must select a status for the order.");
                return;
            }
            if (_resupplyOrderLineDetailList.Count < 1)
            {
                MessageBox.Show("You must add an item to the order.");
                return;
            }

            foreach (var item in _resupplyOrderLineDetailList)
            {
                if (item.Quantity <= 0)
                {
                    MessageBox.Show(item.NameOfItem + "'s quantity must be greater than 0");
                    return;
                }
            }

            // Create ResupplyOrder
            var resupplyOrder = new ResupplyOrder();
            resupplyOrder.EmployeeID = _user.Employee.EmployeeID;
            resupplyOrder.Date = Convert.ToDateTime(lblOrderDate.Content);
            resupplyOrder.SupplyStatusID = this.cboStatus.SelectedItem.ToString();
            var vendor = (Vendor)this.cboVendor.SelectedItem;
            resupplyOrder.VendorID = vendor.VendorID;
            

            try
            {
                newResupplyOrderID = _resupplyOrderManager.CreateResupplyOrder(resupplyOrder);
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error creating the resupply order");
                this.DialogResult = false;
            }


            foreach (var item in _resupplyOrderLineDetailList)
            {
                // Create ResupplyOrderLine items
                ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();

                resupplyOrderLine.SupplyItemID = item.SupplyItemID;
                resupplyOrderLine.Quantity = item.Quantity;
                resupplyOrderLine.Price = item.Price;
                resupplyOrderLine.ResupplyOrderID = newResupplyOrderID;
                try
                {
                    _resupplyOrderLineManager.CreateResupplyOrderLine(resupplyOrderLine);
                }
                catch (Exception ex )
                {
                    MessageBox.Show("There was an error creating the resupply order line" + "\n" + ex);
                }   
            }
            MessageBox.Show("Resupply Order created");
            this.DialogResult = true;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// Method to handle >>> button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItems.Count != 1)
            {
                MessageBox.Show("You must select an item to add.");
                return;
            }
            else
            {
                addSupplyItem();
                
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to add a supply item to the line item list
        /// </summary>
        private void addSupplyItem()
        {
            
            ResupplyOrderLineDetail resupplyOrderLineDetail;
            SupplyItemDetail supplyItemDetail;
            ResupplyOrderLine resupplyOrderLine = new ResupplyOrderLine();
            supplyItemDetail = (SupplyItemDetail)dgItems.SelectedItem;
            resupplyOrderLineDetail = new ResupplyOrderLineDetail();
            resupplyOrderLineDetail.NameOfItem = supplyItemDetail.SupplyItem.Name;
            resupplyOrderLineDetail.SupplyItemID = supplyItemDetail.SupplyItem.SupplyItemID;
            resupplyOrderLineDetail.Quantity = 1;
            resupplyOrderLineDetail.Price = supplyItemDetail.PriceEach;
            resupplyOrderLineDetail.VendorName = supplyItemDetail.VendorName;
            resupplyOrderLineDetail.VendorID = supplyItemDetail.VendorID;

            // checking if the item is alreay in the item line list
            foreach (var item in _resupplyOrderLineDetailList)
            {
                if(item.VendorName == resupplyOrderLineDetail.VendorName 
                    && item.SupplyItemID == resupplyOrderLineDetail.SupplyItemID)
                {
                    MessageBox.Show("The item already exists in the order");
                    return;
                }
            }
            // checking for different vendors in the order
            foreach (var item in _resupplyOrderLineDetailList)
            {
                if(resupplyOrderLineDetail.VendorName != item.VendorName)
                {
                    MessageBox.Show("The resupply order must contain items from only one vendor");
                    return;
                }
            }

            _resupplyOrderLineDetailList.Add(resupplyOrderLineDetail);
            _supplyItemDetailList.Remove(supplyItemDetail);

            refreshOrderItemsList();
            cboVendorEnableDisable();
            //dgOrderItems.Focus();
            //dgOrderItems.CurrentCell = new DataGridCellInfo(dgOrderItems.Items[_resupplyOrderLineDetailList.Count - 1], dgOrderItems.Columns[2]);
            //dgOrderItems.BeginEdit();
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to refresh the list of line items
        /// </summary>
        private void refreshOrderItemsList()
        {
            dgOrderItems.ItemsSource = null;
            dgOrderItems.Items.Clear();
            dgOrderItems.ItemsSource = _resupplyOrderLineDetailList;

            // Removes items from the supply item detail list if they are in the order items list
            for (int i = 0; i < _resupplyOrderLineDetailList.Count; i++)
            {
                for (int j = 0; j < _supplyItemDetailList.Count; j++)
                {
                    if (_resupplyOrderLineDetailList[i].VendorID == _supplyItemDetailList[j].VendorID
                        && _resupplyOrderLineDetailList[i].SupplyItemID == _supplyItemDetailList[j].SupplyItem.SupplyItemID)
                    {
                        _supplyItemDetailList.Remove(_supplyItemDetailList[j]);
                    }
                }
            }
            dgItems.ItemsSource = null;
            dgItems.Items.Clear();
            dgItems.ItemsSource = _supplyItemDetailList;
        }

        /// <summary>
        /// method to handle <<< button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderItems.SelectedItems.Count != 1)
            {
                MessageBox.Show("You must select an item to remove.");
                return;
            }
            else
            {
                removeResupplyOrderLineItem();
                cboVendorEnableDisable();
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to remove supply line item and return it to the supply item list
        /// </summary>
        private void removeResupplyOrderLineItem()
        {
            ResupplyOrderLineDetail resupplyOrderLineDetail;
            resupplyOrderLineDetail = (ResupplyOrderLineDetail)dgOrderItems.SelectedItem;
            var supplyItemDetail = new SupplyItemDetail();

            // if the item removed doesn't match the vendor selected just remove the item and
            // do not add it to the available items list
            if(cboVendor.SelectedItem != null)
            {
                var vendor = (Vendor)cboVendor.SelectedItem;
                if(resupplyOrderLineDetail.VendorName != vendor.Name)
                {
                    _resupplyOrderLineDetailList.Remove(resupplyOrderLineDetail);
                    refreshOrderItemsList();
                    return;
                }
                // else add items to the supply item detail list
                else
                {
                    foreach (var item in _entireSupplyItemDetailList)
                    {
                        if(item.VendorID == resupplyOrderLineDetail.VendorID
                            && item.SupplyItem.SupplyItemID == resupplyOrderLineDetail.SupplyItemID)
                        {
                            _supplyItemDetailList.Add(item);
                            _resupplyOrderLineDetailList.Remove(resupplyOrderLineDetail);
                            refreshOrderItemsList();
                            refreshSupplyItemDetailList();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to handle cancel button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Weston Olund
        /// 2018/04/05
        /// method to add a item if double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResupplyItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            addSupplyItem();
        }

		/// <summary>
		///  Weston Olund
		/// 2018/04/20
		/// Method to handle the drop down being closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cboVendor_DropDownClosed(object sender, EventArgs e)
        {
            if(cboVendor.SelectedItem != null)
            {
                populateSupplyItemBasedOnVendorSelected();
            }
        }

		/// <summary>
		///  Weston Olund
		/// 2018/04/20
		/// Method to populate the datagrid based on the selected vendor
		/// </summary>
		private void populateSupplyItemBasedOnVendorSelected()
        {
            _supplyItemDetailList.RemoveRange(0, _supplyItemDetailList.Count);
            var vendor = (Vendor)cboVendor.SelectedItem;
            // adding all items for a certain vendor to appear in the grid
            foreach (var item in _entireSupplyItemDetailList)
            {
                if (item.VendorID == vendor.VendorID)
                {
                    _supplyItemDetailList.Add(item);
                }
                // making sure the order line does not already contain the item
                foreach (var lineItem in _resupplyOrderLineDetailList)
                {
                    if (lineItem.VendorName == item.VendorName
                        && lineItem.SupplyItemID == item.SupplyItem.SupplyItemID)
                    {
                        _supplyItemDetailList.Remove(item);
                    }
                }
            }
            dgItems.ItemsSource = null;
            dgItems.Items.Clear();
            dgItems.ItemsSource = _supplyItemDetailList;
        }

		/// <summary>
		///  Weston Olund
		/// 2018/04/20
		/// Method to enable and disable dropdown for vendor
		/// </summary>
		private void cboVendorEnableDisable()
        {
            if (_resupplyOrderLineDetailList.Count == 0)
            {
                this.cboVendor.IsEnabled = true;
            }
                if(_resupplyOrderLineDetailList.Count != 0)
            {
                this.cboVendor.IsEnabled = false;
            }
        }

		/// <summary>
		///  Weston Olund
		/// 2018/04/20
		/// Method to update the price
		/// </summary>
		private void updatePrice()
        {
            // loop through supply items and order items to calculate price from price each in supply items
            // and quantity from line items
            for (int i = 0; i < _resupplyOrderLineDetailList.Count; i++)
            {
                for (int j = 0; j < _entireSupplyItemDetailList.Count; j++)
                {
                    if(_resupplyOrderLineDetailList[i].VendorID == _entireSupplyItemDetailList[j].VendorID 
                        && _resupplyOrderLineDetailList[i].SupplyItemID == _entireSupplyItemDetailList[j].SupplyItem.SupplyItemID)
                    {
                        _resupplyOrderLineDetailList[i].Price = _resupplyOrderLineDetailList[i].Quantity * _entireSupplyItemDetailList[j].PriceEach;
                    }
                }
            }
        }

		/// <summary>
		/// Weston Olund
		/// 2018/04/20
		/// Method to handle after edit ends
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgOrderItems_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
		{
			
			if(this.dgOrderItems.SelectedItem != null)
			{
				(sender as DataGrid).RowEditEnding -= dgOrderItems_RowEditEnding;
				(sender as DataGrid).CommitEdit();
				(sender as DataGrid).Items.Refresh();
				(sender as DataGrid).RowEditEnding += dgOrderItems_RowEditEnding;
				updatePrice();
			}
			else
			{
				return;
			}
			
		}
	}
}
