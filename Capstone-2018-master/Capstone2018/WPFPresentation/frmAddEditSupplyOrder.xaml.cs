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
    /// Interaction logic for frmAddEditSupplyOrder.xaml
    /// </summary>
    public partial class frmAddEditSupplyOrder : Window
    {
        private ISupplyOrderManager _supplyOrderManager;
        private ISupplyOrderItemManager _supplyOrderItemManager = new SupplyOrderItemManager();
        private User _user;
        private DetailFormMode _mode;
        private SupplyOrder _originalSupplyOrder;
        private List<SupplyOrderItemDetail> _originalSupplyOrderItems;
        private List<SupplyOrderItemDetail> _newSupplyOrderItems = new List<SupplyOrderItemDetail>();
        private List<SupplyItem> _supplyItems;
        private ISupplyStatusManager _supplyStatusManager = new SupplyStatusManager();

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Constructor for add mode
        /// </summary>
        /// <param name="supplyOrderManager"></param>
        /// <param name="user"></param>
        public frmAddEditSupplyOrder(ISupplyOrderManager supplyOrderManager, User user, List<SupplyItem> supplyItems)
        {
            _supplyOrderManager = supplyOrderManager;
            _user = user;
            _supplyItems = supplyItems;
            _mode = DetailFormMode.Add;
            InitializeComponent();
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Constructor for edit mode
        /// </summary>
        /// <param name="supplyOrderManager"></param>
        /// <param name="user"></param>
        /// <param name="supplyOrder"></param>
        /// <param name="supplyOrderItems"></param>
        /// <param name="mode"></param>
        public frmAddEditSupplyOrder(ISupplyOrderManager supplyOrderManager, User user,
            SupplyOrder supplyOrder, List<SupplyOrderItemDetail> supplyOrderItems, List<SupplyItem> supplyItems, DetailFormMode mode)
        {
            _supplyOrderManager = supplyOrderManager;
            _user = user;
            _originalSupplyOrder = supplyOrder;
            _originalSupplyOrderItems = supplyOrderItems;
            _supplyItems = supplyItems;
            _mode = mode;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (_mode)
            {
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

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to set up add mode
        /// </summary>
        private void setupAddMode()
        {
            txtEmployee.Text = _user.Employee.FirstName;
            lblOrderDate.Content = DateTime.Now.ToShortDateString();
            populateSupplyStatusIDComboBox();
            cboStatus.SelectedValue = "Available";
            cboStatus.IsEnabled = false;
            dgItems.ItemsSource = _supplyItems;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to set up edit mode
        /// </summary>
        private void setupEditMode()
        {
            lblHeader.Content = "Edit Order #" + _originalSupplyOrder.SupplyOrderID;
            txtEmployee.Text = _user.Employee.FirstName;
            lblOrderDate.Content = DateTime.Now.ToShortDateString();
            populateSupplyStatusIDComboBox();
            dgItems.ItemsSource = _supplyItems;
            copyOrderItems();
            dgOrderItems.ItemsSource = _newSupplyOrderItems;
            cboStatus.SelectedItem = _originalSupplyOrder.SupplyStatusID;
            btnAddOrder.Content = "Edit";
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method that copies the original list to create a second list to hold changes
        /// </summary>
        private void copyOrderItems()
        {
            if (_originalSupplyOrderItems != null)
            {
                foreach (var orderItem in _originalSupplyOrderItems)
                {
                    _newSupplyOrderItems.Add(new SupplyOrderItemDetail
                    {
                        Name = orderItem.Name,
                        OrderItem = new SupplyOrderItem()
                        {
                            SupplyItemID = orderItem.OrderItem.SupplyItemID,
                            SupplyOrderID = orderItem.OrderItem.SupplyOrderID,
                            SupplyOrderLineID = orderItem.OrderItem.SupplyOrderLineID,
                            Quantity = orderItem.OrderItem.Quantity
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to populate supply status combo box
        /// Based on Weston Olund's method for resupply orders.
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


        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to add or edit a supply order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_mode == DetailFormMode.Add)
            {
                addSupplyOrder();
            }
            if (_mode == DetailFormMode.Edit)
            {
                editSupplyOrder();
            }
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to edit a supply order
        /// </summary>
        private void editSupplyOrder()
        {
            if (_originalSupplyOrderItems.Equals(_newSupplyOrderItems))
            {
                this.DialogResult = false;
            }
            else
            {
                try
                {
                    foreach (var orderItem in _originalSupplyOrderItems)
                    {
                        if (_newSupplyOrderItems.Exists(o => o.OrderItem.SupplyOrderLineID == orderItem.OrderItem.SupplyOrderLineID) == false)
                        {
                            _supplyOrderItemManager.DeleteSupplyOrderItem(orderItem.OrderItem.SupplyOrderLineID);
                        }
                    }
                    foreach (var orderItem in _newSupplyOrderItems)
                    {
                        if (_originalSupplyOrderItems.Exists(o => o.OrderItem.SupplyOrderLineID == orderItem.OrderItem.SupplyOrderLineID && o.Name.Equals(orderItem.Name)))
                        {
                            var originalOrder = _originalSupplyOrderItems.Find(o => o.OrderItem.SupplyOrderLineID == orderItem.OrderItem.SupplyOrderLineID && o.Name.Equals(orderItem.Name));
                            if (originalOrder.OrderItem.Quantity != orderItem.OrderItem.Quantity)
                            {
                                orderItem.OrderItem.SupplyOrderID = _originalSupplyOrder.SupplyOrderID;
                                _supplyOrderItemManager.EditSupplyOrderItem(originalOrder.OrderItem, orderItem.OrderItem);
                            }
                        }
                        else
                        {
                            orderItem.OrderItem.SupplyOrderID = _originalSupplyOrder.SupplyOrderID;
                            _supplyOrderItemManager.CreateSupplyOrderItem(orderItem.OrderItem);
                        }
                    }
                    var newOrder = new SupplyOrder()
                    {
                        Date = DateTime.Parse(lblOrderDate.Content.ToString()),
                        JobID = _originalSupplyOrder.JobID,
                        EmployeeID = _user.Employee.EmployeeID,
                        SupplyOrderID = _originalSupplyOrder.SupplyOrderID,
                        SupplyStatusID = cboStatus.SelectedItem.ToString()
                    };
                    if (_originalSupplyOrder.JobID == null)
                    {
                        _supplyOrderManager.EditSupplyOrderNoJob(_originalSupplyOrder, newOrder);
                    }
                    else
                    {
                        _supplyOrderManager.EditSupplyOrder(_originalSupplyOrder, newOrder);
                    }
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error editing the supply order.", ex.Message);
                    this.DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to add a supply order
        /// </summary>
        private void addSupplyOrder()
        {

            if (dgOrderItems.Items.Count == 0)
            {
                var result = MessageBox.Show("Are you sure you wish to cancel?", "No items added to the order", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
                else
                {
                    MessageBox.Show("Please add items to the order.");
                }
            }
            if (cboStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status for the order.");
            }
            else
            {
                try
                {
                    int supplyOrderID = _supplyOrderManager.CreateSupplyOrderNoJob(new SupplyOrder()
                    {
                        EmployeeID = _user.Employee.EmployeeID,
                        SupplyOrderID = 1000000,
                        JobID = null,
                        SupplyStatusID = cboStatus.SelectedItem.ToString(),
                        Date = DateTime.Parse(lblOrderDate.Content.ToString())
                    });
                    foreach (var order in _newSupplyOrderItems)
                    {
                        order.OrderItem.SupplyOrderID = supplyOrderID;
                        _supplyOrderItemManager.CreateSupplyOrderItem(order.OrderItem);
                    }
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error adding the supply order.", ex.Message);
                    this.DialogResult = false;
                }
            }
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to add a supply item to the supply order list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to add to the order.");
            }
            else
            {
                SupplyItem orderItem = (SupplyItem)dgItems.SelectedItem;
                if (_newSupplyOrderItems == null)
                {
                    _newSupplyOrderItems = new List<SupplyOrderItemDetail>();
                }
                var frmAddSupplyItem = new frmUpdateOrderItemQuantity(orderItem.Name, orderItem.SupplyItemID, _newSupplyOrderItems);
                var result = frmAddSupplyItem.ShowDialog();
                if (result == true)
                {
                    _newSupplyOrderItems = frmAddSupplyItem.Orders;
                }
                dgOrderItems.ItemsSource = null;
                dgOrderItems.ItemsSource = _newSupplyOrderItems;
            }
        }

        /// <summary>
        /// Jacob Conley
        /// Created on 2018/03/14
        /// 
        /// Method to remove a supply item from the supply order list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to remove from the order.");
            }
            else
            {
                SupplyOrderItemDetail orderItem = (SupplyOrderItemDetail)dgOrderItems.SelectedItem;
                var frmAddSupplyItem = new frmUpdateOrderItemQuantity(orderItem, _newSupplyOrderItems, DetailFormMode.Edit);
                var result = frmAddSupplyItem.ShowDialog();
                if (result == true)
                {
                    _newSupplyOrderItems = frmAddSupplyItem.Orders;
                }
                dgOrderItems.ItemsSource = null;
                dgOrderItems.ItemsSource = _newSupplyOrderItems;
            }
        }
    }
}
